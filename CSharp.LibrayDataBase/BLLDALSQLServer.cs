using System;
using System.Data;
using System.Text;
using System.Linq;
using System.Reflection;
using System.Collections.Generic;
using CSharp.LibrayDataBase.Utils;
using CSharp.LibrayFunction;

namespace CSharp.LibrayDataBase
{
    /// <summary>
    /// Microsoft SQL Server 2008 版本数据库 数据访问器
    /// </summary>
    /// <typeparam name="M">数据访问模型</typeparam>
    public class BLLSQLServer<D, M> : AbsBLL<D, M>
        where D : DALSQLServer<M>
        where M : AbsModelNull
    {
        public BLLSQLServer(D dal) : base(dal) { }
    }

    /// <summary>
    /// Microsoft SQL Server 2008 版本数据库 数据访问器
    /// </summary>
    /// <typeparam name="M">数据访问模型</typeparam>
    public class DALSQLServer<M> : AbsDAL<M> where M : AbsModelNull
    {
        public DALSQLServer() : base() { }

        #region ====== override:AbsDAL<M> ======
        /// <summary>
        /// 增
        /// </summary>
        public override bool Insert(M model) {
            string sqlinsert = ConvertTool.StrToStrTrim(SQLInsert(model, false));
            return CheckData.IsStringNull(sqlinsert) ? false : DbHelperSQL.ExecuteSql(sqlinsert) > 0;
        }
        protected string SQLInsert(M model, bool isResultID) {
            List<string> fieldArr = new List<string>();
            List<string> valueArr = new List<string>();
            foreach (ColumnItemModel item in base.GetCanWriteColumn()) {
                KeyValueModel im = base.modelParser.GetModelValue(item, model);
                if (CheckData.IsObjectNull(im) || CheckData.IsStringNull(im.Key))
                    continue;
                fieldArr.Add(im.Key);
                valueArr.Add(string.Format("'{0}'", im.Value));
            }
            if ((fieldArr.Count != valueArr.Count) || CheckData.IsSizeEmpty(fieldArr)) {
                return string.Empty;
            }
            return CreateSQL.Insert(base.GetTableName(), fieldArr.ToArray(), valueArr.ToArray(), isResultID);
        }

        /// <summary>
        /// 删
        /// </summary>
        public override bool Delete(WhereModel wheres) {
            if (!WhereModel.CheckIsCanUse(wheres)) {
                return false;
            }
            string where = CreateSQL.ParserWhereModel(wheres, base.modelParser);
            string sqldelete = CreateSQL.Delete(base.GetTableName(), where);

            return CheckData.IsStringNull(sqldelete) ? false : DbHelperSQL.ExecuteSql(sqldelete) > 0;
        }
        /// <summary>
        /// 改
        /// </summary>
        public override bool Update(FieldValueModel[] fielvals, WhereModel wheres) {
            if (!FieldValueModel.CheckIsCanUse(fielvals) || !WhereModel.CheckIsCanUse(wheres)) {
                return false;
            }
            string[] expressions = CreateSQL.ParserFieldValueModel(fielvals, base.modelParser,
                isFixedOperChar: true, fixedOperChar: DataChar.OperChar.EQUAL);
            string set_str = ConvertTool.IListToString(expressions, ',');
            string where_str = CreateSQL.ParserWhereModel(wheres, base.modelParser);
            string sql_update = CreateSQL.Update(base.GetTableName(), set_str, where_str);
            if (CheckData.IsStringNull(sql_update)) {
                return false;
            }
            return DbHelperSQL.ExecuteSql(sql_update) > 0;
        }

        /// <summary>
        /// 查
        /// </summary>
        public override M[] Select(int top = 0, WhereModel wheres = null, FieldOrderModel[] fieldOrders = null) {
            DataTable dt = SelectSource(top, wheres, fieldOrders);
            return GetModelList(dt);
        }
        /// <summary>
        /// 分页 查
        /// </summary>
        /// <param name="pageCount">定义: 每页记录数</param>
        /// <param name="pageIndex">定义: 浏览到第几页</param>
        /// <param name="recordCount">得到: 总记录数</param>
        /// <param name="wheres">定义: 查询条件</param>
        /// <param name="fieldOrders">定义: 字段排序集合, true 为正序, false 倒序</param>
        /// <returns>结果数据表</returns>
        public override M[] Select(int pageCount, int pageIndex, out int recordCount, WhereModel wheres = null, FieldOrderModel[] fieldOrders = null) {
            DataTable dt = SelectSource(pageCount, pageIndex, out recordCount, wheres, fieldOrders);
            return GetModelList(dt);
        }
        private M[] GetModelList(DataTable dt) {
            if (CheckData.IsSizeEmpty(dt))
                return new M[] { };
            List<M> list = new List<M>();
            foreach (DataRow dr in dt.Rows) {
                list.Add(DataRowToModel(dr));
            }
            return list.ToArray();
        }
        private M DataRowToModel(DataRow row) {
            if (CheckData.IsSizeEmpty(row))
                return null;
            M model = base.modelParser.CreateDefaultModel();
            foreach (ColumnItemModel item in base.modelParser.ColumnInfoArray) {
                if (CheckData.IsObjectNull(item)) {
                    continue;
                }
                object value = row[item.Property.Name];
                model = base.modelParser.SetModelValue(item, model, value);
            }
            return model;
        }
        private DataTable SelectSource(int top, WhereModel wheres, FieldOrderModel[] fieldOrders) {
            string strWhere = CreateSQL.ParserWhereModel(wheres, base.modelParser);
            string orderbyStr = CreateSQL.ParserFieldOrderModel(fieldOrders);
            string selectStr = CreateSQL.Select(base.GetTableName(), top, strWhere, orderbyStr);
            if (CheckData.IsStringNull(selectStr)) {
                return new DataTable();
            }
            DataSet ds = DbHelperSQL.Query(selectStr);
            return CheckReturnDataTable(ds);
        }
        private DataTable SelectSource(int pageCount, int pageIndex, out int recordCount, WhereModel wheres, FieldOrderModel[] fieldOrders) {
            string strWhere = CreateSQL.ParserWhereModel(wheres, base.modelParser);
            string orderbyStr = CreateSQL.ParserFieldOrderModel(fieldOrders);
            string selectStr = CreateSQL.Select(base.GetTableName(), 0, strWhere, orderbyStr);
            recordCount = GetRecordCount(strWhere);
            if (CheckData.IsStringNull(selectStr)) {
                return new DataTable();
            }
            DataSet ds = DbHelperSQL.Query(PagingHelper.CreatePagingSql(recordCount, pageCount, pageIndex, selectStr, orderbyStr));
            return CheckReturnDataTable(ds);
        }
        private DataTable CheckReturnDataTable(DataSet ds) {
            return CheckData.IsSizeEmpty(ds) ? null : CheckData.IsSizeEmpty(ds.Tables[0]) ? null : ds.Tables[0];
        }


        /// <summary>
        /// 获得条件的记录总数
        /// </summary>
        /// <param name="wheres">条件</param>
        public override int GetRecordCount(WhereModel wheres) {
            return GetRecordCount(CreateSQL.ParserWhereModel(wheres, base.modelParser));
        }
        private int GetRecordCount(string wheresql) {
            const int errorint = 0;
            string sql_select = CreateSQL.Select_Count(base.GetTableName(), wheresql);
            if (CheckData.IsStringNull(sql_select)) {
                return errorint;
            }
            object value = DbHelperSQL.GetSingle(sql_select);
            return ConvertTool.ObjToInt(value, errorint);
        }


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
            foreach (ColumnItemModel item in base.modelParser.ColumnInfoArray) {
                string fieldName = item.Property.Name;
                if (resuDic.ContainsKey(fieldName)) {
                    continue;
                }
                string typeName = item.Attribute.DTParser.TypeName();
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
    }


    /// <summary>
    /// Microsoft SQL Server 2008 版本数据库 数据访问器
    /// </summary>
    /// <typeparam name="M">数据访问模型</typeparam>
    public class BLLSQLServerID<D, M> : BLLSQLServer<D, M>
        where D : DALSQLServerID<M>
        where M : AbsModel_ID
    {
        public BLLSQLServerID(D dal) : base(dal) { }

        /// <summary>
        /// 插入数据
        /// </summary>
        /// <param name="model">需要添加的数据模型</param>
        /// <param name="id">返回的记录ID</param>
        /// <returns></returns>
        public bool Insert(M model, out int id) {
            return base.SelfDAL.Insert(model, out id);
        }
    }
    /// <summary>
    /// Microsoft SQL Server 2008 版本数据库 数据访问器 ID版本
    /// </summary>
    /// <typeparam name="M">数据访问模型ID版本</typeparam>
    public class DALSQLServerID<M> : DALSQLServer<M> where M : AbsModel_ID
    {
        public DALSQLServerID() : base() { }

        /// <summary>
        /// 插入数据
        /// </summary>
        /// <param name="model">需要添加的数据模型</param>
        /// <param name="id">返回的记录ID</param>
        /// <returns></returns>
        public bool Insert(M model, out int id) {
            const int errorID = 0;
            id = errorID;
            string sqlinsert = SQLInsert(model, true);
            if (CheckData.IsStringNull(sqlinsert)) {
                return false;
            }
            object obj = DbHelperSQL.GetSingle(sqlinsert);
            id = CheckData.IsObjectNull(obj) ? errorID : ConvertTool.ObjToInt(obj, errorID);
            return id != errorID;
        }
    }
}