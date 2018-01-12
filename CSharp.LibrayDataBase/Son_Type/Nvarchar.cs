using System;

namespace CSharp.LibrayDataBase
{
    public class Nvarchar : AbsFieldTypeCharCount
    {
        public Nvarchar(ushort charCount) : base(charCount) { }

        public override ushort MinCharCount {
            get { return 1; }
        }

        public override ushort MaxCharCount {
            get { return 4000; }
        }

        public override string FieldTypeName() {
            return charCount == MAXCHARSIGN ? string.Format("nvarchar({0})", charCount) : @"nvarchar(MAX)";
        }
    }
}
