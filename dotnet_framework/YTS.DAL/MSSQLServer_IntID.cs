﻿using System;
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
    public class MSSQLServer_IntID<M> :
        DAL_MSSQLServer<M>,
        IRecordIDPrimaryKey<M, int>
        where M : AbsTable_IntID, new()
    {
        public readonly string ColName_IID;

        public MSSQLServer_IntID()
            : base() {
            this.ColName_IID = ReflexHelp.Name(() => this.DefaultModel.IID);
        }

        #region ====== using:IRecordIDPrimaryKey<M> ======
        /// <summary>
        /// 插入一条数据
        /// </summary>
        /// <param name="model">数据来源</param>
        /// <param name="id">插入成功后需要返回的ID值</param>
        /// <returns>是否成功</returns>
        public virtual bool IDInsert(M model, out int id) {
            const int defid = AbsTable_IntID.ERROR_DEFAULT_INT_VALUE;
            id = defid;
            string sqlinsert = SQLInsert(model, true);
            if (CheckData.IsStringNull(sqlinsert)) {
                return false;
            }
            object obj = DbHelperSQL.GetSingle(sqlinsert);
            id = CheckData.IsObjectNull(obj) ? defid : ConvertTool.ToInt(obj, defid);
            return id != defid;
        }

        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="id">ID条件</param>
        /// <returns>是否成功</returns>
        public virtual bool IDDelete(int id) {
            if (id <= AbsTable_IntID.ERROR_DEFAULT_INT_VALUE) {
                return true; // 没有错误数据不用删除
            }
            return Delete(CreateSQL.WhereEqual(ColName_IID, id.ToString()));
        }

        /// <summary>
        /// 更新数据
        /// </summary>
        /// <param name="keyvaluedic">更新的内容和其值</param>
        /// <param name="id">ID条件</param>
        /// <returns>是否成功</returns>
        public virtual bool IDUpdate(KeyObject[] keyvaluedic, int id) {
            return Update(keyvaluedic, CreateSQL.WhereEqual(ColName_IID, id.ToString()));
        }

        /// <summary>
        /// 获取模型数据
        /// </summary>
        /// <param name="id">ID条件</param>
        /// <returns>映射数据模型</returns>
        public virtual M IDGetModel(int id) {
            return GetModel(CreateSQL.WhereEqual(ColName_IID, id.ToString()), null);
        }
        #endregion
    }
}
