using System;
using YTS.Engine.IOAccess;
using YTS.Engine.ShineUpon;
using YTS.Tools;

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

            ShineUponParser<SystemConfig, ShineUponInfo> perser = new ShineUponParser<SystemConfig, ShineUponInfo>();
            Type type = this.GetType();
            this.SectionName = type.FullName;
            foreach (ShineUponInfo info in perser.GetSortResult()) {
                string sinival = this.ini.ReadString(this.SectionName, info.Name, string.Empty);
                if (CheckData.IsStringNull(sinival)) {
                    string sval = perser.GetModelValue(info, this).ToString();
                    this.ini.WriteString(this.SectionName, info.Name, sval);
                    continue;
                }
                object oval = perser.DataBasicConvert(info, sinival);
                perser.SetModelValue(info, this, oval);
            }
        }

        /// <summary>
        /// 是否 启用调试
        /// </summary>
        public bool IsDeBug {
            get { return _isDeBug; }
            set { _isDeBug = value; ini.WriteString(this.SectionName, @"IsDeBug", value.ToString()); }
        }
        private bool _isDeBug = false;

        /// <summary>
        /// 版本号
        /// </summary>
        public string Version {
            get { return _version; }
            set { _version = value; ini.WriteString(this.SectionName, @"Version", value.ToString()); }
        }
        private string _version = @"v1.0.0.0";

        /// <summary>
        /// 路径: 模板文件夹名称
        /// </summary>
        public string Path_Template {
            get { return _path_template; }
            set { _path_template = value; ini.WriteString(this.SectionName, @"Path_Template", value.ToString()); }
        }
        private string _path_template = @"Template";
    }
}
