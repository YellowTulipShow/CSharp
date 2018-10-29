using System;
using YTS.Engine.IOAccess;
using YTS.Engine.ShineUpon;
using YTS.Tools;
using YTS.Tools.Model;

namespace YTS.SystemService
{
    /// <summary>
    /// 系统配置数据模型
    /// </summary>
    [Serializable]
    public class SystemConfig : AbsShineUponIni
    {
        public SystemConfig() : base() { }

        public override string GetPathFolder() {
            return @"/auto/ini";
        }

        public override string GetFileName() {
            return @"SystemConfig.ini";
        }

        #region === Model Property ===
        /// <summary>
        /// 是否 启用调试
        /// </summary>
        [Explain(@"是否 启用调试")]
        [ShineUponProperty]
        public bool IsDeBug { get { return _isDeBug; } set { _isDeBug = value; } }
        private bool _isDeBug = false;

        /// <summary>
        /// 版本号
        /// </summary>
        [Explain(@"版本号")]
        [ShineUponProperty]
        public string Version { get { return _version; } set { _version = value; } }
        private string _version = @"v1.0.0.0";

        /// <summary>
        /// 路径: 模板文件夹名称
        /// </summary>
        [Explain(@"路径: 模板文件夹名称")]
        [ShineUponProperty]
        public string Path_Template { get { return _path_template; } set { _path_template = value; } }
        private string _path_template = @"Template";
        #endregion
    }
}
