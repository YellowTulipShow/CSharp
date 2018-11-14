﻿using System;
using YTS.Engine.Config;
using YTS.Engine.ShineUpon;
using YTS.Tools;

namespace YTS.Model
{
    /// <summary>
    /// URL重写配置
    /// </summary>
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
        public string RootTemplate { get { return _root_template; } set { _root_template = value; } }
        private string _root_template = @"/Template";

        /// <summary>
        /// 根执行页面路径
        /// </summary>
        [Explain(@"根模板路径")]
        [ShineUponProperty]
        public string RootPage {
            get {
                if (CheckData.IsStringNull(_root_page)) {
                    _root_page = string.Format("/{0}/ASPXPage", YTS.Tools.Const.Names.SYSTEM_AUTO_GENERATES_PATH);
                }
                return _root_page;
            }
            set { _root_page = value; }
        }
        private string _root_page = string.Empty;
        #endregion
    }
}
