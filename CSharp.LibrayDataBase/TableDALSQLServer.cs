using System;

namespace CSharp.LibrayDataBase
{
    /// <summary>
    /// Microsoft SQL Server 2008 版本数据库 数据访问器
    /// </summary>
    /// <typeparam name="M">数据访问模型</typeparam>
    public class TableDALSQLServer<M> : AbsTableDAL<M> where M : AbsModel_Null
    {
    }
}