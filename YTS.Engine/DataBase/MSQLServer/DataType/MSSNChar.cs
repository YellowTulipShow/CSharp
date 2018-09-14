using System;

namespace YTS.Engine.DataBase.MSQLServer.DataType
{
    /// <summary>
    /// Microsoft SQL Server 字段 nchar 类型
    /// </summary>
    public class MSSNChar : AbsDBTypeStrChar
    {
        public override string TypeName() {
            return string.Format("nchar{0}", GetCharLengthStrSign());
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
