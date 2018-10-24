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
    public class SystemConfig : AbsShineUpon
    {
        private readonly IniFile ini = null;
        private readonly string SectionName = null;

        public SystemConfig() {
            string abs_file_path = PathHelp.CreateUseFilePath(@"/auto/ini", @"SystemConfig.ini");
            this.ini = new IniFile(abs_file_path);
            this.SectionName = this.GetType().FullName;
            this.ReReadIniFileInfo();
        }

        /// <summary>
        /// 重新读取 ini 文件信息
        /// </summary>
        public void ReReadIniFileInfo() {
            ShineUponParser<SystemConfig, ShineUponInfo> perser = new ShineUponParser<SystemConfig, ShineUponInfo>();
            foreach (ShineUponInfo info in perser.GetSortResult()) {
                string sinival = this.ini.ReadString(this.SectionName, info.Name, string.Empty);
                if (CheckData.IsStringNull(sinival)) {
                    KeyObject ko = perser.GetModelValue(info, this);
                    string sval = ko.Value.ToString();
                    this.ini.WriteString(this.SectionName, info.Name, sval);
                    continue;
                }
                object oval = perser.DataBasicConvert(info, sinival);
                perser.SetModelValue(info, this, oval);
            }
        }

        #region === Model Property ===
        /// <summary>
        /// 是否 启用调试
        /// </summary>
        [Explain(@"是否 启用调试")]
        [ShineUponProperty]
        public bool IsDeBug {
            get {
                return _isDeBug;
            }
            set {
                _isDeBug = value;
                ini.WriteString(this.SectionName, @"IsDeBug", value.ToString());
            }
        }
        private bool _isDeBug = false;

        /// <summary>
        /// 版本号
        /// </summary>
        [Explain(@"版本号")]
        [ShineUponProperty]
        public string Version {
            get {
                return _version;
            }
            set {
                _version = value;
                ini.WriteString(this.SectionName, @"Version", value.ToString());
            }
        }
        private string _version = @"v1.0.0.0";

        /// <summary>
        /// 路径: 模板文件夹名称
        /// </summary>
        [Explain(@"路径: 模板文件夹名称")]
        [ShineUponProperty]
        public string Path_Template {
            get {
                return _path_template;
            }
            set {
                _path_template = value;
                ini.WriteString(this.SectionName, @"Path_Template", value.ToString());
            }
        }
        private string _path_template = @"Template";
        #endregion
    }
}
