using System;
using YTS.Model.Attribute;
using YTS.Model.Table.Attribute;

namespace YTS.Model.Table
{
    /// <summary>
    /// 表-基础-模型 记录ID
    /// </summary>
    [Serializable]
    public abstract class AbsTable_IntID : AbsTable_Remark
    {
        public const int ERROR_DEFAULT_INT_VALUE = 0;

        /// <summary>
        /// 自增ID
        /// </summary>
        [Explain(@"自增ID")]
        [Column(IsPrimaryKey = true, IsIDentity = true, SortIndex = ushort.MinValue)]
        public int id { get { return _id; } set { _id = value; } }
        private int _id = ERROR_DEFAULT_INT_VALUE;
    }
}
