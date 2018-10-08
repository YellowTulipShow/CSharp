using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YTS.Model;
using YTS.Model.DB;

namespace YTS.Engine.IOAccess
{
    /// <summary>
    /// Microsoft SQL Server 2008 数据库-业务逻辑层(Business Logic Layer) ID列版本
    /// </summary>
    /// <typeparam name="D">调用的DAL类型</typeparam>
    /// <typeparam name="M">数据映射模型</typeparam>
    public class BLL_MSSQLServer_IntID<D, M> :
        BLL_MSSQLServer<D, M>,
        IRecordIDPrimaryKey<M, int>
        where D : DAL_MSSQLServer_IntID<M>
        where M : AbsTable_IntID
    {
        public BLL_MSSQLServer_IntID() : base() { }

        #region ====== using:IRecordIntIDPrimaryKey<M> ======
        /// <summary>
        /// 插入一条数据
        /// </summary>
        /// <param name="model">数据来源</param>
        /// <param name="id">插入成功后需要返回的ID值</param>
        /// <returns>是否成功</returns>
        public virtual bool IDInsert(M model, out int id) {
            return this.SelfDAL.IDInsert(model, out id);
        }

        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="id">ID条件</param>
        /// <returns>是否成功</returns>
        public virtual bool IDDelete(int id) {
            return this.SelfDAL.IDDelete(id);
        }

        /// <summary>
        /// 更新数据
        /// </summary>
        /// <param name="keyvaluedic">更新的内容和其值</param>
        /// <param name="id">ID条件</param>
        /// <returns>是否成功</returns>
        public virtual bool IDUpdate(KeyObject[] keyvaluedic, int id) {
            return this.SelfDAL.IDUpdate(keyvaluedic, id);
        }

        /// <summary>
        /// 获取模型数据
        /// </summary>
        /// <param name="id">ID条件</param>
        /// <returns>映射数据模型</returns>
        public virtual M IDGetModel(int id) {
            return this.SelfDAL.IDGetModel(id);
        }
        #endregion
    }
}
