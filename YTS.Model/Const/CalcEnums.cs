using System;
using YTS.Tools;

namespace YTS.Model.Const
{
    /// <summary>
    /// 计算枚举
    /// </summary>
    public static class CalcEnums
    {
        /// <summary>
        /// 逻辑 运算符
        /// </summary>
        public enum Logic
        {
            /// <summary>
            /// 并且(and)
            /// </summary>
            [Explain("且(and)")]
            And = 0,

            /// <summary>
            /// 或者(or)
            /// </summary>
            [Explain("或(or)")]
            Or = 1,
        }

        /// <summary>
        /// 比较 运算符
        /// </summary>
        public enum Comparison
        {
            /// <summary>
            /// 等于(=)
            /// </summary>
            [Explain(@"等于(=)")]
            Equal = 10,
            
            /// <summary>
            /// 不等于(!=)
            /// </summary>
            [Explain(@"不等于(!=)")]
            NotEqual = 11,

            /// <summary>
            /// 大于(>)
            /// </summary>
            [Explain("大于(>)")]
            BigThan = 20,

            /// <summary>
            /// 大于等于(>=)
            /// </summary>
            [Explain("大于等于(>=)")]
            BigThanEqual = 21,

            /// <summary>
            /// 小于
            /// </summary>
            [Explain("小于(<)")]
            SmallerThan = 30,

            /// <summary>
            /// 小于等于
            /// </summary>
            [Explain("小于(<=)")]
            SmallerThanEqual = 31,
        }
    }
}
