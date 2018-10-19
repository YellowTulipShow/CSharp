using System;
using YTS.Engine.IOAccess;
using YTS.Engine.ShineUpon;

namespace YTS.Model.DB
{
    /// <summary>
    /// 表-基础-模型
    /// </summary>
    [Serializable]
    public abstract class AbsTable : AbsShineUpon, ITableName
    {
        public AbsTable() { }

        /// <summary>
        /// 获得当前表 全名 名称
        /// </summary>
        public abstract string GetTableName();
    }
}
