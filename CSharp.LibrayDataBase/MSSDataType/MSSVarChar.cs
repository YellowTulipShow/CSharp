using System;

namespace CSharp.LibrayDataBase.MSSDataType
{
    /// <summary>
    /// Microsoft SQL Server 字段 nvarchar 类型
    /// </summary>
    public class MSSVarChar : AbsDBType
    {
        public override string TypeName() {
            return string.Format("varchar{0}", GetCharLengthStrSign());
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

        protected override bool IsDefaultCharLengthMAX() {
            return true;
        }
    }
}
