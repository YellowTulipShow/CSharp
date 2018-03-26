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

        #region ====== LogicChar: ======
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
        /// Microsoft SQL Server SQL 逻辑符解析器
        /// </summary>
        public static string MSQLServer_LogicChar_Parser(LogicChar logicChar) {
            const string Space = @" ";
            switch (logicChar) {
                case DataChar.LogicChar.AND:
                    return Space + CreateSQL.WHERE_AND + Space;
                case DataChar.LogicChar.OR:
                    return Space + CreateSQL.WHERE_OR + Space;
                default:
                    return MSQLServer_LogicChar_Parser(DataChar.LogicChar.AND);
            }
        }
        #endregion

        #region ====== OperChar: ======
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
            /// 小于
            /// </summary>
            [Explain("小于(<)")]
            SmallTHAN = 7,

            /// <summary>
            /// 小于等于
            /// </summary>
            [Explain("小于(<=)")]
            SmallTHAN_EQUAL = 8,
        }

        /// <summary>
        /// Microsoft SQL Server SQL 操作符解析器
        /// </summary>
        public static string MSQLServer_OperChar_Parser(FieldValueModel FVm) {
            if (CheckData.IsObjectNull(FVm)) {
                return string.Empty;
            }
            switch (FVm.KeyChar) {
                case DataChar.OperChar.EQUAL:
                    return CreateSQL.WhereEqual(FVm.Name, FVm.Value);
                case DataChar.OperChar.EQUAL_NOT:
                    return CreateSQL.WhereEqualNot(FVm.Name, FVm.Value);
                case DataChar.OperChar.LIKE:
                    return CreateSQL.WhereLike(FVm.Name, FVm.Value);
                case DataChar.OperChar.IN:
                    return CreateSQL.WhereIn(FVm.Name, ConvertTool.ToArrayList(FVm.Value, DataChar.ARRAYLIST_INTERVAL_CHAR));
                case DataChar.OperChar.IN_NOT:
                    return CreateSQL.WhereInNot(FVm.Name, ConvertTool.ToArrayList(FVm.Value, DataChar.ARRAYLIST_INTERVAL_CHAR));
                case DataChar.OperChar.BigTHAN:
                    return CreateSQL.WhereBigThan(FVm.Name, FVm.Value);
                case DataChar.OperChar.BigTHAN_EQUAL:
                    return CreateSQL.WhereBigThanEqual(FVm.Name, FVm.Value);
                case DataChar.OperChar.SmallTHAN:
                    return CreateSQL.WhereSmallThan(FVm.Name, FVm.Value);
                case DataChar.OperChar.SmallTHAN_EQUAL:
                    return CreateSQL.WhereSmallThanEqual(FVm.Name, FVm.Value);
                default:
                    FVm.SetKeyChar(DataChar.OperChar.EQUAL);
                    return MSQLServer_OperChar_Parser(FVm);
            }
        }
        #endregion
    }
}
