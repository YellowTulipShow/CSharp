using System;

namespace CSharp.LibrayDataBase.MSSDataType
{
    /// <summary>
    /// Microsoft SQL Server 字段 nvarchar 类型
    /// </summary>
    public class MSSNVarChar : AbsFieldTypeCharMAX
    {
        public MSSNVarChar(ushort charCount) : base(charCount) { }

        public override ushort MinCharCount {
            get { return 1; }
        }

        public override ushort MaxCharCount {
            get { return 4000; }
        }

        public override string FieldTypeName() {
            return string.Format("nvarchar({0})", (charCount == MAXCHARSIGN) ? @"MAX" : charCount.ToString());
        }
    }
}
