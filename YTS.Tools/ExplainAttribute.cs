using System;
using System.Reflection;

namespace YTS.Tools
{
    /// <summary>
    /// 解释特性 同一程序不能多个解释。
    /// </summary>
    [AttributeUsage(AttributeTargets.All, AllowMultiple = false, Inherited = false)]
    public class ExplainAttribute : AbsBasicAttribute
    {
        /// <summary>
        /// 错误注释文本
        /// </summary>
        public const string ERROR_EXPLAIN_TEXT = @"Error Explain";

        public ExplainAttribute(string explaninStr) {
            this._text = ConvertTool.ObjectToString(explaninStr);
        }

        /// <summary>
        /// 文本注释
        /// </summary>
        public string Text { get { return _text; } }
        private string _text = string.Empty;

        /// <summary>
        /// 获得解释特性信息
        /// </summary>
        public static ExplainAttribute Extract(MemberInfo memberInfo) {
            ExplainAttribute explainAttr = ReflexHelp.AttributeFindOnly<ExplainAttribute>(memberInfo);
            if (CheckData.IsObjectNull(explainAttr)) {
                explainAttr = new ExplainAttribute(ERROR_EXPLAIN_TEXT);
            }
            return explainAttr;
        }
    }
}
