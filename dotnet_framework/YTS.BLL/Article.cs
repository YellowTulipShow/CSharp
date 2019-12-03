using System;
using YTS.Engine.IOAccess;

namespace YTS.BLL
{
    /// <summary>
    /// 业务逻辑层: 文章
    /// </summary>
    public class Article : MSSQLServer_StringID<Model.Article, DAL.MSSQLServer_StringID<Model.Article>>
    {
        public Article() : base() { }
    }
}
