using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using YTS.Model.DB;

namespace YTS.Engine.IOAccess
{
    /// <summary>
    /// Microsoft SQL Server 2008 数据库-业务逻辑层(Business Logic Layer)
    /// </summary>
    /// <typeparam name="D">调用的DAL类型</typeparam>
    /// <typeparam name="M">数据映射模型</typeparam>
    public class BLL_MSSQLServer<D, M> :
        YTS.Engine.IOAccess.AbsBLL<M, D, string>,
        ITableName,
        IDataBaseResult<M>
        where D : DAL_MSSQLServer<M>
        where M : AbsTable
    {
        public BLL_MSSQLServer() : base() { }

        #region ====== using:ITableName ======
        public string GetTableName() {
            return this.SelfDAL.GetTableName();
        }
        #endregion

        #region ====== using:IDataBaseResult<M> ======
        /// <summary>
        /// 查询数据
        /// </summary>
        /// <param name="top">返回的记录数</param>
        /// <param name="sql_where">查询条件</param>
        /// <param name="sql_order">排序条件</param>
        /// <returns>结果数据表</returns>
        public DataSet QueryRecords(int top = 0, string sql_where = null, string sql_order = null) {
            return this.SelfDAL.QueryRecords(top, sql_where, sql_order);
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
            return this.SelfDAL.QueryRecords(pageCount, pageIndex, out recordCount, sql_where, sql_order);
        }

        /// <summary>
        /// 数据集 转为 模型列表
        /// </summary>
        /// <param name="ds">数据集</param>
        /// <returns>模型列表</returns>
        public M[] DataSetToModels(DataSet ds) {
            return this.SelfDAL.DataSetToModels(ds);
        }

        /// <summary>
        /// 数据行 转为 单个映射模型
        /// </summary>
        /// <param name="row">数据行</param>
        /// <returns>单个映射模型</returns>
        public M DataRowToModel(DataRow row) {
            return this.SelfDAL.DataRowToModel(row);
        }
        #endregion
    }
}
