using System;

namespace CSharp.LibrayDataBase.MSSDataType
{
    /// <summary>
    /// Microsoft SQL Server 字段 char 类型
    /// </summary>
    public class MSSChar : AbsFieldTypeCharCount
    {
        public MSSChar(ushort charCount) : base(charCount) { }

        public override ushort MinCharCount {
            get { return 1; }
        }

        public override ushort MaxCharCount {
            get { return 8000; }
        }

        public override string FieldTypeName() {
            return string.Format("char({0})", charCount);
        }
    }
}
