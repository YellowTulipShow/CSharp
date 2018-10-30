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
    public class SystemConfig : AbsConfig
    {
        public SystemConfig() : base() { }

        public override string GetFileName() {
            return @"SystemConfig.ini";
        }

        #region === Model Property ===
        /// <summary>
        /// 是否 启用调试
        /// </summary>
        [Explain(@"是否 启用调试")]
        [ShineUponProperty]
        public bool Is_DeBug { get { return _is_debug; } set { _is_debug = value; } }
        private bool _is_debug = false;


        /// <summary>
        /// 版本号
        /// </summary>
        [Explain(@"版本号")]
        [ShineUponProperty]
        public string Version { get { return _version; } set { _version = value; } }
        private string _version = @"v1.0.0.0";


        /// <summary>
        /// 加密使用字符串
        /// </summary>
        [Explain(@"版本号")]
        [ShineUponProperty]
        public string EncryptedUseString {
            get {
                if (CheckData.IsStringNull(_encrypted_use_string)) {
                    char[] word_chars = CommonData.ASCII_WordText();
                    int len = RandomData.GetInt(16, 32 + 1);
                    _encrypted_use_string = RandomData.GetString(word_chars, len);
                }
                return _encrypted_use_string;
            }
            set {
                _encrypted_use_string = value;
            }
        }
        private string _encrypted_use_string = string.Empty;
        #endregion
    }
}
