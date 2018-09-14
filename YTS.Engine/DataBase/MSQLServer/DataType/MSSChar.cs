using System;

namespace YTS.Engine.DataBase.MSQLServer.DataType
{
    /// <summary>
    /// Microsoft SQL Server 字段 char 类型
    /// </summary>
    public class MSSChar : AbsDBTypeStrChar
    {
        public override string TypeName() {
            return string.Format("char{0}", GetCharLengthStrSign());
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
