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
        /// <returns></returns>
        public override string GetPathFolder() {
            Model.URLReWriterConfig curl = GlobalSystemService.GetInstance().Config.Get<Model.URLReWriterConfig>();
            return GetSiteNamePathFolder(curl.RootTemplate.Trim('/'));
        }

        /// <summary>
        /// 单一模型处理
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public override Model.URLReWriter SingleModelProcessing(Model.URLReWriter model) {
            Model.URLReWriter result = base.SingleModelProcessing(model);
            if (CheckData.IsObjectNull(result)) {
                return null;
            }
            result.Set_SiteName(this.SelfSiteName.Trim('/'));
            return result;
        }

        /* ================================== ~华丽的间隔线~ ================================== */

        public string GetSiteNamePathFolder(string root) {
            if (CheckData.IsStringNull(root)) {
                return string.Format("/{0}", this.SelfSiteName);
            }
            return string.Format("/{0}/{1}", root.Trim('/'), this.SelfSiteName.Trim('/'));
        }

        public void SetSiteName(string sitename) {
            this.SelfSiteName = ConvertTool.ToString(sitename).ToString().Trim('/');
            this.ReCreateAbsFilePath();
        }

        public string GetRootTemplatePathFolder() {
            Model.URLReWriterConfig curl = GlobalSystemService.GetInstance().Config.Get<Model.URLReWriterConfig>();
            return string.Format("/{0}", curl.RootTemplate.Trim('/'));
        }
    }
}
