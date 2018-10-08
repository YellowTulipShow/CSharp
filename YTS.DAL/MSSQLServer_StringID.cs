using System;
using YTS.Engine.DataBase.MSQLServer;
using YTS.Engine.IOAccess;
using YTS.Model.DB;
using YTS.Tools;
using YTS.Tools.Model;

namespace YTS.DAL
{
    /// <summary>
    /// Microsoft SQL Server 2008 数据库-数据访问层(Data Access Layer) ID列版本
    /// </summary>
    /// <typeparam name="M">数据映射模型</typeparam>
    public class MSSQLServer_StringID<M> :
        DAL_MSSQLServer<M>,
        IRecordIDPrimaryKey<M, string>
        where M : AbsTable_StringID
    {
        public static readonly M defmodel = ReflexHelp.CreateNewObject<M>();
        public readonly string ColName_SID = ReflexHelp.Name(() => defmodel.SID);

        public MSSQLServer_StringID() : base() { }

        #region ====== using:IRecordIDPrimaryKey<M> ======
        /// <summary>
        /// 插入一条数据
        /// </summary>
        /// <param name="model">数据来源</param>
        /// <param name="sid">插入成功后需要返回的ID值</param>
        /// <returns>是否成功</returns>
        public virtual bool IDInsert(M model, out string sid) {
            model.SID = model.CreateNewSID();
            M repeat_sid_model = IDGetModel(model.SID);
            if (!CheckData.IsObjectNull(repeat_sid_model)) {
                // 不为空, 证明 SID 是重复的, 必须重新计算一个 使用递归
                return IDInsert(model, out sid);
            }
            // 等于空, 证明 SID 不重复可以插入
            sid = model.SID;
            return Insert(model);
        }

        /// <summary>
        /// 插入一条数据
        /// </summary>
        /// <param name="model">数据来源</param>
        /// <returns>是否成功</returns>
        public override bool Insert(M model) {
            if (model.SID == AbsTable_StringID.ERROR_DEFAULT_SID_VALUE) {
                string sid = string.Empty;
                return IDInsert(model, out sid);
            }
            return base.Insert(model);
        }

        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="sid">ID条件</param>
        /// <returns>是否成功</returns>
        public virtual bool IDDelete(string sid) {
            if (sid == AbsTable_StringID.ERROR_DEFAULT_SID_VALUE) {
                return true; // 没有错误数据不用删除
            }
            return Delete(CreateSQL.WhereEqual(ColName_SID, sid.ToString()));
        }

        /// <summary>
        /// 更新数据
        /// </summary>
        /// <param name="keyvaluedic">更新的内容和其值</param>
        /// <param name="sid">ID条件</param>
        /// <returns>是否成功</returns>
        public virtual bool IDUpdate(KeyObject[] keyvaluedic, string sid) {
            return Update(keyvaluedic, CreateSQL.WhereEqual(ColName_SID, sid.ToString()));
        }

        /// <summary>
        /// 获取模型数据
        /// </summary>
        /// <param name="sid">ID条件</param>
        /// <returns>映射数据模型</returns>
        public virtual M IDGetModel(string sid) {
            return GetModel(CreateSQL.WhereEqual(ColName_SID, sid.ToString()), null);
        }
        #endregion
    }
}
