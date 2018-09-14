using System;
using YTS.Model.Attribute;
using YTS.Model.Table.Attribute;

namespace YTS.Model.Table
{
    /// <summary>
    /// 表-基础-模型 备注
    /// </summary>
    [Serializable]
    public abstract class AbsTable_Remark : AbsTable_TimeAdd
    {
        /// <summary>
        /// 备注
        /// </summary>
        [Explain(@"备注")]
        [Column]
        public string Remark { get { return _remark; } set { _remark = value; } }
        private string _remark = String.Empty;
    }
}
