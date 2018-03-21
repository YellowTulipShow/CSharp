using System;
using System.Data.SqlTypes;
using CSharp.LibrayFunction;
using CSharp.LibrayDataBase.MSSDataType;

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
            Empty,
            /// <summary>
            /// 当前时间
            /// </summary>
            DateTimeNow,
            /// <summary>
            /// Microsoft SQL Server 最小时间值
            /// </summary>
            MSSDateTimeMin,
            /// <summary>
            /// Microsoft SQL Server 最大时间值
            /// </summary>
            MSSDateTimeMax,
        }

        /// <summary>
        /// 常量输出
        /// </summary>
        public static string ConstOutput(ConstEnum constenum) {
            const string timeFormat = LFKeys.TABLE_DATETIME_FORMAT_MILLISECOND;
            switch (constenum) {
                case ConstEnum.Empty:
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
            return string.Empty;
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
            Int,
            /// <summary>
            /// 金额字段 搭配C#程序的数据类型是 decimal
            /// </summary>
            Money,
            /// <summary>
            /// 时间
            /// </summary>
            DateTime,
            /// <summary>
            /// 字符(串)固定长度，存储ANSI字符，不足的补英文半角空格。(1-8000, 不存中文)
            /// </summary>
            Char,
            /// <summary>
            /// 字符(串)固定长度，存储Unicode字符，不足的补英文半角空格。(1-4000)
            /// </summary>
            NChar,
            /// <summary>
            /// 字符(串)可变长度，存储ANSI字符，根 据数据长度自动变化。(1-8000，不存中文 MAX Yes size: 2^31-1byte 4GB)
            /// </summary>
            VarChar,
            /// <summary>
            /// 字符(串)可变长度，存储Unicode字符，根据数据长度自动变化。(1-4000，MAX Yes size: 2^31-1byte 4GB)
            /// </summary>
            NVarChar,
        }

        public static AbsDataType DataTypeBind(DTEnum dtenum) {
            switch (dtenum) {
                case DTEnum.Char:
                    return new MSSChar();
                    break;
                case DTEnum.DateTime:
                    return new MSSDateTime();
                    break;
                case DTEnum.Int:
                    return new MSSInt();
                    break;
                case DTEnum.Money:
                    return new MSSMoney();
                    break;
                case DTEnum.NChar:
                    return new MSSNChar();
                    break;
                case DTEnum.NVarChar:
                    return new MSSNVarChar();
                    break;
                case DTEnum.VarChar:
                    return new MSSVarChar();
                    break;
                default:
                    return DataTypeBind(DTEnum.NVarChar);
                    break;
            }
        }
    }
}
