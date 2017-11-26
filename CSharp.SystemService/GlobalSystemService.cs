using CSharp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CSharp.SystemService
{
    /// <summary>
    /// 系统 全局配置 全局单例
    /// </summary>
    public class GlobalSystemService
    {
        #region ====== Singleton Model Core: ======
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
            _configModel = CreateSystemConfigModel();
        }

        /// <summary>
        /// 配置 数据 模型
        /// </summary>
        public SystemConfigModel ConfigModel { get { return _configModel; } }
        private SystemConfigModel _configModel;

        /// <summary>
        /// 创建系统配置模型参数内容
        /// </summary>
        private SystemConfigModel CreateSystemConfigModel() {
            // 根据 Config.xml 文件内容 构造 ConfigModel 数据模型
            return new SystemConfigModel();
        }
    }
}
