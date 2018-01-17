using System;

namespace CSharp.LibrayDataBase.MSSDataType
{
    /// <summary>
    /// Microsoft SQL Server 字段 nchar 类型
    /// </summary>
    public class MSSNChar : AbsFieldTypeCharCount
    {
        public MSSNChar(ushort charCount) : base(charCount) { }

        public override ushort MinCharCount {
            get { return 1; }
        }

        public override ushort MaxCharCount {
            get { return 4000; }
        }

        public override string FieldTypeName() {
            return string.Format("nchar({0})", charCount);
        }
    }
}
