using System;

namespace CSharp.LibrayFunction
{
    /// <summary>
    /// 解释特性 同一程序不能多个解释。
    /// </summary>
    [AttributeUsage(AttributeTargets.All, AllowMultiple = false, Inherited = false)]
    public class ExplainAttribute : System.Attribute
    {
        public ExplainAttribute(String explaninStr) {
            this.text = explaninStr;
        }

        /// <summary>
        /// 文本注释
        /// </summary>
        public String Text { get { return text; } }
        private String text = String.Empty;
    }
}
