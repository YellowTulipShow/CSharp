using System;
using YTS.Model.Attribute;
using YTS.Model.Table.Attribute;

namespace YTS.Model.Table
{
    /// <summary>
    /// 表-基础-模型 添加时间
    /// </summary>
    [Serializable]
    public abstract class AbsTable_TimeAdd : AbsTable
    {
        /// <summary>
        /// 添加时间
        /// </summary>
        [Explain(@"添加时间")]
        [Column]
        public DateTime TimeAdd { get { return _timeAdd; } set { _timeAdd = value; } }
        private DateTime _timeAdd = DateTime.Now;
    }
}
