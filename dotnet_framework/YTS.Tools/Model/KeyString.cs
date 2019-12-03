using System;

namespace YTS.Tools.Model
{
    /// <summary>
    /// '键':'值' 数据模型
    /// </summary>
    public class KeyString : KeyBasic
    {
        public KeyString() : base() { }
        public KeyString(string key, string value)
            : base() {
            this.Key = key;
            this.Value = value;
        }

        /// <summary>
        /// String 类型值:
        /// </summary>
        public string Value { get { return _value; } set { _value = value; } }
        private string _value = string.Empty;
    }
}
