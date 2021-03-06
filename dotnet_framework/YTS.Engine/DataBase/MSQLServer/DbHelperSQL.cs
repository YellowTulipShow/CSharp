﻿using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Collections.Generic;
using YTS.Tools;

namespace YTS.Engine.DataBase.MSQLServer
{
    public abstract class DbHelperSQL
    {
        /// <summary>
        /// 数据库连接字符串(*.config应用程序配置文件来配置) (对应配置文件节点名称同名)
        /// </summary>
        public static string ConnectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

        public DbHelperSQL() { }

        #region 公用方法
        /// <summary>
        /// 判断是否存在某表的某个字段
        /// </summary>
        /// <param name="tableName">表名称</param>
        /// <param name="columnName">列名称</param>
        /// <returns>是否存在</returns>
        public static bool ColumnExists(string tableName, string columnName) {
            string sql = "select count(1) from syscolumns where [id]=object_id('" + tableName + "') and [name]='" + columnName + "'";
            object res = GetSingle(sql);
            if (res == null) {
                return false;
            }
            return Convert.ToInt32(res) > 0;
        }
        public static int GetMinID(string FieldName, string TableName) {
            string strsql = "select min(" + FieldName + ") from " + TableName;
            object obj = DbHelperSQL.GetSingle(strsql);
            if (obj == null) {
                return 0;
            } else {
                return int.Parse(obj.ToString());
            }
        }
        public static int GetMaxID(string FieldName, string TableName) {
            string strsql = "select max(" + FieldName + ")+1 from " + TableName;
            object obj = DbHelperSQL.GetSingle(strsql);
            if (obj == null) {
                return 1;
            } else {
                return int.Parse(obj.ToString());
            }
        }
        public static int GetMaxID(string FieldName, string strWhere, string TableName) {
            string strsql = "select max(" + FieldName + ")+1 from " + TableName;
            if (strWhere.Trim() != "") {
                strsql += (" where " + strWhere);
            }
            object obj = DbHelperSQL.GetSingle(strsql);
            if (obj == null) {
                return 1;
            } else {
                return int.Parse(obj.ToString());
            }
        }
        public static int GetMaxNumID(string FieldName, string strWhere, string TableName) {
            string strsql = "select max(" + FieldName + ") from " + TableName;
            if (strWhere.Trim() != "") {
                strsql += (" where " + strWhere);
            }
            object obj = DbHelperSQL.GetSingle(strsql);
            if (obj == null) {
                return 1;
            } else {
                return int.Parse(obj.ToString());
            }
        }
        public static bool Exists(string strSql) {
            object obj = DbHelperSQL.GetSingle(strSql);
            int cmdresult;
            if (IsObjectNull(obj)) {
                cmdresult = 0;
            } else {
                cmdresult = int.Parse(obj.ToString());
            }
            if (cmdresult == 0) {
                return false;
            } else {
                return true;
            }
        }
        /// <summary>
        /// 表是否存在
        /// </summary>
        /// <param name="TableName"></param>
        /// <returns></returns>
        public static bool TabExists(string TableName) {
            string strsql = "select count(*) from sysobjects where id = object_id(N'[" + TableName + "]') and OBJECTPROPERTY(id, N'IsUserTable') = 1";
            //string strsql = "SELECT count(*) FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[" + TableName + "]') AND type in (N'U')";
            object obj = DbHelperSQL.GetSingle(strsql);
            int cmdresult;
            if (IsObjectNull(obj)) {
                cmdresult = 0;
            } else {
                cmdresult = int.Parse(obj.ToString());
            }
            if (cmdresult == 0) {
                return false;
            } else {
                return true;
            }
        }
        public static bool Exists(string strSql, params SqlParameter[] cmdParms) {
            object obj = DbHelperSQL.GetSingle(strSql, cmdParms);
            int cmdresult;
            if (IsObjectNull(obj)) {
                cmdresult = 0;
            } else {
                cmdresult = int.Parse(obj.ToString());
            }
            if (cmdresult == 0) {
                return false;
            } else {
                return true;
            }
        }
        #endregion

        #region  执行简单SQL语句
        /// <summary>
        /// 执行查询语句，返回SqlDataReader ( 注意：调用该方法后，一定要对SqlDataReader进行Close )
        /// </summary>
        /// <param name="strSQL">查询语句</param>
        /// <returns>SqlDataReader</returns>
        public static SqlDataReader ExecuteReader(string strSQL) {
            SqlConnection connection = new SqlConnection(ConnectionString);
            SqlCommand cmd = new SqlCommand(strSQL, connection);
            try {
                connection.Open();
                SqlDataReader myReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                return myReader;
            } catch (System.Data.SqlClient.SqlException e) {
                throw e;
            }

        }
        /// <summary>
        /// SqlBulkCopy类批量复制大数据
        /// </summary>
        /// <param name="connectionString">目标连接字符</param>
        /// <param name="TableName">目标表</param>
        /// <param name="dt">源数据</param>
        public static bool SqlBulkCopyByDatatable(string TableName, DataTable dt) {
            using (SqlConnection conn = new SqlConnection(ConnectionString)) {
                using (SqlBulkCopy sqlbulkcopy = new SqlBulkCopy(ConnectionString, SqlBulkCopyOptions.UseInternalTransaction)) {
                    try {
                        sqlbulkcopy.DestinationTableName = TableName;
                        for (int i = 0; i < dt.Columns.Count; i++) {
                            sqlbulkcopy.ColumnMappings.Add(dt.Columns[i].ColumnName, dt.Columns[i].ColumnName);
                        }
                        sqlbulkcopy.WriteToServer(dt);
                        return true;
                    } catch (Exception) {
                        return false;
                    }
                }
            }
        }
        #endregion

        #region 执行带参数的SQL语句
        /// <summary>
        /// 2012-2-21新增重载，执行一条计算查询结果语句，返回查询结果（object）。
        /// </summary>
        /// <param name="connection">SqlConnection对象</param>
        /// <param name="trans">SqlTransaction事务</param>
        /// <param name="SQLString">计算查询结果语句</param>
        /// <returns>查询结果（object）</returns>
        public static object GetSingle(SqlConnection connection, SqlTransaction trans, string SQLString, params SqlParameter[] cmdParms) {
            using (SqlCommand cmd = new SqlCommand()) {
                try {
                    PrepareCommand(cmd, connection, trans, SQLString, cmdParms);
                    object obj = cmd.ExecuteScalar();
                    cmd.Parameters.Clear();
                    return CheckSetObjectNull(obj);
                } catch (System.Data.SqlClient.SqlException e) {
                    //trans.Rollback();
                    throw e;
                }
            }
        }

        /// <summary>
        /// 执行查询语句，返回SqlDataReader ( 注意：调用该方法后，一定要对SqlDataReader进行Close )
        /// </summary>
        /// <param name="strSQL">查询语句</param>
        /// <returns>SqlDataReader</returns>
        public static SqlDataReader ExecuteReader(string SQLString, params SqlParameter[] cmdParms) {
            SqlConnection connection = new SqlConnection(ConnectionString);
            SqlCommand cmd = new SqlCommand();
            try {
                PrepareCommand(cmd, connection, null, SQLString, cmdParms);
                SqlDataReader myReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                cmd.Parameters.Clear();
                return myReader;
            } catch (System.Data.SqlClient.SqlException e) {
                throw e;
            }
            //          finally
            //          {
            //              cmd.Dispose();
            //              connection.Close();
            //          }

        }

        private static void PrepareCommand(SqlCommand cmd, SqlConnection conn, SqlTransaction trans, string cmdText, SqlParameter[] cmdParms) {
            if (conn.State != ConnectionState.Open)
                conn.Open();
            cmd.Connection = conn;
            cmd.CommandText = cmdText;
            if (trans != null)
                cmd.Transaction = trans;
            cmd.CommandType = CommandType.Text;//cmdType;
            if (cmdParms != null) {
                foreach (SqlParameter parameter in cmdParms) {
                    if ((parameter.Direction == ParameterDirection.InputOutput || parameter.Direction == ParameterDirection.Input) &&
                        (parameter.Value == null)) {
                        parameter.Value = DBNull.Value;
                    }
                    cmd.Parameters.Add(parameter);
                }
            }
        }

        #endregion

        #region 存储过程操作

        /// <summary>
        /// 执行存储过程，返回SqlDataReader ( 注意：调用该方法后，一定要对SqlDataReader进行Close )
        /// </summary>
        /// <param name="storedProcName">存储过程名</param>
        /// <param name="parameters">存储过程参数</param>
        /// <returns>SqlDataReader</returns>
        public static SqlDataReader RunProcedure(string storedProcName, IDataParameter[] parameters) {
            SqlConnection connection = new SqlConnection(ConnectionString);
            SqlDataReader returnReader;
            connection.Open();
            SqlCommand command = BuildQueryCommand(connection, storedProcName, parameters);
            command.CommandType = CommandType.StoredProcedure;
            returnReader = command.ExecuteReader(CommandBehavior.CloseConnection);
            return returnReader;

        }

        /// <summary>
        /// 执行存储过程
        /// </summary>
        /// <param name="storedProcName">存储过程名</param>
        /// <param name="parameters">存储过程参数</param>
        /// <param name="tableName">DataSet结果中的表名</param>
        /// <returns>DataSet</returns>
        public static DataSet RunProcedure(string storedProcName, IDataParameter[] parameters, string tableName) {
            using (SqlConnection connection = new SqlConnection(ConnectionString)) {
                DataSet dataSet = new DataSet();
                connection.Open();
                SqlDataAdapter sqlDA = new SqlDataAdapter();
                sqlDA.SelectCommand = BuildQueryCommand(connection, storedProcName, parameters);
                sqlDA.Fill(dataSet, tableName);
                connection.Close();
                return dataSet;
            }
        }
        public static DataSet RunProcedure(string storedProcName, IDataParameter[] parameters, string tableName, int Times) {
            using (SqlConnection connection = new SqlConnection(ConnectionString)) {
                DataSet dataSet = new DataSet();
                connection.Open();
                SqlDataAdapter sqlDA = new SqlDataAdapter();
                sqlDA.SelectCommand = BuildQueryCommand(connection, storedProcName, parameters);
                sqlDA.SelectCommand.CommandTimeout = Times;
                sqlDA.Fill(dataSet, tableName);
                connection.Close();
                return dataSet;
            }
        }

        /// <summary>
        /// 构建 SqlCommand 对象(用来返回一个结果集，而不是一个整数值)
        /// </summary>
        /// <param name="connection">数据库连接</param>
        /// <param name="storedProcName">存储过程名</param>
        /// <param name="parameters">存储过程参数</param>
        /// <returns>SqlCommand</returns>
        private static SqlCommand BuildQueryCommand(SqlConnection connection, string storedProcName, IDataParameter[] parameters) {
            SqlCommand command = new SqlCommand(storedProcName, connection);
            command.CommandType = CommandType.StoredProcedure;
            foreach (SqlParameter parameter in parameters) {
                if (parameter != null) {
                    // 检查未分配值的输出参数,将其分配以DBNull.Value.
                    if ((parameter.Direction == ParameterDirection.InputOutput || parameter.Direction == ParameterDirection.Input) &&
                        (parameter.Value == null)) {
                        parameter.Value = DBNull.Value;
                    }
                    command.Parameters.Add(parameter);
                }
            }

            return command;
        }

        /// <summary>
        /// 执行存储过程，返回影响的行数
        /// </summary>
        /// <param name="storedProcName">存储过程名</param>
        /// <param name="parameters">存储过程参数</param>
        /// <param name="rowsAffected">影响的行数</param>
        /// <returns></returns>
        public static int RunProcedure(string storedProcName, IDataParameter[] parameters, out int rowsAffected) {
            using (SqlConnection connection = new SqlConnection(ConnectionString)) {
                int result;
                connection.Open();
                SqlCommand command = BuildIntCommand(connection, storedProcName, parameters);
                rowsAffected = command.ExecuteNonQuery();
                result = (int)command.Parameters["ReturnValue"].Value;
                //Connection.Close();
                return result;
            }
        }

        /// <summary>
        /// 创建 SqlCommand 对象实例(用来返回一个整数值)
        /// </summary>
        /// <param name="storedProcName">存储过程名</param>
        /// <param name="parameters">存储过程参数</param>
        /// <returns>SqlCommand 对象实例</returns>
        private static SqlCommand BuildIntCommand(SqlConnection connection, string storedProcName, IDataParameter[] parameters) {
            SqlCommand command = BuildQueryCommand(connection, storedProcName, parameters);
            command.Parameters.Add(new SqlParameter("ReturnValue",
                SqlDbType.Int, 4, ParameterDirection.ReturnValue,
                false, 0, 0, string.Empty, DataRowVersion.Default, null));
            return command;
        }
        #endregion

        #region Tools Code
        private static bool IsObjectNull(object obj) {
            if ((Object.Equals(obj, null)) || (Object.Equals(obj, System.DBNull.Value))) {
                return true;
            } else {
                return false;
            }
        }
        private static object CheckSetObjectNull(object obj) {
            if (IsObjectNull(obj)) {
                return null;
            } else {
                return obj;
            }
        }

        #endregion

        #region Custom Code
        /// <summary>
        /// 执行一条计算查询结果语句，返回查询结果（object）。默认超时时间30s
        /// </summary>
        /// <param name="SQLString">计算查询结果语句</param>
        /// <returns>查询结果（object）</returns>
        public static object GetSingle(string SQLString) {
            return GetSingle(SQLString, 30);
        }
        /// <summary>
        /// 执行一条计算查询结果语句，返回查询结果（object）。
        /// </summary>
        /// <param name="SQLString">计算查询结果语句</param>
        /// <param name="Times">超时时间</param>
        /// <returns>查询结果（object）</returns>
        public static object GetSingle(string SQLString, int Times) {
            using (SqlConnection connection = new SqlConnection(ConnectionString)) {
                using (SqlCommand cmd = new SqlCommand(SQLString, connection)) {
                    try {
                        connection.Open();
                        cmd.CommandTimeout = Times;
                        object obj = cmd.ExecuteScalar();
                        return CheckSetObjectNull(obj);
                    } catch (System.Data.SqlClient.SqlException e) {
                        connection.Close();
                        throw e;
                    }
                }
            }
        }

        /// <summary>
        /// 执行一条计算查询结果语句，返回查询结果（object）。
        /// </summary>
        /// <param name="SQLString">计算查询结果语句</param>
        /// <param name="cmdParms">指定的参数列表</param>
        /// <returns></returns>
        public static object GetSingle(string SQLString, params SqlParameter[] cmdParms) {
            using (SqlConnection connection = new SqlConnection(ConnectionString)) {
                using (SqlCommand cmd = new SqlCommand()) {
                    try {
                        PrepareCommand(cmd, connection, null, SQLString, cmdParms);
                        object obj = cmd.ExecuteScalar();
                        cmd.Parameters.Clear();
                        return CheckSetObjectNull(obj);
                    } catch (System.Data.SqlClient.SqlException e) {
                        throw e;
                    }
                }
            }
        }


        /// <summary>
        /// 执行查询语句，返回DataSet。 默认超时时间30s
        /// </summary>
        /// <param name="SQLString">查询语句</param>
        /// <returns>DataSet 数据集</returns>
        public static DataSet Query(string SQLString) {
            return Query(SQLString, 30);
        }
        /// <summary>
        /// 执行查询语句，返回DataSet。 默认超时时间30s
        /// </summary>
        /// <param name="SQLString">查询语句</param>
        /// <param name="Times">超时时间</param>
        /// <returns>DataSet 数据集</returns>
        public static DataSet Query(string SQLString, int Times) {
            using (SqlConnection connection = new SqlConnection(ConnectionString)) {
                DataSet ds = new DataSet();
                try {
                    connection.Open();
                    SqlDataAdapter command = new SqlDataAdapter(SQLString, connection);
                    command.SelectCommand.CommandTimeout = Times;
                    command.Fill(ds, "ds");
                } catch (System.Data.SqlClient.SqlException ex) {
                    throw new Exception(ex.Message);
                }
                return ds;
            }
        }

        /// <summary>
        /// 暂时不明
        /// </summary>
        public static DataSet Query(string SQLString, params SqlParameter[] cmdParms) {
            using (SqlConnection connection = new SqlConnection(ConnectionString)) {
                SqlCommand cmd = new SqlCommand();
                PrepareCommand(cmd, connection, null, SQLString, cmdParms);
                using (SqlDataAdapter da = new SqlDataAdapter(cmd)) {
                    DataSet ds = new DataSet();
                    try {
                        da.Fill(ds, "ds");
                        cmd.Parameters.Clear();
                    } catch (System.Data.SqlClient.SqlException ex) {
                        throw new Exception(ex.Message);
                    }
                    return ds;
                }
            }
        }
        /// <summary>
        /// 暂时不明
        /// </summary>
        public static DataSet Query(SqlConnection connection, SqlTransaction trans, string SQLString) {
            DataSet ds = new DataSet();
            try {
                SqlDataAdapter command = new SqlDataAdapter(SQLString, connection);
                command.SelectCommand.Transaction = trans;
                command.Fill(ds, "ds");
            } catch (System.Data.SqlClient.SqlException ex) {
                throw new Exception(ex.Message);
            }
            return ds;

        }
        /// <summary>
        /// 暂时不明
        /// </summary>
        public static DataSet Query(SqlConnection connection, SqlTransaction trans, string SQLString, params SqlParameter[] cmdParms) {
            SqlCommand cmd = new SqlCommand();
            PrepareCommand(cmd, connection, trans, SQLString, cmdParms);
            using (SqlDataAdapter da = new SqlDataAdapter(cmd)) {
                DataSet ds = new DataSet();
                try {
                    da.Fill(ds, "ds");
                    cmd.Parameters.Clear();
                } catch (System.Data.SqlClient.SqlException ex) {
                    //trans.Rollback();
                    throw new Exception(ex.Message);
                }
                return ds;
            }
        }


        /// <summary>
        /// 执行SQL语句，返回影响的记录数
        /// </summary>
        /// <param name="SQLString">SQL语句</param>
        /// <returns>影响的记录数</returns>
        public static int ExecuteSql(string SQLString) {
            using (SqlConnection connection = new SqlConnection(ConnectionString)) {
                using (SqlCommand cmd = new SqlCommand(SQLString, connection)) {
                    try {
                        connection.Open();
                        int rows = cmd.ExecuteNonQuery();
                        return rows;
                    } catch (System.Data.SqlClient.SqlException e) {
                        connection.Close();
                        throw e;
                    }
                }
            }
        }
        /// <summary>
        /// 执行带一个存储过程参数的的SQL语句。
        /// </summary>
        /// <param name="SQLString">SQL语句</param>
        /// <param name="content">参数内容,比如一个字段是格式复杂的文章，有特殊符号，可以通过这个方式添加</param>
        /// <returns>影响的记录数</returns>
        public static int ExecuteSql(string SQLString, string content) {
            using (SqlConnection connection = new SqlConnection(ConnectionString)) {
                SqlCommand cmd = new SqlCommand(SQLString, connection);
                System.Data.SqlClient.SqlParameter myParameter = new System.Data.SqlClient.SqlParameter("@content", SqlDbType.NText);
                myParameter.Value = content;
                cmd.Parameters.Add(myParameter);
                try {
                    connection.Open();
                    int rows = cmd.ExecuteNonQuery();
                    return rows;
                } catch (System.Data.SqlClient.SqlException e) {
                    throw e;
                } finally {
                    cmd.Dispose();
                    connection.Close();
                }
            }
        }
        /// <summary>
        /// 执行SQL语句，返回影响的记录数
        /// </summary>
        /// <param name="SQLString">SQL语句</param>
        /// <returns>影响的记录数</returns>
        public static int ExecuteSql(string SQLString, params SqlParameter[] cmdParms) {
            using (SqlConnection connection = new SqlConnection(ConnectionString)) {
                using (SqlCommand cmd = new SqlCommand()) {
                    try {
                        PrepareCommand(cmd, connection, null, SQLString, cmdParms);
                        int rows = cmd.ExecuteNonQuery();
                        cmd.Parameters.Clear();
                        return rows;
                    } catch (System.Data.SqlClient.SqlException e) {
                        throw e;
                    }
                }
            }
        }
        /// <summary>
        /// 执行SQL语句，返回影响的记录数
        /// </summary>
        /// <param name="connection">SqlConnection对象</param>
        /// <param name="trans">SqlTransaction对象</param>
        /// <param name="SQLString">SQL语句</param>
        /// <returns>影响的记录数</returns>
        /// <summary>
        /// 执行SQL语句，返回影响的记录数
        /// </summary>
        /// <param name="connection">SqlConnection对象</param>
        /// <param name="trans">SqlTransaction事件</param>
        /// <param name="SQLString">SQL语句</param>
        /// <returns>影响的记录数</returns>
        public static int ExecuteSql(SqlConnection connection, SqlTransaction trans, string SQLString) {
            using (SqlCommand cmd = new SqlCommand(SQLString, connection)) {
                try {
                    cmd.Connection = connection;
                    cmd.Transaction = trans;
                    int rows = cmd.ExecuteNonQuery();
                    return rows;
                } catch (System.Data.SqlClient.SqlException e) {
                    //trans.Rollback();
                    throw e;
                }
            }
        }
        /// <summary>
        /// 暂时不明
        /// </summary>
        public static int ExecuteSql(SqlConnection connection, SqlTransaction trans, string SQLString, params SqlParameter[] cmdParms) {
            using (SqlCommand cmd = new SqlCommand()) {
                try {
                    PrepareCommand(cmd, connection, trans, SQLString, cmdParms);
                    int rows = cmd.ExecuteNonQuery();
                    cmd.Parameters.Clear();
                    return rows;
                } catch (System.Data.SqlClient.SqlException e) {
                    //trans.Rollback();
                    throw e;
                }
            }
        }
        /// <summary>
        /// 执行Sql和Oracle滴混合事务
        /// </summary>
        /// <param name="list">SQL命令行列表</param>
        /// <param name="oracleCmdSqlList">Oracle命令行列表</param>
        /// <returns>执行结果 0-由于SQL造成事务失败 -1 由于Oracle造成事务失败 1-整体事务执行成功</returns>
        public static int ExecuteSqlTran(List<CommandInfo> list, List<CommandInfo> oracleCmdSqlList) {
            using (SqlConnection conn = new SqlConnection(ConnectionString)) {
                conn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                SqlTransaction tx = conn.BeginTransaction();
                cmd.Transaction = tx;
                try {
                    foreach (CommandInfo myDE in list) {
                        string cmdText = myDE.CommandText;
                        SqlParameter[] cmdParms = (SqlParameter[])myDE.Parameters;
                        PrepareCommand(cmd, conn, tx, cmdText, cmdParms);
                        if (myDE.EffentNextType == EffentNextType.SolicitationEvent) {
                            if (myDE.CommandText.ToLower().IndexOf("count(") == -1) {
                                tx.Rollback();
                                throw new Exception("违背要求" + myDE.CommandText + "必须符合select count(..的格式");
                                //return 0;
                            }

                            object obj = cmd.ExecuteScalar();
                            bool isHave = false;
                            if (obj == null && obj == DBNull.Value) {
                                isHave = false;
                            }
                            isHave = Convert.ToInt32(obj) > 0;
                            if (isHave) {
                                //引发事件
                                myDE.OnSolicitationEvent();
                            }
                        }
                        if (myDE.EffentNextType == EffentNextType.WhenHaveContine || myDE.EffentNextType == EffentNextType.WhenNoHaveContine) {
                            if (myDE.CommandText.ToLower().IndexOf("count(") == -1) {
                                tx.Rollback();
                                throw new Exception("SQL:违背要求" + myDE.CommandText + "必须符合select count(..的格式");
                                //return 0;
                            }

                            object obj = cmd.ExecuteScalar();
                            bool isHave = false;
                            if (obj == null && obj == DBNull.Value) {
                                isHave = false;
                            }
                            isHave = Convert.ToInt32(obj) > 0;

                            if (myDE.EffentNextType == EffentNextType.WhenHaveContine && !isHave) {
                                tx.Rollback();
                                throw new Exception("SQL:违背要求" + myDE.CommandText + "返回值必须大于0");
                                //return 0;
                            }
                            if (myDE.EffentNextType == EffentNextType.WhenNoHaveContine && isHave) {
                                tx.Rollback();
                                throw new Exception("SQL:违背要求" + myDE.CommandText + "返回值必须等于0");
                                //return 0;
                            }
                            continue;
                        }
                        int val = cmd.ExecuteNonQuery();
                        if (myDE.EffentNextType == EffentNextType.ExcuteEffectRows && val == 0) {
                            tx.Rollback();
                            throw new Exception("SQL:违背要求" + myDE.CommandText + "必须有影响行");
                            //return 0;
                        }
                        cmd.Parameters.Clear();
                    }
                    //string oraConnectionString = PubConstant.GetConnectionString("ConnectionStringPPC");
                    //bool res = OracleHelper.ExecuteSqlTran(oraConnectionString, oracleCmdSqlList);
                    //if (!res)
                    //{
                    //    tx.Rollback();
                    //    throw new Exception("Oracle执行失败");
                    // return -1;
                    //}
                    tx.Commit();
                    return 1;
                } catch (System.Data.SqlClient.SqlException e) {
                    tx.Rollback();
                    throw e;
                } catch (Exception e) {
                    tx.Rollback();
                    throw e;
                }
            }
        }
        /// <summary>
        /// 执行多条SQL语句，实现数据库事务。
        /// </summary>
        /// <param name="SQLStringList">多条SQL语句</param>
        public static int ExecuteSqlTran(List<String> SQLStringList) {
            using (SqlConnection conn = new SqlConnection(ConnectionString)) {
                conn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                SqlTransaction tx = conn.BeginTransaction();
                cmd.Transaction = tx;
                try {
                    int count = 0;
                    for (int n = 0; n < SQLStringList.Count; n++) {
                        string strsql = SQLStringList[n];
                        if (strsql.Trim().Length > 1) {
                            cmd.CommandText = strsql;
                            count += cmd.ExecuteNonQuery();
                        }
                    }
                    tx.Commit();
                    return count;
                } catch {
                    tx.Rollback();
                    return 0;
                }
            }
        }
        /// <summary>
        /// 执行多条SQL语句，实现数据库事务。
        /// </summary>
        /// <param name="SQLStringList">SQL语句的哈希表（key为sql语句，value是该语句的SqlParameter[]）</param>
        public static int ExecuteSqlTran(System.Collections.Generic.List<CommandInfo> cmdList) {
            using (SqlConnection conn = new SqlConnection(ConnectionString)) {
                conn.Open();
                using (SqlTransaction trans = conn.BeginTransaction()) {
                    SqlCommand cmd = new SqlCommand();
                    try {
                        int count = 0;
                        //循环
                        foreach (CommandInfo myDE in cmdList) {
                            string cmdText = myDE.CommandText;
                            SqlParameter[] cmdParms = (SqlParameter[])myDE.Parameters;
                            PrepareCommand(cmd, conn, trans, cmdText, cmdParms);

                            if (myDE.EffentNextType == EffentNextType.WhenHaveContine || myDE.EffentNextType == EffentNextType.WhenNoHaveContine) {
                                if (myDE.CommandText.ToLower().IndexOf("count(") == -1) {
                                    trans.Rollback();
                                    return 0;
                                }

                                object obj = cmd.ExecuteScalar();
                                bool isHave = false;
                                if (obj == null && obj == DBNull.Value) {
                                    isHave = false;
                                }
                                isHave = Convert.ToInt32(obj) > 0;

                                if (myDE.EffentNextType == EffentNextType.WhenHaveContine && !isHave) {
                                    trans.Rollback();
                                    return 0;
                                }
                                if (myDE.EffentNextType == EffentNextType.WhenNoHaveContine && isHave) {
                                    trans.Rollback();
                                    return 0;
                                }
                                continue;
                            }
                            int val = cmd.ExecuteNonQuery();
                            count += val;
                            if (myDE.EffentNextType == EffentNextType.ExcuteEffectRows && val == 0) {
                                trans.Rollback();
                                return 0;
                            }
                            cmd.Parameters.Clear();
                        }
                        trans.Commit();
                        return count;
                    } catch (Exception) {
                        trans.Rollback();
                        throw;
                    }
                }
            }
        }
        /// <summary>
        /// 执行多条SQL语句，实现数据库事务。
        /// </summary>
        /// <param name="SQLStringList">SQL语句的哈希表（key为sql语句，value是该语句的SqlParameter[]）</param>
        public static void ExecuteSqlTran(Hashtable SQLStringList) {
            using (SqlConnection conn = new SqlConnection(ConnectionString)) {
                conn.Open();
                using (SqlTransaction trans = conn.BeginTransaction()) {
                    SqlCommand cmd = new SqlCommand();
                    try {
                        //循环
                        foreach (DictionaryEntry myDE in SQLStringList) {
                            string cmdText = myDE.Key.ToString();
                            SqlParameter[] cmdParms = (SqlParameter[])myDE.Value;
                            PrepareCommand(cmd, conn, trans, cmdText, cmdParms);
                            int val = cmd.ExecuteNonQuery();
                            cmd.Parameters.Clear();
                        }
                        trans.Commit();
                    } catch {
                        trans.Rollback();
                        throw;
                    }
                }
            }
        }
        /// <summary>
        /// 执行多条SQL语句，实现数据库事务。
        /// </summary>
        /// <param name="SQLStringList">SQL语句的哈希表（key为sql语句，value是该语句的SqlParameter[]）</param>
        public static void ExecuteSqlTranWithIndentity(System.Collections.Generic.List<CommandInfo> SQLStringList) {
            using (SqlConnection conn = new SqlConnection(ConnectionString)) {
                conn.Open();
                using (SqlTransaction trans = conn.BeginTransaction()) {
                    SqlCommand cmd = new SqlCommand();
                    try {
                        int indentity = 0;
                        //循环
                        foreach (CommandInfo myDE in SQLStringList) {
                            string cmdText = myDE.CommandText;
                            SqlParameter[] cmdParms = (SqlParameter[])myDE.Parameters;
                            foreach (SqlParameter q in cmdParms) {
                                if (q.Direction == ParameterDirection.InputOutput) {
                                    q.Value = indentity;
                                }
                            }
                            PrepareCommand(cmd, conn, trans, cmdText, cmdParms);
                            int val = cmd.ExecuteNonQuery();
                            foreach (SqlParameter q in cmdParms) {
                                if (q.Direction == ParameterDirection.Output) {
                                    indentity = Convert.ToInt32(q.Value);
                                }
                            }
                            cmd.Parameters.Clear();
                        }
                        trans.Commit();
                    } catch {

                        trans.Rollback();
                        throw;
                    }
                }
            }
        }
        /// <summary>
        /// 执行多条SQL语句，实现数据库事务。
        /// </summary>
        /// <param name="SQLStringList">SQL语句的哈希表（key为sql语句，value是该语句的SqlParameter[]）</param>
        public static void ExecuteSqlTranWithIndentity(Hashtable SQLStringList) {
            using (SqlConnection conn = new SqlConnection(ConnectionString)) {
                conn.Open();
                using (SqlTransaction trans = conn.BeginTransaction()) {
                    SqlCommand cmd = new SqlCommand();
                    try {
                        int indentity = 0;
                        //循环
                        foreach (DictionaryEntry myDE in SQLStringList) {
                            string cmdText = myDE.Key.ToString();
                            SqlParameter[] cmdParms = (SqlParameter[])myDE.Value;
                            foreach (SqlParameter q in cmdParms) {
                                if (q.Direction == ParameterDirection.InputOutput) {
                                    q.Value = indentity;
                                }
                            }
                            PrepareCommand(cmd, conn, trans, cmdText, cmdParms);
                            int val = cmd.ExecuteNonQuery();
                            foreach (SqlParameter q in cmdParms) {
                                if (q.Direction == ParameterDirection.Output) {
                                    indentity = Convert.ToInt32(q.Value);
                                }
                            }
                            cmd.Parameters.Clear();
                        }
                        trans.Commit();
                    } catch {
                        trans.Rollback();
                        throw;
                    }
                }
            }
        }
        /// <summary>
        /// 暂时不明
        /// </summary>
        public static int ExecuteSqlByTime(string SQLString, int Times) {
            using (SqlConnection connection = new SqlConnection(ConnectionString)) {
                using (SqlCommand cmd = new SqlCommand(SQLString, connection)) {
                    try {
                        connection.Open();
                        cmd.CommandTimeout = Times;
                        int rows = cmd.ExecuteNonQuery();
                        return rows;
                    } catch (System.Data.SqlClient.SqlException e) {
                        connection.Close();
                        throw e;
                    }
                }
            }
        }
        /// <summary>
        /// 执行带一个存储过程参数的的SQL语句。
        /// </summary>
        /// <param name="SQLString">SQL语句</param>
        /// <param name="content">参数内容,比如一个字段是格式复杂的文章，有特殊符号，可以通过这个方式添加</param>
        /// <returns>影响的记录数</returns>
        public static object ExecuteSqlGet(string SQLString, string content) {
            using (SqlConnection connection = new SqlConnection(ConnectionString)) {
                SqlCommand cmd = new SqlCommand(SQLString, connection);
                System.Data.SqlClient.SqlParameter myParameter = new System.Data.SqlClient.SqlParameter("@content", SqlDbType.NText);
                myParameter.Value = content;
                cmd.Parameters.Add(myParameter);
                try {
                    connection.Open();
                    object obj = cmd.ExecuteScalar();
                    return CheckSetObjectNull(obj);
                } catch (System.Data.SqlClient.SqlException e) {
                    throw e;
                } finally {
                    cmd.Dispose();
                    connection.Close();
                }
            }
        }
        /// <summary>
        /// 向数据库里插入图像格式的字段(和上面情况类似的另一种实例)
        /// </summary>
        /// <param name="strSQL">SQL语句</param>
        /// <param name="fs">图像字节,数据库的字段类型为image的情况</param>
        /// <returns>影响的记录数</returns>
        public static int ExecuteSqlInsertImg(string strSQL, byte[] fs) {
            using (SqlConnection connection = new SqlConnection(ConnectionString)) {
                SqlCommand cmd = new SqlCommand(strSQL, connection);
                System.Data.SqlClient.SqlParameter myParameter = new System.Data.SqlClient.SqlParameter("@fs", SqlDbType.Image);
                myParameter.Value = fs;
                cmd.Parameters.Add(myParameter);
                try {
                    connection.Open();
                    int rows = cmd.ExecuteNonQuery();
                    return rows;
                } catch (System.Data.SqlClient.SqlException e) {
                    throw e;
                } finally {
                    cmd.Dispose();
                    connection.Close();
                }
            }
        }


        #endregion
        /*===============自定义后的方法================*/
        #region 事务
        /// <summary>
        /// 执行事务: 执行多条SQL语句
        /// </summary>
        /// <param name="SQLStrings"></param>
        /// <returns>表示是否成功</returns>
        public static bool ExecuteTransaction(IList<string> SQLStrings) {
            using (SqlConnection myConnection = new SqlConnection(ConnectionString)) {
                myConnection.Open();
                //启动一个事务
                SqlTransaction myTrans = myConnection.BeginTransaction();
                //为事务创建一个命令
                SqlCommand myCommand = new SqlCommand();
                myCommand.Connection = myConnection;
                myCommand.Transaction = myTrans;
                try {
                    if (SQLStrings.Count <= 0)
                        return false;

                    foreach (string sqlCommendstr in SQLStrings) {
                        myCommand.CommandText = sqlCommendstr;
                        myCommand.ExecuteNonQuery();
                    }

                    myTrans.Commit();//提交
                    return true;
                } catch (Exception) {
                    myTrans.Rollback();//遇到错误，回滚
                    return false;
                } finally {
                    myConnection.Close();
                }
            }
        }
        #endregion


        /// <summary>
        /// SQL Server 数据库
        /// 执行多条SQL语句，实现数据库事务
        /// https://blog.csdn.net/wzcool273509239/article/details/51821341
        /// </summary>
        /// <param name="SQLStringList">多条SQL语句</param>
        public static int Transaction_SQLServer(IList<string> SQLStringList) {
            using (SqlConnection conn = new SqlConnection(ConnectionString)) {
                int result = 0;
                conn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                SqlTransaction tx = conn.BeginTransaction();
                cmd.Transaction = tx;
                try {
                    for (int n = 0; n < SQLStringList.Count; n++) {
                        string strsql = SQLStringList[n].ToString();
                        if (strsql.Trim().Length > 1) {
                            cmd.CommandText = strsql;
                            result += cmd.ExecuteNonQuery();
                        }
                        //后来加上的
                        if (n > 0 && (n % 500 == 0 || n == SQLStringList.Count - 1)) {
                            tx.Commit();

                            //二次事务处理
                            tx = conn.BeginTransaction();
                            cmd.Transaction = tx;
                        }
                    }
                    //最后一次提交（网上提供的这句话是被注释掉的，大爷的，错了。该句必须有，不然最后一个循环的数据无法提交）
                    tx.Commit();
                } catch (System.Data.SqlClient.SqlException E) {
                    result = -1;
                    tx.Rollback();
                    throw new Exception(E.Message);
                } catch (Exception ex) {
                    result = -1;
                    tx.Rollback();
                    throw new Exception(ex.Message);
                }

                return result;
            }
        }

        /*
        /// <summary>
        /// mysql数据库
        /// 执行多条SQL语句，实现数据库事务
        /// https://blog.csdn.net/wzcool273509239/article/details/51821341
        /// </summary>
        /// <param name="SQLStringList">多条SQL语句</param>
        public static int Transaction_MySQL(IList<string> SQLStringList)
        {
            using (MySqlConnection conn = new MySqlConnection(m_connectionString))
            {
                int result = 0;
                conn.Open();
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = conn;
                MySqlTransaction tx = conn.BeginTransaction();
                cmd.Transaction = tx;
                try
                {
                    for (int n = 0; n < SQLStringList.Count; n++)
                    {
                        string strsql = SQLStringList[n].ToString();
                        if (strsql.Trim().Length > 1)
                        {
                            cmd.CommandText = strsql;
                            result += cmd.ExecuteNonQuery();
                        }
                        //后来加上的，防止数据量过大，事务卡死现象
                        if (n > 0 && (n % 500 == 0 || n == SQLStringList.Count - 1))
                        {
                            tx.Commit();
                            //二次事务处理
                            tx = conn.BeginTransaction();
                            cmd.Transaction = tx;
                        }
                    }
                    //最后一次提交（网上提供的这句话是被注释掉的，大爷的，错了。该句必须有，不然最后一个循环的数据无法提交）
                    tx.Commit();
                }
                catch (System.Data.SqlClient.SqlException E)
                {
                    result = -1;
                    tx.Rollback();
                    throw new Exception(E.Message);
                }
                catch (Exception ex)
                {
                    result = -1;
                    tx.Rollback();
                    throw new Exception(ex.Message);
                }

                return result;
            }
        }

        /// <summary>
        /// Oracle
        /// 执行多条SQL语句，实现数据库事务
        /// https://blog.csdn.net/wzcool273509239/article/details/51821341
        /// </summary>
        /// <param name="SQLStringList">多条SQL语句</param>
        public static int Transaction_Oracle(IList<string> SQLStringList)
        {
            try
            {
                using (OracleConnection conn = new OracleConnection(m_ConnectString))
                {
                    int result = 0;
                    conn.Open();
                    OracleCommand cmd = new OracleCommand();
                    cmd.Connection = conn;
                    OracleTransaction tx = conn.BeginTransaction();
                    cmd.Transaction = tx;
                    try
                    {
                        for (int n = 0; n < SQLStringList.Count; n++)
                        {
                            string strsql = SQLStringList[n].ToString();
                            if (strsql.Trim().Length > 1)
                            {
                                cmd.CommandText = strsql;
                                result += cmd.ExecuteNonQuery();
                            }
                            //后来加上的  ，500条提交一次事务，防止数据量过大，程序卡死
                            if (n > 0 && (n % 500 == 0 || n == SQLStringList.Count - 1))
                            {
                                tx.Commit();
                                tx = conn.BeginTransaction();
                            }
                        }
                        //最后一次提交（网上提供的这句话是被注释掉的，大爷的，错了。该句必须有，不然最后一个循环的数据无法提交）
                        tx.Commit();
                    }
                    catch (System.Data.SqlClient.SqlException E)
                    {
                        LogLib.LogHelper.WriteErrorLog(typeof(OracleHelper), E);
                        result = -1;
                        tx.Rollback();
                    }
                    catch (Exception ex)
                    {
                        LogLib.LogHelper.WriteErrorLog(typeof(OracleHelper), ex);
                        result = -1;
                        tx.Rollback();
                    }
                    return result;
                }
            }
            catch (Exception ex)
            {
                LogLib.LogHelper.WriteErrorLog(typeof(OracleHelper), ex);
                return -1;
            }
        }
        */
    }
}
