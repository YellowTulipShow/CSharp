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
        private string SelfSiteName { get; set; }

        /* ================================== ~华丽的间隔线~ ================================== */

        public URLReWriter()
            : base() {
        }
        public URLReWriter(FileShare fileShare)
            : base(fileShare) {
        }

        /* ================================== ~华丽的间隔线~ ================================== */

        public void SetSiteName(string sitename) {
            this.SelfSiteName = ConvertTool.ToString(sitename).ToString().Trim('/');
            this.ReCreateAbsFilePath();
        }

        public override string GetPathFolder() {
            Model.URLReWriterConfig curl = GlobalSystemService.GetInstance().Config.Get<Model.URLReWriterConfig>();
            return GetSiteNamePathFolder(curl.RootTemplate);
        }

        public string GetSiteNamePathFolder(string root) {
            if (CheckData.IsStringNull(root)) {
                return string.Format("/{0}", this.SelfSiteName);
            }
            return string.Format("/{0}/{1}", root, this.SelfSiteName);
        }

        public override Model.URLReWriter SingleModelProcessing(Model.URLReWriter model) {
            Model.URLReWriter result = base.SingleModelProcessing(model);
            if (CheckData.IsObjectNull(result)) {
                return null;
            }
            result.Set_SiteName(this.SelfSiteName);
            return result;
        }
    }
}
