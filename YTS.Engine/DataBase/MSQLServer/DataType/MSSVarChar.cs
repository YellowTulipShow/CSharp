using System;

namespace YTS.Engine.DataBase.MSQLServer.DataType
{
    /// <summary>
    /// Microsoft SQL Server 字段 nvarchar 类型
    /// </summary>
    public class MSSVarChar : AbsDBTypeStrChar
    {
        public override string TypeName() {
            return string.Format("varchar{0}", GetCharLengthStrSign());
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
