using System;
using System.Data.SqlTypes;
using CSharp.LibrayFunction;

namespace CSharp.LibrayDataBase
{
    /// <summary>
    /// 常量数据解析器
    /// </summary>
    public static class ConstData
    {
        /// <summary>
        /// 常量枚举
        /// </summary>
        public enum ConstEnum
        {
            /// <summary>
            /// 空字符串
            /// </summary>
            Empty = 0,
            /// <summary>
            /// 当前时间
            /// </summary>
            DateTimeNow = 1,

            /// <summary>
            /// Microsoft SQL Server 最小时间值
            /// </summary>
            MSSDateTimeMin = 20,
            /// <summary>
            /// Microsoft SQL Server 最大时间值
            /// </summary>
            MSSDateTimeMax = 21,
        }

        /// <summary>
        /// 常量输出
        /// </summary>
        public static string ConstOutput(ConstEnum constenum) {
            const string timeFormat = LFKeys.TABLE_DATETIME_FORMAT_MILLISECOND;
            switch (constenum) {
                case ConstEnum.Empty:
                    return string.Empty;
                    break;
                case ConstEnum.DateTimeNow:
                    return DateTime.Now.ToString(timeFormat);
                    break;
                case ConstEnum.MSSDateTimeMin:
                    return SqlDateTime.MinValue.Value.ToString(timeFormat);
                    break;
                case ConstEnum.MSSDateTimeMax:
                    return SqlDateTime.MaxValue.Value.ToString(timeFormat);
                    break;
                default:
                    return ConstOutput(ConstEnum.Empty);
                    break;
            }
        }
    }

    /// <summary>
    /// Microsoft C# 程序语言 数据类型解析器
    /// </summary>
    public static class MCSharpDTParser
    {
        public enum DTEnum
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

        public static AbsCSType DataTypeBind(DTEnum dtenum) {
            switch (dtenum) {
                case DTEnum.Struct:
                    return new MCSDataType.MCSStruct();
                    break;
                case DTEnum.Model:
                    return new MCSDataType.MCSModel();
                    break;
                case DTEnum.List:
                    return new MCSDataType.MCSList();
                    break;
                case DTEnum.ListModel:
                    return new MCSDataType.MCSListModel();
                    break;
                default:
                    return DataTypeBind(DTEnum.Struct);
                    break;
            }
        }
    }

    /// <summary>
    /// Microsoft SQL Server 数据类型解析器
    /// </summary>
    public static class MSQLServerDTParser
    {
        public enum DTEnum
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

        public static AbsDBType DataTypeBind(DTEnum dtenum) {
            switch (dtenum) {
                case DTEnum.Char:
                    return new MSSDataType.MSSChar();
                    break;
                case DTEnum.DateTime:
                    return new MSSDataType.MSSDateTime();
                    break;
                case DTEnum.Int:
                    return new MSSDataType.MSSInt();
                    break;
                case DTEnum.Money:
                    return new MSSDataType.MSSMoney();
                    break;
                case DTEnum.NChar:
                    return new MSSDataType.MSSNChar();
                    break;
                case DTEnum.NVarChar:
                    return new MSSDataType.MSSNVarChar();
                    break;
                case DTEnum.VarChar:
                    return new MSSDataType.MSSVarChar();
                    break;
                default:
                    return DataTypeBind(DTEnum.NVarChar);
                    break;
            }
        }
    }
}
