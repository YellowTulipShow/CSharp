using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YTS.BLL.DB
{
    /// <summary>
    /// 业务逻辑层: 用户
    /// </summary>
    public class User : MSSQLServer_StringID<DAL.MSSQLServer_StringID<Model.DB.User>, Model.DB.User>
    {
        public User() : base() { }
    }
}
