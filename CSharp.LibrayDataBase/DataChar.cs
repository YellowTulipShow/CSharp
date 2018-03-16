using System;
using CSharp.LibrayFunction;

namespace CSharp.LibrayDataBase
{
    /// <summary>
    /// 数据符号
    /// </summary>
    public static class DataChar
    {
        /// <summary>
        /// 数组列表间隔符
        /// </summary>
        public const char ARRAYLIST_INTERVAL_CHAR = ',';

        /// <summary>
        /// 逻辑符
        /// </summary>
        public enum LogicChar
        {
            /// <summary>
            /// 且(and)
            /// </summary>
            [Explain("且(and)")]
            AND = 0,

            /// <summary>
            /// 或(or)
            /// </summary>
            [Explain("或(or)")]
            OR = 1,
        }

        /// <summary>
        /// 操作符
        /// </summary>
        public enum OperChar
        {
            /// <summary>
            /// 等于(=)
            /// </summary>
            [Explain("等于(=)")]
            EQUAL = 0,

            /// <summary>
            /// 不等于(!=)
            /// </summary>
            [Explain("不等于(!=)")]
            EQUAL_NOT = 1,

            /// <summary>
            /// 模糊匹配(like)
            /// </summary>
            [Explain("模糊匹配(like)")]
            LIKE = 2,

            /// <summary>
            /// 含有(in)
            /// </summary>
            [Explain("含有(in)")]
            IN = 3,

            /// <summary>
            /// 不包含(not in)
            /// </summary>
            [Explain("不包含(not in)")]
            IN_NOT = 4,

            /// <summary>
            /// 大于(>)
            /// </summary>
            [Explain("大于(>)")]
            BigTHAN = 5,

            /// <summary>
            /// 大于等于(>=)
            /// </summary>
            [Explain("大于等于(>=)")]
            BigTHAN_EQUAL = 6,

            /// <summary>
            /// 小于(<)
            /// </summary>
            [Explain("小于(<)")]
            SmallTHAN = 7,

            /// <summary>
            /// 小于(<=)
            /// </summary>
            [Explain("小于(<=)")]
            SmallTHAN_EQUAL = 8,
        }
    }
}
