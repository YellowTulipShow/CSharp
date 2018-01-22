using System;
using CSharp.LibrayFunction;

namespace CSharp.LibrayDataBase
{
    /// <summary>
    /// 表-基础-模型 备注
    /// </summary>
    [Serializable]
    public abstract class AbsModel_Remark : AbsModel_TimeAdd
    {
        /// <summary>
        /// 备注
        /// </summary>
        [Explain(@"备注")]
        [Column(MSSFieldTypeCharCount.NVarChar, 500, SortIndex = ushort.MinValue + 1)]
        public string Remark { get { return _remark; } set { _remark = value; } }
        private string _remark = String.Empty;
    }
}
