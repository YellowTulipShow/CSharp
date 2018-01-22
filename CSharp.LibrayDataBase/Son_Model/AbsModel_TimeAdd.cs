using System;
using CSharp.LibrayFunction;

namespace CSharp.LibrayDataBase
{
    /// <summary>
    /// 表-基础-模型 添加时间
    /// </summary>
    [Serializable]
    public abstract class AbsModel_TimeAdd : AbsModel_Null
    {
        /// <summary>
        /// 添加时间
        /// </summary>
        [Explain(@"添加时间")]
        [Column(MSSFieldTypeDefault.Datetime, MSSDefalutValues.DateTimeNow, SortIndex = ushort.MinValue + 1)]
        public DateTime TimeAdd { get { return _timeAdd; } set { _timeAdd = value; } }
        private DateTime _timeAdd = DateTime.Now;
    }
}
