using System;
using YTS.Model.Attribute;

namespace YTS.Model.Const
{
    public class DataType
    {
        public enum CSHarp
        {
            /// <summary>
            /// 基础数据类型(bool, byte, char, DateTime, decimal, double, short, int, long, sbyte, float, string, ushort, uint, ulong, enum)
            /// </summary>
            [Explain(@"基础数据类型(bool, byte, char, DateTime, decimal, double, short, int, long, sbyte, float, string, ushort, uint, ulong, enum)")]
            Struct = 0,
            /// <summary>
            /// 继承自 AbsModelNull 的数据模型
            /// </summary>
            [Explain(@"继承自 AbsModelNull 的数据模型")]
            Model = 1,
            /// <summary>
            /// 以基础数据类型组成的数组集合
            /// </summary>
            [Explain(@"以基础数据类型组成的数组集合")]
            List = 2,
            /// <summary>
            /// 以 AbsModelNull 的数据模型组成的数组集合
            /// </summary>
            [Explain(@"以 AbsModelNull 的数据模型组成的数组集合")]
            ListModel = 3,
        }
        public enum MSSQLServer
        {
            /// <summary>
            /// 数值整形
            /// </summary>
            [Explain(@"数值整形")]
            Int = 0,
            /// <summary>
            /// 金额字段 搭配C#程序的数据类型是 decimal
            /// </summary>
            [Explain(@"金额字段")]
            Money = 3,
            /// <summary>
            /// 时间
            /// </summary>
            [Explain(@"时间")]
            DateTime = 1,

            /// <summary>
            /// 字符(串)固定长度，存储ANSI字符，不足的补英文半角空格。(1-8000, 不存中文)
            /// </summary>
            [Explain(@"字符(串)固定长度，存储ANSI字符，不足的补英文半角空格。(1-8000, 不存中文)")]
            Char = 13,
            /// <summary>
            /// 字符(串)固定长度，存储Unicode字符，不足的补英文半角空格。(1-4000)
            /// </summary>
            [Explain(@"字符(串)固定长度，存储Unicode字符，不足的补英文半角空格。(1-4000)")]
            NChar = 12,
            /// <summary>
            /// 字符(串)可变长度，存储ANSI字符，根 据数据长度自动变化。(1-8000，不存中文 MAX Yes size: 2^31-1byte 4GB)
            /// </summary>
            [Explain(@"字符(串)可变长度，存储ANSI字符，根 据数据长度自动变化。(1-8000，不存中文 MAX Yes size: 2^31-1byte 4GB)")]
            VarChar = 11,
            /// <summary>
            /// 字符(串)可变长度，存储Unicode字符，根据数据长度自动变化。(1-4000，MAX Yes size: 2^31-1byte 4GB)
            /// </summary>
            [Explain(@"字符(串)可变长度，存储Unicode字符，根据数据长度自动变化。(1-4000，MAX Yes size: 2^31-1byte 4GB)")]
            NVarChar = 10,
        }
    }
}
