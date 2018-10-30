using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YTS.Engine.IOAccess;
using YTS.Tools;

namespace YTS.SystemService
{
    /// <summary>
    /// 系统 全局配置 全局单例
    /// </summary>
    public class GlobalSystemService
    {
        #region ====== Single Model Core: ======
        /// <summary>
        /// 获得 实例 对象
        /// </summary>
        public static GlobalSystemService GetInstance() {
            return HolderClass.instance;
        }
        private static class HolderClass
        {
            public static GlobalSystemService instance = new GlobalSystemService();
        }
        #endregion
        
        private GlobalSystemService() {
        }

        /// <summary>
        /// 配置 分析器
        /// </summary>
        public ConfigParser Config {
            get {
                if (CheckData.IsObjectNull(_config)) {
                    _config = new ConfigParser();
                }
                return _config;
            }
        }
        private ConfigParser _config = null;
    }
}
