using System;

namespace CSharp.LibrayDataBase
{
    /// <summary>
    /// Microsoft SQL Server 2008 版本数据库 数据访问器
    /// </summary>
    /// <typeparam name="M">数据访问模型</typeparam>
    public class TableBLLSQLServer<M> : AbsTableBLL<M> where M : AbsModel_Null
    {
        public TableDALSQLServer<M> TableDAL;

        public TableBLLSQLServer(TableDALSQLServer<M> dal) {
            this.TableDAL = dal;
        }
    }
}