using System;
using YTS.Engine.IOAccess;

namespace YTS.BLL
{
    /// <summary>
    /// 业务逻辑层: 组别
    /// </summary>
    public class Group : MSSQLServer_StringID<Model.Group, DAL.MSSQLServer_StringID<Model.Group>>
    {
        public Group() : base() { }
    }
}
