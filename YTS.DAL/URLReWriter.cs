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
            this._siteName = ConvertTool.ToString(sitename).ToString().Trim('/');
            this.ReCreateAbsFilePath();
        }

        public override string GetPathFolder() {
            Model.URLReWriterConfig curl = GlobalSystemService.GetInstance().Config.Get<Model.URLReWriterConfig>();
            return string.Format("/{0}/{1}", curl.RootTemplatePath, this._siteName);
        }

        public override Model.URLReWriter SingleModelProcessing(Model.URLReWriter model) {
            Model.URLReWriter result = base.SingleModelProcessing(model);
            result.Set_SiteName(this._siteName);
            return result;
        }
    }
}
