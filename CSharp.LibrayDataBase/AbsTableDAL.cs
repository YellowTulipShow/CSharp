using System;
using System.Text;
using System.Data;
using System.Collections.Generic;
using System.Reflection;
using CSharp.LibrayFunction;
using CSharp.LibrayDataBase.Utils;

namespace CSharp.LibrayDataBase
{
    /// <summary>
    /// 数据表-访问类
    /// </summary>
    public abstract class AbsTableDAL<M> :
        IPropertyColumn, IAutoTable, IBasicsSQL<M>, ITableBasicFunction<M>
        where M : AbsModel_Null
    {
        private ColumnModel[] alltypeColums = null;

        /// <summary>
        /// 构造函数
        /// </summary>
        public AbsTableDAL() {
            EXECreateTable();
            SetALLTypeColumns();
        }

        /// <summary>
        /// 执行创建数据表
        /// </summary>
        private void EXECreateTable() {
            DbHelperSQL.GetSingle(SQLCreateTable());
        }

        /// <summary>
        /// 设置所有 类型 列
        /// </summary>
        private void SetALLTypeColumns() {
            this.alltypeColums = AnalysisPropertyColumns();
        }

        /// <summary>
        /// 获得所有 类型 列
        /// </summary>
        public ColumnModel[] GetALLTypeColumns() {
            if (CheckData.IsSizeEmpty(alltypeColums))
                SetALLTypeColumns();
            return alltypeColums;
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
        public ColumnModel[] AnalysisPropertyColumns() {
            Type modelT = typeof(M);
            if (!modelT.IsDefined(typeof(TableAttribute), false)) {
                return new ColumnModel[] { };
            }

            List<ColumnModel> colms = new List<ColumnModel>();
            PropertyInfo[] protertys = modelT.GetProperties();
            foreach (PropertyInfo pro in protertys) {
                ColumnAttribute columnAttr = ReflexHelper.FindAttributesOnly<ColumnAttribute>(pro);
                if (CheckData.IsObjectNull(columnAttr))
                    continue;
                colms.Add(new ColumnModel() {
                    Name = pro.Name,
                    Property = pro,
                    ColAttr = columnAttr
                });
            }
            colms.Sort(ColumnModel.Sort);
            return colms.ToArray();
        }

        /// <summary>
        /// 获取主键列
        /// </summary>
        /// <returns>若无,返回null</returns>
        public PropertyInfo PrimaryKeyColumn() {
            foreach (ColumnModel item in GetALLTypeColumns()) {
                if (item.ColAttr.IsPrimaryKey)
                    return item.Property;
            }
            return null;
        }

        /// <summary>
        /// 获取主键并且是ID标识列
        /// </summary>
        /// <returns>若无,返回null</returns>
        public PropertyInfo IDentityColumn() {
            foreach (ColumnModel item in GetALLTypeColumns()) {
                if (item.ColAttr.IsPrimaryKey && item.ColAttr.IsIDentity)
                    return item.Property;
            }
            return null;
        }

        /// <summary>
        /// 获取能执行插入语句的列
        /// </summary>
        public ColumnModel[] CanGetSetColumns() {
            List<ColumnModel> colms = new List<ColumnModel>();
            foreach (ColumnModel item in GetALLTypeColumns()) {
                if (!item.ColAttr.IsDbGenerated)
                    colms.Add(item);
            }
            return colms.ToArray();
        }
        #endregion


        #region === IAutoTable ===
        public abstract string SQLCreateTable();
        public abstract string SQLClearTable();
        public abstract string SQLKillTable();
        #endregion


        #region === IBasicsSQL<M> ===
        /// <summary>
        /// 产生 插入 SQL
        /// </summary>
        public virtual string SQLInsert(M model) {
            List<string> fieldArr = new List<string>();
            List<string> valueArr = new List<string>();

            foreach (ColumnModel item in this.CanGetSetColumns()) {
                object value = item.Property.GetValue(model, null);
                value = item.ColAttr.DbType.PrintSaveValue(value);
                if (CheckData.IsObjectNull(value))
                    continue;
                fieldArr.Add(item.Property.Name);
                valueArr.Add(string.Format("'{0}'", value));
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
            foreach (ColumnModel item in this.CanGetSetColumns()) {
                object value = item.Property.GetValue(model, null);
                value = item.ColAttr.DbType.PrintSaveValue(value);
                if (CheckData.IsObjectNull(value))
                    continue;
                setArr.Add(string.Format("{0} = '{1}'", item.Property.Name, value.ToString()));
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
                CheckData.IsStringNull(nowVal.ToString().Trim())) {
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
            if (CheckData.IsStringNull(strWhere.Trim())) {
                strSql.Append(" where " + strWhere);
            }
            return ConvertTool.ObjToInt(DbHelperSQL.GetSingle(strSql.ToString()), 0);
        }

        public virtual bool Insert(M model, out int IDentity) {
            const int errorID = 0;
            IDentity = errorID;
            string sqlinsert = SQLInsert(model);
            if (CheckData.IsStringNull(sqlinsert.Trim()))
                return false;
            string strSql = sqlinsert + " ;select @@IDENTITY; ";
            object obj = DbHelperSQL.GetSingle(strSql);
            IDentity = CheckData.IsObjectNull(obj) ? errorID : ConvertTool.ObjToInt(obj, errorID);
            return IDentity == errorID ? false : true;
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
            foreach (ColumnModel item in GetALLTypeColumns()) {
                object value = row[item.Property.Name];
                if (!CheckData.IsObjectNull(item.Property) && !CheckData.IsObjectNull(value)) {
                    item.Property.SetValue(model, value, null);
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
            string selectStr = SQLALLSelectWhere(top, strWhere, orderbyStr);
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

        internal string SQLALLSelectWhere(int top, string strWhere, string orderBy) {
            string column = top > 0 ? string.Format(@"top {0} *", top) : @"*";
            string sql = string.Format(@"select {0} from {1}", column, DefaultModel().GetTableName());
            if (!CheckData.IsStringNull(strWhere.Trim()))
                sql += string.Format(@" where {0}", strWhere);
            if (!CheckData.IsStringNull(orderBy.Trim()))
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