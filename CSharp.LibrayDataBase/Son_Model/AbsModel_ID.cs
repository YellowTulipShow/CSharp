using System;
using CSharp.LibrayFunction;

namespace CSharp.LibrayDataBase
{
    /// <summary>
    /// 表-基础-模型 记录ID
    /// </summary>
    [Serializable]
    public abstract class AbsModel_ID : AbsModel_Remark
    {
        /// <summary>
        /// 自增ID
        /// </summary>
        [Explain("自增ID")]
        [Column(MSSFieldTypeStruct.Int, IsPrimaryKey = true, IsIDentity = true)]
        public int id { get { return _id; } set { _id = value; } }
        private int _id = 0;
    }
}
