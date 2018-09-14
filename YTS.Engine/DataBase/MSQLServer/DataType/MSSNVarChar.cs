using System;

namespace YTS.Engine.DataBase.MSQLServer.DataType
{
    /// <summary>
    /// Microsoft SQL Server 字段 nvarchar 类型
    /// </summary>
    public class MSSNVarChar : AbsDBTypeStrChar
    {
        public override string TypeName() {
            return string.Format("nvarchar{0}", GetCharLengthStrSign());
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
