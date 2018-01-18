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

    public static class UsingExplainExtension
    {
        private delegate MemberInfo ObjectMemberInfo(string memberName);
        private static ExplainAttribute AnalysisExplain(object obj, ObjectMemberInfo getMemberInfo) {
            try {
                if (CheckData.IsObjectNull(obj))
                    return new ExplainAttribute(string.Empty);
                // 获得成员元数据
                string name = obj.GetType().Name;
                MemberInfo memberinfo = getMemberInfo(name);
                if (CheckData.IsObjectNull(memberinfo))
                    return new ExplainAttribute(string.Empty);

                // 获取唯一的解释信息
                ExplainAttribute explain = memberinfo.FindAttributeOnly<ExplainAttribute>();
                return !CheckData.IsObjectNull(explain) ? explain :
                    new ExplainAttribute(string.Empty);
            } catch (Exception) {
                return new ExplainAttribute(string.Empty);
            }
        }

        /// <summary>
        /// 获得-特性-解释-枚举类型
        /// </summary>
        //public static ExplainAttribute GetExplain(this Enum em) {
        //    return AnalysisExplain(em, name => em.GetType().GetField(em.ToString()));
        //}

        /// <summary>
        /// 获得-特性-解释-'值'类型(注意查询的是属性)
        /// </summary>
        public static ExplainAttribute GetExplain(this MemberExpression v) {
            return AnalysisExplain(v, name => v.GetType().GetProperty(name));
        }
    }
}
