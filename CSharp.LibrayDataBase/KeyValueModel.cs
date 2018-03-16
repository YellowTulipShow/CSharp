using System;
using CSharp.LibrayFunction;

namespace CSharp.LibrayDataBase
{
    /// <summary>
    /// 选项 KeyValuePair 模型Model
    /// </summary>
    public class KeyValueModel : AbsBasicDataModel
    {
        public KeyValueModel() { }

        /// <summary>
        /// 键:
        /// </summary>
        public string Key { get { return _key; } set { _key = value; } }
        private string _key = string.Empty;

        /// <summary>
        /// 值:
        /// </summary>
        public string Value { get { return _value; } set { _value = value; } }
        private string _value = string.Empty;
    }
}
