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

        /// <summary>
        /// 重写: 获取 URL 重写路径配置文件文件夹路径
        /// </summary>
        public override string GetPathFolder() {
            Model.URLReWriterConfig curl = GlobalSystemService.GetInstance().Config.Get<Model.URLReWriterConfig>();
            return GetSiteNamePathFolder(PathHelp.ToPathSymbol(curl.RootTemplate));
        }

        /// <summary>
        /// 重写: 单一模型处理
        /// </summary>
        public override Model.URLReWriter SingleModelProcessing(Model.URLReWriter model) {
            Model.URLReWriter result = base.SingleModelProcessing(model);
            if (CheckData.IsObjectNull(result)) {
                return null;
            }
            result.Set_SiteName(this.SelfSiteName);
            return result;
        }

        /* ================================== ~华丽的间隔线~ ================================== */

        public string GetSiteNamePathFolder(string root) {
            if (CheckData.IsStringNull(root)) {
                return string.Format("/{0}", this.SelfSiteName);
            }
            return string.Format("/{0}/{1}", PathHelp.ToPathSymbol(root), PathHelp.ToPathSymbol(this.SelfSiteName));
        }

        public string GetRootTemplatePathFolder() {
            Model.URLReWriterConfig curl = GlobalSystemService.GetInstance().Config.Get<Model.URLReWriterConfig>();
            return string.Format("/{0}", PathHelp.ToPathSymbol(curl.RootTemplate));
        }

        public void SetSiteName(Model.WebSite modelsite) {
            if (CheckData.IsObjectNull(modelsite)) {
                return;
            }
            this.SelfSiteName = PathHelp.ToPathSymbol(modelsite.Name);
            this.ReCreateAbsFilePath();
        }
    }
}
