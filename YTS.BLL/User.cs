using System;
using YTS.Engine.IOAccess;

namespace YTS.BLL
{
    /// <summary>
    /// 业务逻辑层: 用户
    /// </summary>
    public class User : MSSQLServer_StringID<Model.User, DAL.MSSQLServer_StringID<Model.User>>
    {
        public User() : base() { }
    }
}
