using System;
using YTS.Model.DB;
using YTS.Tools.Model;

namespace YTS.Engine.IOAccess
{
    /// <summary>
    /// Microsoft SQL Server 2008 数据库-业务逻辑层(Business Logic Layer) ID列版本
    /// </summary>
    /// <typeparam name="D">调用的DAL类型</typeparam>
    /// <typeparam name="M">数据映射模型</typeparam>
    public class BLL_MSSQLServer_StringID<D, M> :
        BLL_MSSQLServer<D, M>,
        IRecordIDPrimaryKey<M, string>
        where D : DAL_MSSQLServer_StringID<M>
        where M : AbsTable_StringID
    {
        public BLL_MSSQLServer_StringID() : base() { }

        #region ====== using:IRecordStringIDPrimaryKey<M> ======
        /// <summary>
        /// 插入一条数据
        /// </summary>
        /// <param name="model">数据来源</param>
        /// <param name="sid">插入成功后需要返回的ID值</param>
        /// <returns>是否成功</returns>
        public virtual bool IDInsert(M model, out string sid) {
            return this.SelfDAL.IDInsert(model, out sid);
        }

        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="sid">ID条件</param>
        /// <returns>是否成功</returns>
        public virtual bool IDDelete(string sid) {
            return this.SelfDAL.IDDelete(sid);
        }

        /// <summary>
        /// 更新数据
        /// </summary>
        /// <param name="keyvaluedic">更新的内容和其值</param>
        /// <param name="sid">ID条件</param>
        /// <returns>是否成功</returns>
        public virtual bool IDUpdate(KeyObject[] keyvaluedic, string sid) {
            return this.SelfDAL.IDUpdate(keyvaluedic, sid);
        }

        /// <summary>
        /// 获取模型数据
        /// </summary>
        /// <param name="sid">ID条件</param>
        /// <returns>映射数据模型</returns>
        public virtual M IDGetModel(string sid) {
            return this.SelfDAL.IDGetModel(sid);
        }
        #endregion
    }
}
