using System;

namespace YTS.Model
{
    /// <summary>
    /// '键':'值' 数据模型
    /// </summary>
    public class KeyString : KeyBasic
    {
        public KeyString() : base() { }

        /// <summary>
        /// String 类型值:
        /// </summary>
        public string Value { get { return _value; } set { _value = value; } }
        private string _value = string.Empty;
    }
}
