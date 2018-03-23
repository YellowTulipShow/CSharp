using System;

namespace CSharp.LibrayDataBase.MSSDataType
{
    /// <summary>
    /// Microsoft SQL Server 字段 nchar 类型
    /// </summary>
    public class MSSNChar : AbsDBType
    {
        public override string TypeName() {
            return string.Format("nchar{0}", GetCharLengthStrSign());
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
                return 4000;
            }
        }
    }
}
