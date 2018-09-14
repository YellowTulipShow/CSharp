using System;

namespace YTS.Model.Table
{
    /// <summary>
    /// 表-基础-模型
    /// </summary>
    [Serializable]
    public abstract class AbsTable : AbsBasicDataModel, ITableName
    {
        public AbsTable() { }

        /// <summary>
        /// 获得当前表 全名 名称
        /// </summary>
        public abstract string GetTableName();
    }
}
