using System;
using YTS.SystemService;
using YTS.Engine.IOAccess;
using YTS.Engine.ShineUpon;
using YTS.Tools;

namespace YTS.DAL
{
    public class URLReWriter : DAL_LocalXML<Model.URLReWriter>
    {
        public readonly string SiteName = string.Empty;

        public URLReWriter(string sitename)
            : base() {
            this.SiteName = ConvertTool.ObjToString(sitename);
        }

        public override string GetPathFolder() {
            GlobalSystemService Gsys = GlobalSystemService.GetInstance();
            string temp_folder = @"Template"; // Gsys.Config.
            return string.Format("/{0}/{1}", temp_folder, this.SiteName.ToString().Trim('/'));
        }
    }
}
