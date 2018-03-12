using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CSharp.LibrayDataBase
{
    /// <summary>
    /// 抽象: 选项
    /// </summary>
    public class ItemModel : AbsBasicsAttribute
    {
        /// <summary>
        /// 关键字
        /// </summary>
        public virtual string KeyWords() {
            return string.Empty;
        }

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
