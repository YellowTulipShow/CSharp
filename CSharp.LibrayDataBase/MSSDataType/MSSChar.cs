using System;
using CSharp.LibrayFunction;

namespace CSharp.LibrayDataBase.MSSDataType
{
    /// <summary>
    /// Microsoft SQL Server 字段 char 类型
    /// </summary>
    public class MSSChar : AbsDataType
    {
        public override string TypeName() {
            return string.Format("char{0}", GetCharLengthStrSign());
        }

        public override object TypeConvert(object sourceValue) {
            return base.CharStringTypeConvert(sourceValue);
        }

        protected override ushort MinCharLength {
            get {
                return 1;
            }
        }
        protected override ushort MaxCharLength {
            get {
                return 8000;
            }
        }
    }
}
