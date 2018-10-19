using System;
using YTS.Engine.DataBase;
using YTS.Tools;

namespace YTS.Model.DB
{
    /// <summary>
    /// 表-基础-模型 添加时间
    /// </summary>
    [Serializable]
    public abstract class AbsTable_TimeAdd : AbsTable_Remark
    {
        /// <summary>
        /// 添加时间
        /// </summary>
        [Explain(@"添加时间")]
        [Column(SortIndex = ushort.MaxValue - 1)]
        public DateTime TimeAdd { get { return _timeAdd; } set { _timeAdd = value; } }
        private DateTime _timeAdd = DateTime.Now;
    }
}
