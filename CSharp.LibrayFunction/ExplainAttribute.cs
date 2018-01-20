using System;
using System.Linq.Expressions;
using System.Reflection;

namespace CSharp.LibrayFunction
{
    /// <summary>
    /// 解释特性 同一程序不能多个解释。
    /// </summary>
    [AttributeUsage(AttributeTargets.All, AllowMultiple = false, Inherited = false)]
    public class ExplainAttribute : System.Attribute
    {
        public ExplainAttribute(String explaninStr) {
            this._text = explaninStr;
        }

        /// <summary>
        /// 文本注释
        /// </summary>
        public String Text { get { return _text; } }
        private String _text = String.Empty;
    }
}
