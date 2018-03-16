using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using CSharp.LibrayFunction;

namespace CSharp.LibrayDataBase
{
    /// <summary>
    /// SQL创造器
    /// </summary>
    public static class CreateSQL
    {
        #region ====== Const Field String Char ======
        /* ====== Where Part ====== */
        /// <summary>
        /// 和并且 and 字段
        /// </summary>
        public const string WHERE_AND = @"and";
        /// <summary>
        /// 或者 or 字段
        /// </summary>
        public const string WHERE_OR = @"or";
        /// <summary>
        /// 取反 not 字段
        /// </summary>
        public const string WHERE_NOT = @"not";
        /// <summary>
        /// 左边 小括号 ( 
        /// </summary>
        public const string WHERE_LEFTPARENTHESES = @"(";
        /// <summary>
        /// 右边 小括号 )
        /// </summary>
        public const string WHERE_RIGHTPARENTHESES = @")";

        /* ====== Order By Part ====== */
        /// <summary>
        /// 排序: 正序
        /// </summary>
        public const string ORDERBY_ASC = @"asc";
        /// <summary>
        /// 排序: 倒序
        /// </summary>
        public const string ORDERBY_DESC = @"desc";
        /// <summary>
        /// 排序: 间隔
        /// </summary>
        public const string ORDERBY_INTERVALSYMBOL = @",";
        #endregion

        #region ====== Where Function ======
        /// <summary>
        /// 小括号包裹: (source)
        /// </summary>
        public static string WhereParenthesesPackage(string source) {
            return WHERE_LEFTPARENTHESES + source + WHERE_RIGHTPARENTHESES;
        }

        /* ====== Where Equal Part ====== */
        /// <summary>
        /// 条件: field = 'values'
        /// </summary>
        public static string WhereEqual(string field, string values) {
            return String.Format("{0} = '{1}'", field, values);
        }
        /// <summary>
        /// 条件: field != 'values'
        /// </summary>
        public static string WhereNotEqual(string field, string values) {
            return String.Format("{0} != '{1}'", field, values);
        }

        /* ====== Where Range Part ====== */
        /// <summary>
        /// 条件: field 小于 'values'
        /// </summary>
        public static string WhereLessThan(string field, string values) {
            return String.Format("{0} < '{1}'", field, values);
        }
        /// <summary>
        /// 条件: field 小于= 'values'
        /// </summary>
        public static string WhereLessThanEqual(string field, string values) {
            return String.Format("{0} <= '{1}'", field, values);
        }
        /// <summary>
        /// 条件: field > 'values'
        /// </summary>
        public static string WhereGreaterThan(string field, string values) {
            return String.Format("{0} > '{1}'", field, values);
        }
        /// <summary>
        /// 条件: field >= 'values'
        /// </summary>
        public static string WhereGreaterThanEqual(string field, string values) {
            return String.Format("{0} >= '{1}'", field, values);
        }

        /* ====== Where Like Part ====== */
        /// <summary>
        /// 条件: field like '%values%'
        /// </summary>
        public static string WhereLike(string field, string values) {
            return String.Format("{0} like '%{1}%'", field, values);
        }
        /// <summary>
        /// 条件: field not like '%values%'
        /// </summary>
        public static string WhereNotLike(string field, string values) {
            return String.Format("{0} not like '%{1}%'", field, values);
        }
        /// <summary>
        /// 条件: Convert(varchar,{0},120) like '%{1}%'
        /// </summary>
        public static string WhereLikeTime(string field, string timeFormatValue) {
            string timefield = String.Format("Convert(varchar,{0},120)", field);
            return WhereLike(timefield, timeFormatValue);
        }
        /// <summary>
        /// 条件: ('values' like '%'+field+'%' or field like '%values%')
        /// </summary>
        public static string WhereLikeTwoWay(string field, string values) {
            return String.Format("('{1}' like '%'+{0}+'%' or {0} like '%{1}%')", field, values);
        }
        /// <summary>
        /// 条件查询 根据所有的公共属性遍历
        /// </summary>
        /// <param name="model">数据模型</param>
        /// <param name="values">用于查询的值数组</param>
        public static string WhereLikeAllPropertyInfo(AbsBasicDataModel model, string[] values) {
            PropertyInfo[] ProInfoArrs = new PropertyInfo[] { };
            try { ProInfoArrs = model.GetType().GetProperties(); } catch (Exception) { return String.Empty; }

            // 如果 所有的属性 和 要查询的内容 都小于等于0的话 没必要执行了
            if (ProInfoArrs.Length <= 0 || values.Length <= 0)
                return String.Empty;

            StringBuilder SQLString = new StringBuilder();
            SQLString.Append(WHERE_LEFTPARENTHESES); // 左边 小括号
            for (int proCount = 0; proCount < ProInfoArrs.Length; proCount++) {
                for (int valCount = 0; valCount < values.Length; valCount++) {
                    if (valCount != 0 || proCount != 0)
                        SQLString.Append(WHERE_OR);
                    SQLString.Append(WhereLike(ProInfoArrs[proCount].Name, values[valCount]));
                }
            }
            SQLString.Append(WHERE_RIGHTPARENTHESES); // 右边 小括号
            return SQLString.ToString();
        }
        /// <summary>
        /// 组合内容字段查询 条件: field like '%,values%' or field like '%values,%' or field ='values'
        /// </summary>
        /// <returns></returns>
        public static string WhereSymbolLikeOrEqual(string field, string values, char symbol) {
            return String.Format("({0} like '%{2}{1}%' or {0} like '%{1}{2}%' or {0} ='{1}')", field, values, symbol);
        }

        /* ====== Where In Part ====== */
        /// <summary>
        /// 条件: and field in ('v1','v2','v3','v4')
        /// </summary>
        public static string WhereAndIn(string field, string[] values) {
            string SQLWhereIn = WhereIn(field, values);
            if (CheckData.IsStringNull(SQLWhereIn))
                return String.Empty;
            return WHERE_AND + SQLWhereIn;
        }
        /// <summary>
        /// 条件: field in ('v1','v2','v3','v4')
        /// </summary>
        public static string WhereIn(string field, string[] values) {
            return WhereNotIn(field, values, false);
        }
        /// <summary>
        /// 条件: field not in ('v1','v2','v3','v4')
        /// </summary>
        public static string WhereNotIn(string field, string[] values) {
            return WhereNotIn(field, values, true);
        }
        private static string WhereNotIn(string field, string[] values, bool isNot) {
            StringBuilder instr = new StringBuilder();
            for (int i = 0; i < values.Length; i++) {
                if (i != 0) {
                    instr.Append(",");
                }
                instr.AppendFormat("'{0}'", values[i]);
            }
            if (CheckData.IsStringNull(instr.ToString())) {
                instr.Append("''");
            }
            string notstr = String.Empty;
            if (isNot) {
                notstr = WHERE_NOT;
            }
            return String.Format("{0} {1} in ({2})", field, notstr, instr.ToString());
        }
        #endregion

        #region ====== Order By Function ======
        /// <summary>
        /// 排序语句: order by field asc
        /// </summary>
        public static string OrderBy(string field, bool isasc) {
            return "order by" + OrderBySimp(field, isasc);
        }
        /// <summary>
        /// 排序语句: order by field asc
        /// </summary>
        public static string OrderBy(Dictionary<string, bool> fields) {
            return "order by" + OrderBySimp(fields);
        }
        /// <summary>
        /// 排序语句(简洁版:适用于GetList): field asc
        /// </summary>
        public static string OrderBySimp(string field, bool isasc) {
            string symbol = String.Empty;
            if (isasc) {
                symbol = ORDERBY_ASC;
            } else {
                symbol = ORDERBY_DESC;
            }
            return field + "" + symbol;
        }
        /// <summary>
        /// 排序语句(简洁版:适用于GetList): field asc
        /// </summary>
        public static string OrderBySimp(Dictionary<string, bool> fields) {
            StringBuilder order = new StringBuilder();
            foreach (KeyValuePair<string, bool> item in fields) {
                order.Append(OrderBySimp(item.Key, item.Value));
                order.Append(ORDERBY_INTERVALSYMBOL);
            }
            return order.ToString().Trim(ORDERBY_INTERVALSYMBOL.ToCharArray()).ToString();
        }
        #endregion

        #region ====== Update Sentence Function ======
        /// <summary>
        /// 获得更新语句
        /// </summary>
        public static string Update(string tableName, string field, string values, string where) {
            Dictionary<string, string> paramDic = new Dictionary<string, string>();
            paramDic.Add(field, values);
            string updatestr = Update(tableName, paramDic, where);
            return updatestr;
        }
        /// <summary>
        /// 获得更新语句 (多个更新内容)
        /// </summary>
        public static string Update(string tableName, Dictionary<string, string> paramDic, string where) {
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("update {0} set", tableName);
            StringBuilder setstr = new StringBuilder();
            foreach (KeyValuePair<string, string> item in paramDic) {
                setstr.AppendFormat("{0} = '{1}',", item.Key, item.Value);
            }
            sql.Append(setstr.ToString().Trim(','));
            if (!CheckData.IsStringNull(where)) {
                sql.AppendFormat("where {0}", where);
            }
            return sql.ToString();
        }
        #endregion
    }
}
