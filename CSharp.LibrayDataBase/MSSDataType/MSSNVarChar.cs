using System;

namespace CSharp.LibrayDataBase.MSSDataType
{
    /// <summary>
    /// Microsoft SQL Server 字段 nvarchar 类型
    /// </summary>
    public class MSSNVarChar : AbsDataType
    {
        public override string TypeName() {
            return string.Format("nvarchar{0}", GetCharLengthStrSign());
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
                return 4000;
            }
        }

        protected override bool IsDefaultCharLengthMAX() {
            return true;
        }
    }
}
