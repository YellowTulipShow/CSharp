using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YTS.Engine.IOAccess;
using YTS.Engine.ShineUpon;
using YTS.Tools;

namespace YTS.Engine.Config
{
    /// <summary>
    /// 映射处理数据模型 ini 配置文件版
    /// </summary>
    public abstract class AbsShineUponIni : AbsShineUpon, IFileInfo
    {
        public readonly IniFile ini = null;

        /// <summary>
        /// 构造函数: 初始化 ini 配置文件对象,
        /// </summary>
        public AbsShineUponIni() {
            string rel_folder = GetPathFolder();
            string rel_filename = GetFileName();
            string abs_file_path = PathHelp.CreateUseFilePath(rel_folder, rel_filename);
            this.ini = new IniFile(abs_file_path);
        }

        public virtual string GetPathFolder() {
            return string.Format("/{0}", PathHelp.ToPathSymbol(YTS.Tools.Const.Names.SYSTEM_AUTO_GENERATES_PATH));
        }

        public abstract string GetFileName();

        /// <summary>
        /// 加载
        /// </summary>
        public virtual void Load() {
            this.ini.IniConfig_Read(this);
        }

        /// <summary>
        /// 保存配置文件
        /// </summary>
        public virtual void Save() {
            this.ini.IniConfig_Write(this);
        }
    }
}
