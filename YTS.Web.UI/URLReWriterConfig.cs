using System;
using YTS.Engine.ShineUpon;
using YTS.SystemService;
using YTS.Tools;

namespace YTS.Web.UI
{
    public class URLReWriterConfig: AbsConfig
    {
        public URLReWriterConfig() : base() { }

        public override string GetFileName() {
            return @"URLReWriterConfig.ini";
        }

        #region === Model Property ===
        /// <summary>
        /// 根模板路径
        /// </summary>
        [Explain(@"根模板路径")]
        [ShineUponProperty]
        public string RootTemplatePath { get { return _root_template_path; } set { _root_template_path = value; } }
        private string _root_template_path = @"Template";
        #endregion
    }
}
