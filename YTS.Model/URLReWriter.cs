using System;
using YTS.Engine.IOAccess;
using YTS.Engine.ShineUpon;
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

        /// <summary>
        /// 名称标识
        /// </summary>
        [Explain(@"名称标识")]
        [ShineUponProperty]
        public string Name { get { return _name; } set { _name = value; } }
        private string _name = string.Empty;

        /// <summary>
        /// 频道类型
        /// </summary>
        [Explain(@"频道类型")]
        [ShineUponProperty]
        public string Type { get { return _type; } set { _type = value; } }
        private string _type = string.Empty;

        /// <summary>
        /// 源页面地址
        /// </summary>
        [Explain(@"源页面地址")]
        [ShineUponProperty]
        public string Page { get { return _page; } set { _page = value; } }
        private string _page = string.Empty;

        /// <summary>
        /// 页面继承的类名
        /// </summary>
        [Explain(@"页面继承的类名")]
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
        /// 所属频道名称
        /// </summary>
        [Explain(@"所属频道名称")]
        [ShineUponProperty]
        public string Channel { get { return _channel; } set { _channel = value; } }
        private string _channel = string.Empty;

        /// <summary>
        /// 每页数量，类型为列表页时启用该字段
        /// </summary>
        [Explain(@"每页数量，类型为列表页时启用该字段")]
        [ShineUponProperty]
        public string PageSize { get { return _pagesize; } set { _pagesize = value; } }
        private string _pagesize = string.Empty;

        /// <summary>
        /// URL重写表达式连接字符串，后台编辑用到
        /// </summary>
        [Explain(@"URL重写表达式连接字符串，后台编辑用到")]
        [ShineUponProperty]
        public string JoinString { get { return _joinstring; } set { _joinstring = value; } }
        private string _joinstring = string.Empty;

        /// <summary>
        /// 解释解释
        /// </summary>
        [Explain(@"解释解释")]
        [ShineUponProperty]
        public Item[] Items { get { return _items; } set { _items = value; } }
        private Item[] _items = new Item[] { };

        public class Item : AbsShineUpon
        {
            /// <summary>
            /// URL重写表达式
            /// </summary>
            [Explain(@"URL重写表达式")]
            [ShineUponProperty]
            public string Path { get { return _path; } set { _path = value; } }
            private string _path = string.Empty;

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
            public string QueryString { get { return _querystring; } set { _querystring = value; } }
            private string _querystring = string.Empty;
        }
    }
}
