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
        public SystemConfig() {
            string abs_file_path = PathHelp.CreateUseFilePath(@"/auto/ini", @"SystemConfig.ini");
            IniFile ini = new IniFile(abs_file_path);

            ShineUponParser<SystemConfig, ShineUponInfo> perser = new ShineUponParser<SystemConfig, ShineUponInfo>();
            Type type = this.GetType();
            string section = type.FullName;
            foreach (ShineUponInfo info in perser.GetSortResult()) {
                string sinival = ini.ReadString(section, info.Name, string.Empty);
                if (CheckData.IsStringNull(sinival)) {
                    string sval = perser.GetModelValue(info, this).ToString();
                    ini.WriteString(section, info.Name, sval);
                    continue;
                }
                object oval = perser.DataBasicConvert(info, sinival);
                perser.SetModelValue(info, this, oval);
            }
        }

        /// <summary>
        /// 是否 启用调试
        /// </summary>
        public bool IsDeBug { get { return _isDeBug; } set { _isDeBug = value; } }
        private bool _isDeBug = true;

        /// <summary>
        /// 版本号
        /// </summary>
        public string Version { get { return _version; } set { _version = value; } }
        private string _version = @"v1.0.0.0";
    }
}
