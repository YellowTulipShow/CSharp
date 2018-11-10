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

    }
}
