using System;

namespace YTS.Tools.Model
{
    /// <summary>
    /// '键':'值' 数据模型
    /// </summary>
    public class KeyBoolean : KeyBasic
    {
        public KeyBoolean() : base() { }
        public KeyBoolean(string key, bool value)
            : base() {
            this.Key = key;
            this.Value = value;
        }

        /// <summary>
        /// Boolean 类型值:
        /// </summary>
        public bool Value { get { return _value; } set { _value = value; } }
        private bool _value = false;
    }
}
