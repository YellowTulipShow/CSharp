using System;

namespace YTS.Tools.Model
{
    /// <summary>
    /// '键':'值','值' 数据模型
    /// </summary>
    public class KeyObjectTwo : KeyObject
    {
        public KeyObjectTwo() : base() { }
        public KeyObjectTwo(string key, object value) : base(key, value) { }
        public KeyObjectTwo(string key, object value, object value_two)
            : base() {
            this.Key = key;
            this.Value = value;
            this.ValueTwo = value_two;
        }

        /// <summary>
        /// Object 类型值: (第二个)
        /// </summary>
        public object ValueTwo { get { return _valueTwo; } set { _valueTwo = value; } }
        private object _valueTwo = string.Empty;
    }
}
