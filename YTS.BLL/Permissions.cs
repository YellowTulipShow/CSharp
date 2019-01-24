using System;
using YTS.Engine.IOAccess;

namespace YTS.BLL
{
    /// <summary>
    /// 业务逻辑层: 权限
    /// </summary>
    public class Permissions : MSSQLServer_StringID<Model.Permissions, DAL.MSSQLServer_StringID<Model.Permissions>>
    {
        public Permissions() : base() { }
    }
}
