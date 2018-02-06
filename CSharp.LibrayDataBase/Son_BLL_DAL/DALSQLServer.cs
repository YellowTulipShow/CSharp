using System;
using System.Data;
using System.Text;
using System.Collections.Generic;
using CSharp.LibrayDataBase.Utils;
using CSharp.LibrayFunction;
using System.Reflection;

namespace CSharp.LibrayDataBase
{
    /// <summary>
    /// Microsoft SQL Server 2008 版本数据库 数据访问器
    /// </summary>
    /// <typeparam name="M">数据访问模型</typeparam>
    public class DALSQLServer<M> : AbsTableDAL<M> where M : AbsModel_Null
    {
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
        /// 分页查询
        /// </summary>
        /// <param name="pageCount">定义: 每页记录数</param>
        /// <param name="pageIndex">定义: 浏览到第几页</param>
        /// <param name="recordCount">得到: 总记录数</param>
        /// <param name="strWhere">定义: 查询条件</param>
        /// <param name="fieldOrders">定义: 字段排序集合, true 为正序, false 倒序</param>
        /// <returns>结果数据表</returns>
        public override DataTable GetList(int pageCount, int pageIndex, out int recordCount, string strWhere, Dictionary<string, bool> fieldOrders) {
            string orderbyStr = AnalysisDictionaryOrderByInfos(fieldOrders);
            string selectStr = SQLALLSelectWhere(0, strWhere, orderbyStr);
            recordCount = GetRecordCount(selectStr);
            DataSet ds = DbHelperSQL.Query(PagingHelper.CreatePagingSql(recordCount, pageCount, pageIndex, selectStr, orderbyStr));
            return CheckReturnDataTable(ds);
        }

        #region === IAutoTable ===
        public override string SQLCreateTable() {
            Dictionary<string, string> columns = GetCreateColumns();
            string sql = SQLif(NotExists(SQLSelectTable()), SQLCreateTable(columns), SQLAlterColumns(columns));
            return sql;
        }
        public override string SQLClearTable() {
            return string.Format("truncate table {0}", GetTableName());
        }
        public override string SQLKillTable() {
            return string.Format("drop table {0}", GetTableName());
        }
        private Dictionary<string, string> GetCreateColumns() {
            Dictionary<string, string> resuDic = new Dictionary<string, string>();
            foreach (ColumnInfo item in GetALLTypeColumns()) {
                string value = string.Format("{0} {1}", item.Property.Name, item.Attribute.DbType.FieldTypeName());
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
    }
}