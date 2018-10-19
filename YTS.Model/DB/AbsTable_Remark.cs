using System;
using YTS.Engine.DataBase;
using YTS.Tools;

namespace YTS.Model.DB
{
    /// <summary>
    /// 表-基础-模型 备注
    /// </summary>
    [Serializable]
    public abstract class AbsTable_Remark : AbsTable
    {
        /// <summary>
        /// 备注
        /// </summary>
        [Explain(@"备注")]
        [Column(SortIndex = ushort.MaxValue)]
        public string Remark { get { return _remark; } set { _remark = value; } }
        private string _remark = String.Empty;
    }
}
