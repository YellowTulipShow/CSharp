using System;
using YTS.Engine.IOAccess;
using YTS.Engine.ShineUpon;
using YTS.SystemService;
using YTS.Tools;

namespace YTS.Model
{
    /// <summary>
    /// URL重写路径
    /// </summary>
    public class URLReWriter : AbsShineUpon, IFileInfo
    {

        public URLReWriter() { }

        public string GetPathFolder() {
            return string.Empty;
        }

        public string GetFileName() {
            return @"URLReWriter.config";
        }

        #region === Site Name ===
        private string _sitename = string.Empty;

        /// <summary>
        /// 获取站点名称
        /// </summary>
        public string Get_SiteName() {
            return _sitename;
        }

        /// <summary>
        /// 设置站点名称
        /// </summary>
        /// <param name="sitename"></param>
        public void Set_SiteName(string sitename) {
            this._sitename = sitename;
        }
        #endregion

        #region === Model ===
        /// <summary>
        /// 名称标识
        /// </summary>
        [Explain(@"名称标识")]
        [ShineUponProperty]
        public string Name { get { return _name; } set { _name = value; } }
        private string _name = string.Empty;

        /// <summary>
        /// 页面继承后台逻辑类
        /// </summary>
        [Explain(@"页面继承后台逻辑类")]
        [ShineUponProperty]
        public string Inherit { get { return _inherit; } set { _inherit = value; } }
        private string _inherit = string.Empty;

        /// <summary>
        /// 模板文件名称
        /// </summary>
        [Explain(@"模板文件名称")]
        [ShineUponProperty]
        public string Templet { get { return _templet; } set { _templet = value; } }
        private string _templet = string.Empty;

        /// <summary>
        /// 目标文件名称
        /// </summary>
        [Explain(@"目标文件名称")]
        [ShineUponProperty]
        public string Target { get { return _target; } set { _target = value; } }
        private string _target = string.Empty;

        /// <summary>
        /// 正则请求选项列表
        /// </summary>
        [Explain(@"解释解释")]
        [ShineUponProperty]
        public RegularQuery[] ReItems { get { return _re_query_items; } set { _re_query_items = value; } }
        private RegularQuery[] _re_query_items = null;

        /// <summary>
        /// 正则请求选项
        /// </summary>
        public class RegularQuery : AbsShineUpon
        {
            /// <summary>
            /// 正则表达式
            /// </summary>
            [Explain(@"正则表达式")]
            [ShineUponProperty]
            public string Pattern { get { return _pattern; } set { _pattern = value; } }
            private string _pattern = string.Empty;

            /// <summary>
            /// 传输(请求)参数
            /// </summary>
            [Explain(@"传输(请求)参数")]
            [ShineUponProperty]
            public string QueryParameter { get { return _query_parameter; } set { _query_parameter = value; } }
            private string _query_parameter = string.Empty;
        }
        #endregion

        #region === GetFilePath ===
        /// <summary>
        /// 获取模型数据-文件路径-模板
        /// </summary>
        /// <returns>文件绝对路径</returns>
        public string GetFilePath_Templet() {
            string directory = GetFilePath_Directory();
            string path = PathHelp.CreateUseFilePath(directory, Templet);
            return path;
        }

        /// <summary>
        /// 获取模型数据-文件路径-目标
        /// </summary>
        /// <returns>文件绝对路径</returns>
        public string GetFilePath_Target() {
            string directory = GetFilePath_Directory();
            string path = PathHelp.CreateUseFilePath(directory, Target);
            return path;
        }

        /// <summary>
        /// 获取模型数据-文件夹路径
        /// </summary>
        /// <returns>文件夹相对路径</returns>
        private string GetFilePath_Directory() {
            Model.URLReWriterConfig urlre_config = GlobalSystemService.GetInstance().Config.Get<Model.URLReWriterConfig>();
            string directory = string.Format("/{0}/{1}", urlre_config.RootTemplatePath, Get_SiteName());
            return directory;
        }
        #endregion
    }
}
