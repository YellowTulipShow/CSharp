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
            return HolderClass.sysConfig;
        }
        private static class HolderClass
        {
            public static GlobalSystemService sysConfig = new GlobalSystemService();
        }
        #endregion
        
        private GlobalSystemService() {
            // 获得配置信息
            this._config = new SystemConfig();
        }

        /// <summary>
        /// 配置 数据 模型
        /// </summary>
        public SystemConfig Config { get { return _config; } }
        private SystemConfig _config;
    }
}
