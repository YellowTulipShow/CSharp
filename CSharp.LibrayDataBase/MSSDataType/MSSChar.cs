using System;
using CSharp.LibrayFunction;

namespace CSharp.LibrayDataBase.MSSDataType
{
    /// <summary>
    /// Microsoft SQL Server 字段 char 类型
    /// </summary>
    public class MSSChar : AbsDBType
    {
        public override string TypeName() {
            return string.Format("char{0}", GetCharLengthStrSign());
        }

        public override object InputConvert(object sourceValue, ColumnItemModel colmodel) {
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
