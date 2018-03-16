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

        /// <summary>
        /// 获取主键列
        /// </summary>
        /// <returns>若无,返回null</returns>
        public ColumnItemModel PrimaryKeyColumn() {
            foreach (ColumnItemModel item in this.modelParser.ColumnInfoArray) {
                if (item.Attribute.IsPrimaryKey)
                    return item;
            }
            return null;
        }

        /// <summary>
        /// 获取主键并且是ID标识列
        /// </summary>
        /// <returns>若无,返回null</returns>
        public ColumnItemModel IDentityColumn() {
            foreach (ColumnItemModel item in this.modelParser.ColumnInfoArray) {
                if (item.Attribute.IsPrimaryKey && item.Attribute.IsIDentity)
                    return item;
            }
            return null;
        }

        /// <summary>
        /// 获取能执行插入语句的列
        /// </summary>
        public ColumnItemModel[] CanGetSetColumns() {
            return this.modelParser.ColumnInfoArray.Where(colinfo => !colinfo.Attribute.IsDbGenerated).ToArray();
        }

        #region === IAutoTable ===
        public string SQLCreateTable() {
            Dictionary<string, string> columns = GetCreateColumns();
            string sql = SQLif(NotExists(SQLSelectTable()), SQLCreateTable(columns), SQLAlterColumns(columns));
            return sql;
        }
        public string SQLClearTable() {
            return string.Format("truncate table {0}", GetTableName());
        }
        public string SQLKillTable() {
            return string.Format("drop table {0}", GetTableName());
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
        /// <summary>
        /// 创建 SQL if 语句
        /// </summary>
        /// <param name="whereExpression">条件表达式, 必填</param>
        /// <param name="trueCode">true 代码执行体, 必填</param>
        /// <param name="falseCode">false 代码执行体, 选填</param>
        /// <returns></returns>
        private string SQLif(string whereExpression, string trueCode, string falseCode) {
            if (CheckData.IsStringNull(whereExpression.Trim()) || CheckData.IsStringNull(trueCode.Trim()))
                return string.Empty;
            string sql = string.Format("if {0} begin {1} end", whereExpression, trueCode);
            if (!CheckData.IsStringNull(falseCode.Trim())) {
                sql += string.Format(" else begin {0} end", falseCode);
            }
            return sql;
        }
        private string NotExists(string where) {
            return string.Format("not exists({0})", where);
        }
        private string SQLSelectTable() {
            return string.Format("select top 1 object_id from sys.tables where name = '{0}'", GetTableName());
        }
        private string SQLCreateTable(Dictionary<string, string> columns) {
            string[] tvalues = ConvertTool.ListConvertType(columns, d => d.Value);
            string columnFormats = ConvertTool.IListToString(tvalues, ',');
            return string.Format("Create Table {0} ({1})", GetTableName(), columnFormats);
        }
        private string SQLAlterColumns(Dictionary<string, string> columns) {
            List<string> ifExists = new List<string>();
            foreach (KeyValuePair<string, string> item in columns) {
                string sql = SQLif(NotExists(SQLSelectColumn(item.Key)), SQLAlterAddColumn(item.Value), string.Empty);
                ifExists.Add(sql);
            }
            return ConvertTool.IListToString(ifExists, " ");
        }
        private string SQLSelectColumn(string columnName) {
            return string.Format("select * from sys.columns where name = '{0}' and object_id = ({1})", columnName, SQLSelectTable());
        }
        private string SQLAlterAddColumn(string columnFormat) {
            return string.Format("ALTER TABLE {0} ADD {1}", GetTableName(), columnFormat);
        }
        #endregion

        #region SQL WhereModel Parser
        public string WhereModelParserToSQLString(WhereModel wheres) {
            return string.Empty;
        }
        #endregion

        #region === IBasicsSQL<M> ===
        /// <summary>
        /// 产生 插入 SQL
        /// </summary>
        public virtual string SQLInsert(M model) {
            List<string> fieldArr = new List<string>();
            List<string> valueArr = new List<string>();

            foreach (ColumnItemModel item in this.CanGetSetColumns()) {
                KeyValueModel im = this.modelParser.ExtractValue(item, model);
                if (CheckData.IsObjectNull(im))
                    continue;
                fieldArr.Add(im.Key);
                valueArr.Add(string.Format("'{0}'", im.Value));
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
        public virtual string SQLDelete(WhereModel wheres) {
            return string.Format("delete {0} where {1}", base.GetTableName(), WhereModelParserToSQLString(wheres));
        }

        /// <summary>
        /// 产生 更新 SQL 异常: CreateSQLNotHaveWhereException
        /// </summary>
        public virtual string SQLUpdate(M model) {
            List<string> setArr = new List<string>();
            foreach (ColumnItemModel item in this.CanGetSetColumns()) {
                KeyValueModel im = this.modelParser.ExtractValue(item, model);
                if (CheckData.IsObjectNull(im))
                    continue;
                setArr.Add(string.Format("{0} = '{1}'", im.Key, im.Value));
            }

            if (CheckData.IsSizeEmpty(setArr)) {
                return string.Empty;
            }
            string setStr = ConvertTool.IListToString(setArr, ',');
            return string.Format("update {0} set {1} where {2}", model.GetTableName(), setStr, CreateSignSQLWhere(model));
        }

        /// <summary>
        /// 创建 标识准确记录的 SQL where 条件部分字符串
        /// </summary>
        /// <param name="model">数据来源</param>
        public virtual string CreateSignSQLWhere(M model) {
            ColumnItemModel colmodel = null;
            colmodel = !CheckData.IsObjectNull(colmodel) ? colmodel : PrimaryKeyColumn();
            colmodel = !CheckData.IsObjectNull(colmodel) ? colmodel : IDentityColumn();
            if (CheckData.IsObjectNull(colmodel)) {
                throw new CreateSQLNotHaveWhereException();
            }
            KeyValueModel nowVal = this.modelParser.ExtractValue(colmodel, model);
            KeyValueModel defVal = this.modelParser.ExtractValue(colmodel, CreateDefaultModel());
            if (nowVal.IsObjectNull() || nowVal.Equals(defVal) || nowVal.ToString().Trim().IsStringNull()) {
                throw new CreateSQLNotHaveWhereException();
            }
            string where = string.Format("{0} = '{1}'", nowVal.Key, nowVal.Value);
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

        //public virtual bool Insert(M model, out int IDentity) {
        //}

        //public virtual bool Delete(M model) {
        //    return DbHelperSQL.ExecuteSql(SQLDelete(model)) > 0;
        //}

        //public virtual bool Update(M model) {
        //    return DbHelperSQL.ExecuteSql(SQLUpdate(model)) > 0;
        //}

        //public virtual M GetModel(int IDentity) {
        //    ColumnItemModel colmodel = IDentityColumn();
        //    if (CheckData.IsObjectNull(colmodel))
        //        return null;
        //    string sql = string.Format("select top 1 * from {0} where {1} = {2}",
        //        GetTableName(),
        //        colmodel.Property.Name,
        //        IDentity);
        //    DataSet ds = DbHelperSQL.Query(sql);
        //    return CheckReturnModel(ds);
        //}
        //public virtual DataTable GetList(int top = 0, string strWhere = "", Dictionary<string, bool> fieldOrders = null) {
        //    string orderbyStr = AnalysisDictionaryOrderByInfos(fieldOrders);
        //    string selectStr = SQLALLSelectWhere(top, strWhere, orderbyStr);
        //    DataSet ds = DbHelperSQL.Query(selectStr);
        //    return CheckReturnDataTable(ds);
        //}
        ///// <summary>
        ///// 分页查询
        ///// </summary>
        ///// <param name="pageCount">定义: 每页记录数</param>
        ///// <param name="pageIndex">定义: 浏览到第几页</param>
        ///// <param name="recordCount">得到: 总记录数</param>
        ///// <param name="strWhere">定义: 查询条件</param>
        ///// <param name="fieldOrders">定义: 字段排序集合, true 为正序, false 倒序</param>
        ///// <returns>结果数据表</returns>
        //public DataTable GetList(int pageCount, int pageIndex, out int recordCount, string strWhere, Dictionary<string, bool> fieldOrders) {
        //    string orderbyStr = AnalysisDictionaryOrderByInfos(fieldOrders);
        //    string selectStr = SQLALLSelectWhere(0, strWhere, orderbyStr);
        //    recordCount = GetRecordCount(selectStr);
        //    DataSet ds = DbHelperSQL.Query(PagingHelper.CreatePagingSql(recordCount, pageCount, pageIndex, selectStr, orderbyStr));
        //    return CheckReturnDataTable(ds);
        //}

        #region === ITableBasicFunction<M> ===
        public virtual int GetRecordCount(string strWhere) {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(*) as H from " + GetTableName());
            if (!CheckData.IsStringNull(strWhere.Trim())) {
                strSql.Append(" where " + strWhere);
            }
            return ConvertTool.ObjToInt(DbHelperSQL.GetSingle(strSql.ToString()), 0);
        }

        public virtual M DataRowToModel(DataRow row) {
            if (CheckData.IsSizeEmpty(row))
                return null;
            M model = CreateDefaultModel();
            foreach (ColumnItemModel item in this.modelParser.ColumnInfoArray) {
                if (CheckData.IsObjectNull(item)) {
                    continue;
                }
                object value = row[item.Property.Name];
                model = this.modelParser.FillValue(item, model, value);
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

        internal string SQLALLSelectWhere(int top, string strWhere, string orderBy) {
            string column = top > 0 ? string.Format(@"top {0} *", top) : @"*";
            string sql = string.Format(@"select {0} from {1}", column, GetTableName());
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
            if (!FieldValueModel.CheckIsCanUse(fielvals) || !WhereModel.CheckIsCanUse(wheres)) {
                return false;
            }

            //List<string> setArr = new List<string>();
            //foreach (ColumnItemModel item in this.CanGetSetColumns()) {
            //    KeyValueModel im = this.modelParser.ExtractValue(item, model);
            //    if (CheckData.IsObjectNull(im))
            //        continue;
            //    setArr.Add(string.Format("{0} = '{1}'", im.Key, im.Value));
            //}

            //if (CheckData.IsSizeEmpty(setArr)) {
            //    return string.Empty;
            //}
            //string setStr = ConvertTool.IListToString(setArr, ',');
            //return string.Format("update {0} set {1} where {2}", model.GetTableName(), setStr, CreateSignSQLWhere(model));
            return false;
        }

        public override M[] Select(int top = 0, WhereModel wheres = null, FieldOrderModel[] fieldOrders = null) {
            return new M[] { };
        }
    }
}