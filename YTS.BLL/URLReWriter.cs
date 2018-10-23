using System;
using YTS.Engine.IOAccess;
using YTS.Tools;

namespace YTS.BLL
{
    public class URLReWriter : BLL_LocalXML<Model.URLReWriter, DAL.URLReWriter>
    {
        public readonly string SiteName = string.Empty;

        public URLReWriter(string sitename)
            : base() {
            this.SiteName = ConvertTool.ObjToString(sitename);
        }

        public override DAL.URLReWriter InitCreateDAL() {
            return new DAL.URLReWriter(this.SiteName);
        }
    }
}
