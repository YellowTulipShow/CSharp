using System;
using YTS.Engine.IOAccess;

namespace YTS.BLL
{
    /// <summary>
    /// 业务逻辑层: 用户
    /// </summary>
    public class User : MSSQLServer_StringID<DAL.MSSQLServer_StringID<Model.User>, Model.User>
    {
        public User() : base() { }
    }
}
