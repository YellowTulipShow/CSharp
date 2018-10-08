using System;
using YTS.Engine.IOAccess;

namespace YTS.BLL.DB
{
    /// <summary>
    /// 业务逻辑层: 用户
    /// </summary>
    public class User : BLL_MSSQLServer_StringID<DAL_MSSQLServer_StringID<Model.DB.Table.User>, Model.DB.Table.User>
    {
        public User() : base() { }
    }
}
