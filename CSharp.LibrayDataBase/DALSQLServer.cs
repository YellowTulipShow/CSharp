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
    public class BLLSQLServer<M> : AbsBLL<DALSQLServer<M>, M>
        //, IDefaultRecord<M>
        where M : AbsModelNull
    {
        public BLLSQLServer(DALSQLServer<M> dal) : base(dal) { }

        //public static bool Transaction(string[] sqllist) {
        //    return DALSQLServer<M>.Transaction(sqllist);
        //}
        //#region === IBasicsSQL<M> ===
        //public string SQLInsert(M model) {
        //    return TableDAL.SQLInsert(model);
        //}

        //public string SQLDelete(M model) {
        //    return TableDAL.SQLDelete(model);
        //}

        //public string SQLUpdate(M model) {
        //    return TableDAL.SQLUpdate(model);
        //}
        //#endregion

        //#region === ITableBasicFunction<M> ===
        //public int GetRecordCount(string strWhere) {
        //    return TableDAL.GetRecordCount(strWhere);
        //}

        //public bool Insert(M model, out int IDentity) {
        //    return TableDAL.Insert(model, out IDentity);
        //}

        //public bool Delete(M model) {
        //    return TableDAL.Delete(model);
        //}

        //public bool Update(M model) {
        //    return TableDAL.Update(model);
        //}

        //public M GetModel(int IDentity) {
        //    return TableDAL.GetModel(IDentity);
        //}

        //public M DataRowToModel(DataRow row) {
        //    return TableDAL.DataRowToModel(row);
        //}

        //public M[] GetModelList(DataTable dt) {
        //    return TableDAL.GetModelList(dt);
        //}

        //public DataTable GetList(int top = 0, string strWhere = "", Dictionary<string, bool> fieldOrders = null) {
        //    DataTable dt = TableDAL.GetList(top, strWhere, fieldOrders);
        //    if (CheckData.IsSizeEmpty(dt)) {
        //        KeepDataNotBlank();
        //        dt = TableDAL.GetList(top, strWhere, fieldOrders);
        //    }
        //    return dt;
        //}

        //public DataTable GetList(int pageCount, int pageIndex, out int recordCount, string strWhere, Dictionary<string, bool> fieldOrders) {
        //    DataTable dt = TableDAL.GetList(pageCount, pageIndex, out recordCount, strWhere, fieldOrders);
        //    if (CheckData.IsSizeEmpty(dt)) {
        //        KeepDataNotBlank();
        //        dt = TableDAL.GetList(pageCount, pageIndex, out recordCount, strWhere, fieldOrders);
        //    }
        //    return dt;
        //}
        //#endregion

        ///// <summary>
        ///// 保持数据不空白
        ///// </summary>
        //private void KeepDataNotBlank() {
        //    M model = DefaultDataModel();
        //    if (CheckData.IsObjectNull(model))
        //        return;
        //    int count = GetRecordCount(string.Empty);
        //    if (count <= 0) {
        //        int id = 0;
        //        Insert(model, out id);
        //    }
        //}

        ///// <summary>
        ///// 默认数据模型
        ///// </summary>
        //public virtual M DefaultDataModel() {
        //    return null;
        //}
    }

    /// <summary>
    /// Microsoft SQL Server 2008 版本数据库 数据访问器
    /// </summary>
    /// <typeparam name="M">数据访问模型</typeparam>
    public class DALSQLServer<M> : AbsDAL<M>
        where M : AbsModelNull
    {
        public DALSQLServer()
            : base() {
            EXECreateTable();
        }

        /// <summary>
        /// 执行创建数据表
        /// </summary>
        private void EXECreateTable() {
            DbHelperSQL.GetSingle(SQLCreateTable());
        }

        /// <summary>
        /// 执行-SQL字符串事务处理
        /// </summary>
        /// <param name="sqllist">SQL字符串列表</param>
        /// <returns>是否成功</returns>
        public static bool Transaction(string[] sqllist) {
            if (CheckData.IsSizeEmpty(sqllist))
                return false;
            bool resu = DbHelperSQL.ExecuteTransaction(sqllist);
            return resu;
        }

        private string SQLCreateTable() {
            string str_if_where = CreateSQL.NotExists(CreateSQL.MSSSysTable(GetTableName()));

            Dictionary<string, string> columns = GetCreateColumns();
            string[] column_formats = ConvertTool.ListConvertType(columns, d => d.Value);
            string SQL_CreateTable = CreateSQL.CreateTable(GetTableName(), column_formats);
            string SQL_AlterColumns = SQLAlterColumns(columns);

            string sql = CreateSQL.If(str_if_where, SQL_CreateTable, SQL_AlterColumns);
            return sql;
        }
        private Dictionary<string, string> GetCreateColumns() {
            Dictionary<string, string> resuDic = new Dictionary<string, string>();
            foreach (ColumnItemModel item in base.modelParser.ColumnInfoArray) {
                string datafieldName = item.Property.Name;
                string datatypeName = item.Attribute.DbType.FieldTypeName();
                string value = string.Format("{0} {1}", datafieldName, datatypeName);
                if (item.Attribute.IsPrimaryKey)
                    value += @" primary key";
                if (!item.Attribute.IsCanBeNull)
                    value += @" not null";
                if (item.Attribute.IsIDentity)
                    value += @" identity (1,1)";
                resuDic[item.Property.Name] = value;
            }
            return resuDic;
        }
        private string SQLAlterColumns(Dictionary<string, string> columns) {
            List<string> ifExists = new List<string>();
            foreach (KeyValuePair<string, string> item in columns) {
                string if_where = CreateSQL.NotExists(CreateSQL.MSSSysColumns(GetTableName(), item.Key));
                string sql = CreateSQL.If(if_where, CreateSQL.AlterColumn(GetTableName(), item.Value));
                ifExists.Add(sql);
            }
            return ConvertTool.IListToString(ifExists, " ");
        }


        private string SQLInsert(M model) {
            ColumnItemModel[] caninsertColumns = base.modelParser.ColumnInfoArray.Where(colinfo => {
                return !colinfo.Attribute.IsDbGenerated;
            }).ToArray(); // 排除那些由数据库系统自动生成的数据列, 来执行插入操作

            List<string> fieldArr = new List<string>();
            List<string> valueArr = new List<string>();
            foreach (ColumnItemModel item in caninsertColumns) {
                KeyValueModel im = base.modelParser.ExtractValue(item, model);
                if (CheckData.IsObjectNull(im) || CheckData.IsStringNull(im.Key))
                    continue;
                fieldArr.Add(im.Key);
                valueArr.Add(string.Format("'{0}'", im.Value));
            }
            if ((fieldArr.Count != valueArr.Count) || CheckData.IsSizeEmpty(fieldArr)) {
                return string.Empty;
            }
            return CreateSQL.Insert(base.GetTableName(), fieldArr.ToArray(), valueArr.ToArray());
        }
        private string SQLDelete(WhereModel wheres) {
            return string.Format("delete {0} where {1}", base.GetTableName(), CreateSQL.ParserWhereModel(wheres));
        }
        private string SQLUpdate(FieldValueModel[] fielvals, WhereModel wheres) {
            string set_str = ConvertTool.IListToString(CreateSQL.ParserFieldValueModel(fielvals, DataChar.OperChar.EQUAL), ',');
            string where_str = CreateSQL.ParserWhereModel(wheres);
            return string.Format("update {0} set {1} where {2}", base.GetTableName(), set_str, where_str);
        }
        private string SQLSelect(int top, string strWhere, string orderBy) {
            string column = top > 0 ? string.Format(@"top {0} *", top) : @"*";
            string sql = string.Format(@"select {0} from {1}", column, GetTableName());
            if (!CheckData.IsStringNull(strWhere.Trim()))
                sql += string.Format(@" where {0}", strWhere);
            if (!CheckData.IsStringNull(orderBy.Trim()))
                sql += string.Format(@" order by {0}", orderBy);
            return sql;
        }

        public override bool Insert(M model) {
            const int errorID = 0;
            string sqlinsert = SQLInsert(model);
            if (CheckData.IsStringNull(sqlinsert.Trim()))
                return false;
            string strSql = sqlinsert + " ;select @@IDENTITY; ";
            object obj = DbHelperSQL.GetSingle(strSql);
            int IDentity = CheckData.IsObjectNull(obj) ? errorID : ConvertTool.ObjToInt(obj, errorID);
            return IDentity != errorID;
        }
        public override bool Delete(WhereModel wheres) {
            return !WhereModel.CheckIsCanUse(wheres) ? false : DbHelperSQL.ExecuteSql(SQLDelete(wheres)) > 0;
        }
        public override bool Update(FieldValueModel[] fielvals, WhereModel wheres) {
            return (!FieldValueModel.CheckIsCanUse(fielvals) || !WhereModel.CheckIsCanUse(wheres)) ? false :
                DbHelperSQL.ExecuteSql(SQLUpdate(fielvals, wheres)) > 0;
        }
        public override M[] Select(int top = 0, WhereModel wheres = null, FieldOrderModel[] fieldOrders = null) {
            DataTable dt = SelectSource(top, wheres, fieldOrders);
            return GetModelList(dt);
        }

        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="pageCount">定义: 每页记录数</param>
        /// <param name="pageIndex">定义: 浏览到第几页</param>
        /// <param name="recordCount">得到: 总记录数</param>
        /// <param name="wheres">定义: 查询条件</param>
        /// <param name="fieldOrders">定义: 字段排序集合, true 为正序, false 倒序</param>
        /// <returns>结果数据表</returns>
        public M[] Select(int pageCount, int pageIndex, out int recordCount, WhereModel wheres = null, FieldOrderModel[] fieldOrders = null) {
            DataTable dt = SelectSource(pageCount, pageIndex, out recordCount, wheres, fieldOrders);
            return GetModelList(dt);
        }
        public DataTable SelectSource(int top = 0, WhereModel wheres = null, FieldOrderModel[] fieldOrders = null) {
            string strWhere = CreateSQL.ParserWhereModel(wheres);
            string orderbyStr = CreateSQL.ParserFieldOrderModel(fieldOrders);
            string selectStr = SQLSelect(top, strWhere, orderbyStr);
            DataSet ds = DbHelperSQL.Query(selectStr);
            return CheckReturnDataTable(ds);
        }
        private DataTable SelectSource(int pageCount, int pageIndex, out int recordCount, WhereModel wheres = null, FieldOrderModel[] fieldOrders = null) {
            string strWhere = CreateSQL.ParserWhereModel(wheres);
            string orderbyStr = CreateSQL.ParserFieldOrderModel(fieldOrders);
            string selectStr = SQLSelect(0, strWhere, orderbyStr);
            recordCount = GetRecordCount(selectStr);
            DataSet ds = DbHelperSQL.Query(PagingHelper.CreatePagingSql(recordCount, pageCount, pageIndex, selectStr, orderbyStr));
            return CheckReturnDataTable(ds);
        }

        #region === tools method ===
        private DataTable CheckReturnDataTable(DataSet ds) {
            return CheckData.IsSizeEmpty(ds) ? null : CheckData.IsSizeEmpty(ds.Tables[0]) ? null : ds.Tables[0];
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
                model = base.modelParser.FillValue(item, model, value);
            }
            return model;
        }
        #endregion

        public int GetRecordCount(WhereModel wheres) {
            return GetRecordCount(CreateSQL.ParserWhereModel(wheres));
        }
        private int GetRecordCount(string wheresql) {
            StringBuilder strSql = new StringBuilder();
            strSql.AppendFormat("select count(*) as H from {0}", base.GetTableName());
            if (!CheckData.IsStringNull(wheresql.Trim())) {
                strSql.Append(" where " + wheresql);
            }
            return ConvertTool.ObjToInt(DbHelperSQL.GetSingle(strSql.ToString()), 0);
        }
    }
}