using System;
using YTS.SystemService;
using YTS.Engine.IOAccess;
using YTS.Engine.ShineUpon;
using YTS.Tools;
using System.IO;

namespace YTS.DAL
{
    public class URLReWriter : DAL_LocalXML<Model.URLReWriter>
    {
        /// <summary>
        /// 站点名称
        /// </summary>
        private string _siteName = string.Empty;

        /* ================================== ~华丽的间隔线~ ================================== */

        public URLReWriter()
            : base() {
        }
        public URLReWriter(FileShare fileShare)
            : base(fileShare) {
        }

        /* ================================== ~华丽的间隔线~ ================================== */

        public void SetSiteName(string sitename) {
            this._siteName = ConvertTool.ToString(sitename);
            this.ReCreateAbsFilePath();
        }

        public override string GetPathFolder() {
            SystemConfig sys_config = GlobalSystemService.GetInstance().Config.Get<SystemConfig>();
            return string.Format("/{0}/{1}", sys_config.Path_Template, this._siteName.ToString().Trim('/'));
        }
    }
}
