using System;

namespace YTS.Tools.Model
{
    /// <summary>
    /// '键':'值' 数据模型
    /// </summary>
    public class KeyObject : KeyBasic
    {
        public KeyObject() : base() { }
        public KeyObject(string key, object value)
            : base() {
            this.Key = key;
            this.Value = value;
        }

        /// <summary>
        /// Object 类型值:
        /// </summary>
        public object Value { get { return _value; } set { _value = value; } }
        private object _value = string.Empty;
    }
}
