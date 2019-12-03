using System;
using YTS.Engine.IOAccess;

namespace YTS.BLL
{
    /// <summary>
    /// 业务逻辑层: 角色
    /// </summary>
    public class Role : MSSQLServer_StringID<Model.Role, DAL.MSSQLServer_StringID<Model.Role>>
    {
        public Role() : base() { }
    }
}
