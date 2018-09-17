using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YTS.Model;

namespace YTS.SystemService
{
    /// <summary>
    /// 系统配置数据模型
    /// </summary>
    [Serializable]
    public class SystemConfigModel : AbsBasicDataModel
    {
        /// <summary>
        /// 是否 启用调试
        /// </summary>
        public bool IsDeBug { get { return _isDeBug; } set { _isDeBug = value; } }
        private bool _isDeBug = true;
    }
}
