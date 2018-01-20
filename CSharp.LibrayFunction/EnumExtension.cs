using System;
using System.Linq;
using System.Reflection;

namespace CSharp.LibrayFunction
{
    /// <summary>
    /// System.Enum 的扩展类
    /// </summary>
    public static class EnumExtension
    {
        /// <summary>
        /// 获得枚举选项的名称
        /// </summary>
        public static string GetName(this Enum em) {
            return em.ToString();
        }

        /// <summary>
        /// 获得枚举选项的int值
        /// </summary>
        public static int GetIntValue(this Enum em) {
            return Convert.ToInt32(em);
        }

        /// <summary>
        /// 获得枚举选项的 自定义'解析'特性
        /// </summary>
        public static ExplainAttribute GetExplain(this Enum em) {
            try {
                FieldInfo info = em.GetType().GetField(em.GetName());
                if (info.IsObjectNull()) {
                    throw new Exception();
                }
                ExplainAttribute explain = info.FindAttributeOnly<ExplainAttribute>();
                if (explain.IsObjectNull()) {
                    throw new Exception();
                }
                return explain;
            } catch (Exception) {
                return new ExplainAttribute(string.Empty);
            }
        }
    }
}
