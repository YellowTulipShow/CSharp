using System;
using System.Text;
using System.Collections.Generic;
using System.Reflection;
using CSharp.LibrayFunction;
using CSharp.LibrayDataBase.Utils;
using System.Data;

namespace CSharp.LibrayDataBase
{
    /// <summary>
    /// 数据表-访问类
    /// </summary>
    public abstract class AbsTableDAL<M> :
        IPropertyColumn, IBasicsSQL<M>, ITableBasicFunction<M>
        where M : AbsModel_Null
    {
        /// <summary>
        /// 所有 类型 列
        /// </summary>
        public Dictionary<PropertyInfo, ColumnAttribute> ALLTypeColumns {
            get {
                _alltypeColums = CheckData.IsSizeEmpty(_alltypeColums) ? AnalysisPropertyColumns() : _alltypeColums;
                return _alltypeColums;
            }
        }
        private Dictionary<PropertyInfo, ColumnAttribute> _alltypeColums = null;

        /// <summary>
        /// 构造函数
        /// </summary>
        public AbsTableDAL() {
        }

        /// <summary>
        /// 默认模型实例
        /// </summary>
        public M DefaultModel() {
            return System.Activator.CreateInstance<M>();
        }

        #region === IPropertyColumn ===
        /// <summary>
        /// 接口: IPropertyColumn 解析-属性-列
        /// </summary>
        public Dictionary<PropertyInfo, ColumnAttribute> AnalysisPropertyColumns() {
            Type modelT = typeof(M);
            if (!modelT.IsDefined(typeof(TableAttribute), false)) {
                return new Dictionary<PropertyInfo, ColumnAttribute>();
            }

            Dictionary<PropertyInfo, ColumnAttribute> dictionary = new Dictionary<PropertyInfo, ColumnAttribute>();
            PropertyInfo[] protertys = modelT.GetProperties();
            foreach (PropertyInfo pro in protertys) {
                ColumnAttribute columnAttr = ReflexHelper.FindAttributesOnly<ColumnAttribute>(pro);
                if (CheckData.IsObjectNull(columnAttr))
                    continue;
                dictionary[pro] = columnAttr;
            }
            return dictionary;
        }

        /// <summary>
        /// 获取主键列
        /// </summary>
        /// <returns>若无,返回null</returns>
        public PropertyInfo PrimaryKeyColumn() {
            foreach (KeyValuePair<PropertyInfo, ColumnAttribute> item in this.ALLTypeColumns) {
                if (item.Value.IsPrimaryKey)
                    return item.Key;
            }
            return null;
        }
        /// <summary>
        /// 获取主键并且自动生成的列
        /// </summary>
        /// <returns>若无,返回null</returns>
        public PropertyInfo IDentityColumn() {
            foreach (KeyValuePair<PropertyInfo, ColumnAttribute> item in this.ALLTypeColumns) {
                if (item.Value.IsPrimaryKey && item.Value.IsDbGenerated)
                    return item.Key;
            }
            return null;
        }

        /// <summary>
        /// 获取能执行插入语句的列
        /// </summary>
        public Dictionary<PropertyInfo, ColumnAttribute> CanGetSetColumns() {
            Dictionary<PropertyInfo, ColumnAttribute> dictionary = new Dictionary<PropertyInfo, ColumnAttribute>();
            foreach (KeyValuePair<PropertyInfo, ColumnAttribute> item in this.ALLTypeColumns) {
                if (!item.Value.IsDbGenerated)
                    dictionary[item.Key] = item.Value;
            }
            return dictionary;
        }
        #endregion

        #region === IBasicsSQL<M> ===
        /// <summary>
        /// 产生 插入 SQL
        /// </summary>
        public virtual string SQLInsert(M model) {
            List<string> fieldArr = new List<string>();
            List<string> valueArr = new List<string>();

            foreach (KeyValuePair<PropertyInfo, ColumnAttribute> item in this.CanGetSetColumns()) {
                object value = item.Key.GetValue(model, null);
                if (CheckData.IsObjectNull(value))
                    continue;
                fieldArr.Add(item.Key.Name);
                valueArr.Add(string.Format("'{0}'", value.ToString()));
            }

            if ((fieldArr.Count != valueArr.Count) && CheckData.IsSizeEmpty(fieldArr)) {
                return string.Empty;
            }

            string fieldStr = ConvertTool.IListToString(fieldArr, ',');
            string valueStr = ConvertTool.IListToString(valueArr, ',');
            return string.Format("insert into {0}({1}) values({2})", model.GetTableName(), fieldStr, valueStr);
        }

        /// <summary>
        /// 产生 删除 SQL 异常: CreateSQLNotHaveWhereException
        /// </summary>
        public virtual string SQLDelete(M model) {
            return string.Format("delete {0} where {1}", model.GetTableName(), CreateSignSQLWhere(model));
        }

        /// <summary>
        /// 产生 更新 SQL 异常: CreateSQLNotHaveWhereException
        /// </summary>
        public virtual string SQLUpdate(M model) {
            List<string> setArr = new List<string>();
            foreach (KeyValuePair<PropertyInfo, ColumnAttribute> item in this.CanGetSetColumns()) {
                object value = item.Key.GetValue(model, null);
                if (CheckData.IsObjectNull(value))
                    continue;
                setArr.Add(string.Format("{0} = '{1}'", item.Key.Name, value.ToString()));
            }

            if (CheckData.IsSizeEmpty(setArr)) {
                return string.Empty;
            }
            string setStr = ConvertTool.IListToString(setArr, ',');
            return string.Format("update {0} set {1} where {2}", model.GetTableName(), setStr, CreateSignSQLWhere(model));
        }

        /// <summary>
        /// 产生 标记SQL条件
        /// </summary>
        /// <param name="model">数据来源</param>
        public virtual string CreateSignSQLWhere(M model) {
            PropertyInfo property = null;
            property = !CheckData.IsObjectNull(property) ? property : PrimaryKeyColumn();
            property = !CheckData.IsObjectNull(property) ? property : IDentityColumn();
            if (CheckData.IsObjectNull(property)) {
                throw new CreateSQLNotHaveWhereException();
            }
            object nowVal = property.GetValue(model, null);
            if (CheckData.IsObjectNull(nowVal) ||
                nowVal.ToString() == property.GetValue(DefaultModel(), null).ToString() ||
                CheckData.IsStringNull(nowVal.ToString())) {
                throw new CreateSQLNotHaveWhereException();
            }
            string where = string.Format("{0} = '{1}'", property.Name, nowVal.ToString());
            return where;
        }
        /// <summary>
        /// 创建SQL语句没有where-产生异常
        /// </summary>
        public class CreateSQLNotHaveWhereException : Exception
        {
            public override string Message {
                get { return @"创建数据SQL字符串时, 无法确定 where 条件, 会产生重大隐患!"; }
            }
        }
        #endregion

        #region === ITableBasicFunction<M> ===
        public virtual int GetRecordCount(string strWhere) {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(*) as H from " + DefaultModel().GetTableName());
            if (CheckData.IsStringNull(strWhere)) {
                strSql.Append(" where " + strWhere);
            }
            return ConvertTool.ObjToInt(DbHelperSQL.GetSingle(strSql.ToString()), 0);
        }

        public virtual bool Insert(M model, out int IDentity) {
            string strSql = SQLInsert(model) + " ;select @@IDENTITY; ";
            object obj = DbHelperSQL.GetSingle(strSql);
            IDentity = CheckData.IsObjectNull(obj) ? 0 : ConvertTool.ObjToInt(obj, 0);
            return IDentity == 0 ? false : true;
        }

        public virtual bool Delete(M model) {
            return DbHelperSQL.ExecuteSql(SQLDelete(model)) > 0;
        }

        public virtual bool Update(M model) {
            return DbHelperSQL.ExecuteSql(SQLUpdate(model)) > 0;
        }

        public virtual M GetModel(int IDentity) {
            string sql = string.Format("select top 1 * from {0} where {1} = {2}",
                DefaultModel().GetTableName(),
                IDentityColumn().Name,
                IDentity);
            DataSet ds = DbHelperSQL.Query(sql);
            return CheckReturnModel(ds);
        }

        public virtual M DataRowToModel(DataRow row) {
            if (CheckData.IsSizeEmpty(row))
                return null;
            M model = DefaultModel();
            foreach (KeyValuePair<PropertyInfo, ColumnAttribute> item in this.ALLTypeColumns) {
                object value = row[item.Key.Name];
                if (!CheckData.IsObjectNull(item.Key) && !CheckData.IsObjectNull(value)) {
                    item.Key.SetValue(model, value, null);
                }
            }
            return model;
        }

        public virtual M[] GetModelList(DataTable dt) {
            if (CheckData.IsSizeEmpty(dt))
                return new M[] { };
            List<M> list = new List<M>();
            foreach (DataRow dr in dt.Rows) {
                list.Add(DataRowToModel(dr));
            }
            return list.ToArray();
        }

        public virtual DataTable GetList(int top, string strWhere, Dictionary<string, bool> fieldOrders) {
            string orderbyStr = AnalysisDictionaryOrderByInfos(fieldOrders);
            string selectStr = SQLSelectAllWhere(top, strWhere, orderbyStr);
            DataSet ds = DbHelperSQL.Query(selectStr);
            return CheckReturnDataTable(ds);
        }
        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="pageCount">定义: 每页记录数</param>
        /// <param name="pageIndex">gi定义: 浏览到第几页</param>
        /// <param name="recordCount">得到: 总记录数</param>
        /// <param name="strWhere">定义: 查询条件</param>
        /// <param name="fieldOrders">定义: 字段排序集合, true 为正序, false 倒序</param>
        /// <returns>结果数据表</returns>
        public abstract DataTable GetList(int pageCount, int pageIndex, out int recordCount, string strWhere, Dictionary<string, bool> fieldOrders);

        internal string SQLSelectAllWhere(int top, string strWhere, string orderBy) {
            string column = top > 0 ? string.Format(@" top {0} * ", top) : @" * ";
            string sql = string.Format(@"select {0} from {1}", column, DefaultModel().GetTableName());
            if (!CheckData.IsStringNull(strWhere))
                sql += string.Format(@" where {0}", strWhere);
            if (!CheckData.IsStringNull(strWhere))
                sql += string.Format(@" order by {0}", orderBy);
            return sql;
        }
        internal string AnalysisDictionaryOrderByInfos(Dictionary<string, bool> fieldOrders) {
            if (CheckData.IsSizeEmpty(fieldOrders))
                return string.Empty;
            List<string> fields = new List<string>();
            foreach (KeyValuePair<string, bool> item in fieldOrders) {
                string symbol = item.Value ? @"asc" : @"desc";
                fields.Add(string.Format(@"{0} {1}", item.Key, symbol));
            }
            return ConvertTool.IListToString(fields, ',');
        }
        internal DataTable CheckReturnDataTable(DataSet ds) {
            return CheckData.IsSizeEmpty(ds) ? null : CheckData.IsSizeEmpty(ds.Tables[0]) ? null : ds.Tables[0];
        }
        internal M CheckReturnModel(DataSet ds) {
            return CheckData.IsSizeEmpty(CheckReturnDataTable(ds)) ? null : DataRowToModel(ds.Tables[0].Rows[0]);
        }
        #endregion
    }
}