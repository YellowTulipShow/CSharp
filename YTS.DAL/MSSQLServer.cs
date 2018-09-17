using System;
using System.Collections.Generic;
using System.Data;
using YTS.Engine.DataBase;
using YTS.Engine.DataBase.MSQLServer;
using YTS.Model;
using YTS.Model.Table;
using YTS.Tools;

namespace YTS.DAL
{
    /// <summary>
    /// Microsoft SQL Server 2008 数据库-数据访问层(Data Access Layer)
    /// </summary>
    /// <typeparam name="M">数据映射模型</typeparam>
    public class MSSQLServer<M> :
        AbsDAL<M>,
        ITableName,
        IDataBaseResult<M>
        where M : AbsTable
    {
        /// <summary>
        /// 字段: 表名
        /// </summary>
        private readonly string _tableName_ = string.Empty;
        /// <summary>
        /// 列数据模型解析器
        /// </summary>
        public readonly ColumnModelParser<M> modelParser = null;

        public MSSQLServer() : base() {
            this.modelParser = new ColumnModelParser<M>();
            this._tableName_ = ReflexHelp.CreateNewObject<M>().GetTableName();
        }

        #region ====== using:ITableName ======
        /// <summary>
        /// 获得当前表 全名 名称
        /// </summary>
        public string GetTableName() {
            return this._tableName_;
        }
        #endregion

        #region ====== using:IBasicDataAccess<M> ======
        /// <summary>
        /// 插入一条数据
        /// </summary>
        /// <param name="model">数据来源</param>
        /// <returns>是否成功</returns>
        public override bool Insert(M model) {
            string sqlinsert = ConvertTool.StrToStrTrim(SQLInsert(model, false));
            return CheckData.IsStringNull(sqlinsert) ? false : DbHelperSQL.ExecuteSql(sqlinsert) > 0;
        }
        protected string SQLInsert(M model, bool isResultID) {
            List<string> fieldArr = new List<string>();
            List<string> valueArr = new List<string>();
            foreach (ColumnInfo item in this.modelParser.GetColumn_CanWrite()) {
                KeyObject im = this.modelParser.GetModelValue(item, model);
                if (CheckData.IsObjectNull(im) || CheckData.IsStringNull(im.Key)) {
                    continue;
                }
                fieldArr.Add(im.Key);
                string str_value = ModelValueToDataBaseValue(im.Value);
                valueArr.Add(string.Format("'{0}'", str_value));
            }
            if ((fieldArr.Count != valueArr.Count) || CheckData.IsSizeEmpty(fieldArr)) {
                return string.Empty;
            }
            return CreateSQL.Insert(this.GetTableName(), fieldArr.ToArray(), valueArr.ToArray(), isResultID);
        }

        /// <summary>
        /// 将映射模型的值转为数据库识别的数据值
        /// </summary>
        /// <param name="model_value">需要转化的映射模型值</param>
        /// <returns>数据库数据值</returns>
        public string ModelValueToDataBaseValue(object model_value) {
            if (CheckData.IsTypeValue<DateTime>(model_value)) {
                return ((DateTime)model_value).ToString(Model.Const.Format.DATETIME_MILLISECOND);
            }
            if (CheckData.IsTypeValue<Enum>(model_value)) {
                return ((int)model_value).ToString();
            }
            return ConvertTool.ObjToString(model_value);
        }

        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="where">删除条件</param>
        /// <returns>是否成功</returns>
        public override bool Delete(string where) {
            if (CheckData.IsStringNull(where)) {
                return false;
            }
            string sqldelete = CreateSQL.Delete(this.GetTableName(), where);
            return CheckData.IsStringNull(sqldelete) ? false : DbHelperSQL.ExecuteSql(sqldelete) > 0;
        }

        /// <summary>
        /// 更新数据
        /// </summary>
        /// <param name="keyvaluedic">更新的内容和其值</param>
        /// <param name="where">筛选更新的条件</param>
        /// <returns>是否成功</returns>
        public override bool Update(KeyString[] keyvaluedic, string where) {
            if (CheckData.IsSizeEmpty(keyvaluedic) || CheckData.IsStringNull(where)) {
                return false;
            }
            string[] expressions = ConvertTool.ListConvertType(keyvaluedic, item => {
                if (CheckData.IsStringNull(item.Key)) {
                    return null;
                }
                return string.Format("{0} = '{1}'", item.Key, item.Value);
            }, null);
            string set_str = ConvertTool.IListToString(expressions, ',');
            string sql_update = CreateSQL.Update(this.GetTableName(), set_str, where);
            return CheckData.IsStringNull(sql_update) ? false : DbHelperSQL.ExecuteSql(sql_update) > 0;
        }

        /// <summary>
        /// 查询数据
        /// </summary>
        /// <param name="top">返回的记录数</param>
        /// <param name="where">查询条件</param>
        /// <param name="sort">排序条件</param>
        /// <returns>映射数据模型列表</returns>
        public override M[] Select(int top = 0, string where = null, string sort = null) {
            DataSet ds = QueryRecords(top, where, sort);
            return DataSetToModels(ds);
        }

        /// <summary>
        /// 分页查询数据
        /// </summary>
        /// <param name="pageCount">定义: 每页记录数</param>
        /// <param name="pageIndex">定义: 浏览到第几页</param>
        /// <param name="recordCount">得到: 总记录数</param>
        /// <param name="where">定义: 查询条件</param>
        /// <param name="sort">定义: 字段排序集合, true 为正序, false 倒序</param>
        /// <returns>映射数据模型列表</returns>
        public override M[] Select(int pageCount, int pageIndex, out int recordCount, string where = null, string sort = null) {
            DataSet ds = QueryRecords(pageCount, pageIndex, out recordCount, where, sort);
            return DataSetToModels(ds);
        }

        /// <summary>
        /// 统计符合查询条件的记录总数
        /// </summary>
        /// <param name="where">查询条件</param>
        /// <returns>记录总数</returns>
        public override int GetRecordCount(string where = null) {
            const int errorint = 0;
            string sql_select = CreateSQL.Select_Count(this.GetTableName(), where);
            if (CheckData.IsStringNull(sql_select)) {
                return errorint;
            }
            object value = DbHelperSQL.GetSingle(sql_select);
            return ConvertTool.ObjToInt(value, errorint);
        }
        #endregion

        #region ====== using:IDatathisResult<M> ======
        /// <summary>
        /// 查询数据
        /// </summary>
        /// <param name="top">返回的记录数</param>
        /// <param name="sql_where">查询条件</param>
        /// <param name="sql_order">排序条件</param>
        /// <returns>结果数据表</returns>
        public DataSet QueryRecords(int top = 0, string sql_where = null, string sql_order = null) {
            string sql_select = CreateSQL.Select(this.GetTableName(), top, sql_where, sql_order);
            if (CheckData.IsStringNull(sql_select)) {
                return new DataSet();
            }
            DataSet ds = DbHelperSQL.Query(sql_select);
            return ds;
        }

        /// <summary>
        /// 分页查询数据
        /// </summary>
        /// <param name="pageCount">定义: 每页记录数</param>
        /// <param name="pageIndex">定义: 浏览到第几页</param>
        /// <param name="recordCount">得到: 总记录数</param>
        /// <param name="sql_where">定义: 查询条件</param>
        /// <param name="sql_order">定义: 字段排序集合, true 为正序, false 倒序</param>
        /// <returns>结果数据表</returns>
        public DataSet QueryRecords(int pageCount, int pageIndex, out int recordCount, string sql_where = null, string sql_order = null) {
            recordCount = GetRecordCount(sql_where);
            string sql_paging = PagingHelper.CreatePagingSql(recordCount, pageCount, pageIndex, sql_where, sql_order);
            if (CheckData.IsStringNull(sql_paging)) {
                return new DataSet();
            }
            DataSet ds = DbHelperSQL.Query(sql_paging);
            return ds;
        }

        /// <summary>
        /// 数据集 转为 模型列表
        /// </summary>
        /// <param name="ds">数据集</param>
        /// <returns>模型列表</returns>
        public M[] DataSetToModels(DataSet ds) {
            if (CheckData.IsSizeEmpty(ds)) {
                return new M[] { };
            }
            List<M> list = new List<M>();
            foreach (DataTable dt in ds.Tables) {
                if (CheckData.IsSizeEmpty(dt)) {
                    continue;
                }
                foreach (DataRow dr in dt.Rows) {
                    M model = DataRowToModel(dr);
                    if (!CheckData.IsObjectNull(model)) {
                        list.Add(model);
                    }
                }
            }
            return list.ToArray();
        }

        /// <summary>
        /// 数据行 转为 单个映射模型
        /// </summary>
        /// <param name="row">数据行</param>
        /// <returns>单个映射模型</returns>
        public M DataRowToModel(DataRow row) {
            if (CheckData.IsSizeEmpty(row)) {
                return null;
            }
            M model = ReflexHelp.CreateNewObject<M>();
            ColumnInfo[] columninfos = this.modelParser.GetColumn_ALL();
            foreach (ColumnInfo item in columninfos) {
                if (CheckData.IsObjectNull(item)) {
                    continue;
                }
                object value = row[item.Property.Name];
                model = this.modelParser.SetModelValue(item, model, value);
            }
            return model;
        }
        #endregion





        /// <summary>
        /// 执行-SQL字符串事务处理
        /// </summary>
        /// <param name="sqllist">SQL字符串列表</param>
        /// <returns>是否成功</returns>
        public static bool Transaction(string[] sqllist) {
            if (CheckData.IsSizeEmpty(sqllist)) {
                return false;
            }
            bool resu = DbHelperSQL.ExecuteTransaction(sqllist);
            return resu;
        }

        # region Supplementary new Method:
        /*
        /// <summary>
        /// 执行补全
        /// </summary>
        public override void Supplementary() {
            // sql if 判断条件 判断表是否不存在
            string str_if_where = CreateSQL.NotExists(CreateSQL.MSSSysTable(GetTableName()));
            // sql 列的名称 和 数据类型格式 数据源集合
            Dictionary<string, string> columns = GetCreateColumns();

            string[] column_formats = ConvertTool.ListConvertType(columns, d => d.Value);
            // 不存在 需要创建表
            string SQL_CreateTable = CreateSQL.CreateTable(GetTableName(), column_formats);

            //存在 需要补全缺失的列
            string SQL_AlterColumns = SQLAlterColumns(columns);

            string sql = CreateSQL.If(str_if_where, SQL_CreateTable, SQL_AlterColumns);
            DbHelperSQL.GetSingle(sql);
        }
        /// <summary>
        /// 获得列信息
        /// </summary>
        private Dictionary<string, string> GetCreateColumns() {
            Dictionary<string, string> resuDic = new Dictionary<string, string>();
            foreach (ColumnItemModel item in this.modelParser.ColumnInfoArray) {
                string fieldName = item.Property.Name;
                if (resuDic.ContainsKey(fieldName)) {
                    continue;
                }
                //string typeName = item.Attribute.DTParser.TypeName();
                string typeName = string.Empty;
                string[] vals = new string[] {
                    fieldName,
                    typeName,
                    item.Attribute.IsPrimaryKey ? @"primary key" : null,
                    !item.Attribute.IsCanBeNull ? @"not null" : null,
                    item.Attribute.IsIDentity ? @"identity(1,1)" : null,
                };
                resuDic[fieldName] = ConvertTool.IListToString(vals, @" ");
            }
            return resuDic;
        }
        /// <summary>
        /// 获得补全表列信息
        /// </summary>
        private string SQLAlterColumns(Dictionary<string, string> columns) {
            List<string> ifExists = new List<string>();
            foreach (KeyValuePair<string, string> item in columns) {
                string if_where = CreateSQL.NotExists(CreateSQL.MSSSysColumns(GetTableName(), item.Key));
                string sql = CreateSQL.If(if_where, CreateSQL.AlterColumn(GetTableName(), item.Value));
                ifExists.Add(sql);
            }
            return ConvertTool.IListToString(ifExists, @" ");
        }
        */
        #endregion
    }
}
