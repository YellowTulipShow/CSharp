using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YTS.Model
{
    /// <summary>
    /// '键':'值' 数据模型
    /// </summary>
    public class KeyObject : KeyBasic
    {
        public KeyObject() : base() { }

        /// <summary>
        /// Object 类型值:
        /// </summary>
        public object Value { get { return _value; } set { _value = value; } }
        private object _value = string.Empty;
    }
}
