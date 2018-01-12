using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlTypes;

namespace CSharp.LibrayFunction
{
    /// <summary>
    /// 转化工具
    /// </summary>
    public static class ConvertTool
    {
        /// <summary>
        /// "垃圾"集合字符串堆 (组合所有参数ToString()方法组合)
        /// </summary>
        public static string CombinationContent(params object[] objs) {
            StringBuilder returnString = new StringBuilder();
            foreach (object obj in objs) {
                returnString.Append(obj.ToString());
            }
            return returnString.ToString();
        }

        #region === List Array String Convert ===
        /// <summary>
        /// 数组列表转字符串
        /// </summary>
        /// <param name="list">需要合并的字符串数组</param>
        /// <param name="symbolSign">用于间隔内容的间隔符号</param>
        /// <returns></returns>
        public static string IListToString(IList list, object symbolSign) {
            try {
                if (CheckData.IsObjectNull(list) || CheckData.IsObjectNull(symbolSign)) {
                    return string.Empty;
                }
                StringBuilder strs = new StringBuilder();
                int firstSign = 0;
                bool isHavefirstValue = false;
                for (int i = firstSign; i < list.Count; i++) {
                    if (CheckData.IsObjectNull(list[i]) || CheckData.IsStringNull(list[i].ToString())) {
                        if (!isHavefirstValue) {
                            firstSign = i + 1;
                        }
                        continue;
                    }
                    if (i > firstSign) {
                        strs.Append(symbolSign);
                    } else {
                        isHavefirstValue = true;
                    }
                    strs.Append(list[i].ToString());
                }
                return strs.ToString();
            } catch (Exception) {
                return string.Empty;
            }
        }
        /// <summary>
        /// 泛型键值对的'键'组合生成字符串
        /// </summary>
        /// <typeparam name="TKey">键</typeparam>
        /// <typeparam name="TValue">值</typeparam>
        /// <param name="dictionary">数据源泛型集合</param>
        /// <param name="symbolSign">用于间隔内容的间隔符号</param>
        /// <returns></returns>
        public static string IDictionaryTKeyToString<TKey, TValue>(IDictionary<TKey, TValue> dictionary, object symbolSign) {
            List<TKey> vals = new List<TKey>();
            foreach (KeyValuePair<TKey, TValue> item in dictionary) {
                vals.Add(item.Key);
            }
            return IListToString(vals, symbolSign);
        }
        /// <summary>
        /// 泛型键值对的'值'组合生成字符串
        /// </summary>
        /// <typeparam name="TKey">键</typeparam>
        /// <typeparam name="TValue">值</typeparam>
        /// <param name="dictionary">数据源泛型集合</param>
        /// <param name="symbolSign">用于间隔内容的间隔符号</param>
        /// <returns></returns>
        public static string IDictionaryTValueToString<TKey, TValue>(IDictionary<TKey, TValue> dictionary, object symbolSign) {
            List<TValue> vals = new List<TValue>();
            foreach (KeyValuePair<TKey, TValue> item in dictionary) {
                vals.Add(item.Value);
            }
            return IListToString(vals, symbolSign);
        }
        /// <summary>
        /// 字符串转数组列表
        /// </summary>
        /// <param name="strValue">要转化的字符串</param>
        /// <param name="Symbol">用于分隔的间隔符号</param>
        /// <returns></returns>
        public static string[] stringToArray(String strValue, Char Symbol) {
            if (CheckData.IsStringNull(strValue.Trim()))
                return new string[] { };
            string[] strarr = strValue.Split(Symbol);
            return strarr;
        }
        #endregion

        #region === Data Type Convert ===
        /// <summary>
        /// 将对象转换为Int32类型
        /// </summary>
        /// <param name="expression">要转换的字符串</param>
        /// <param name="defValue">缺省值</param>
        /// <returns>转换后的int类型结果</returns>
        public static int ObjToInt(object expression, int defValue) {
            if (expression != null)
                return StrToInt(expression.ToString(), defValue);

            return defValue;
        }
        /// <summary>
        /// 将字符串转换为Int32类型
        /// </summary>
        /// <param name="expression">要转换的字符串</param>
        /// <param name="defValue">缺省值</param>
        /// <returns>转换后的int类型结果</returns>
        public static int StrToInt(String expression, int defValue) {
            if (String.IsNullOrEmpty(expression) || expression.Trim().Length >= 11 || !Regex.IsMatch(expression.Trim(), @"^([-]|[0-9])[0-9]*(\.\w*)?$"))
                return defValue;

            int rv;
            if (Int32.TryParse(expression, out rv))
                return rv;

            return Convert.ToInt32(StrToFloat(expression, defValue));
        }


        /// <summary>
        /// Object型转换为decimal型
        /// </summary>
        /// <param name="expression">要转换的字符串</param>
        /// <param name="defValue">缺省值</param>
        /// <returns>转换后的decimal类型结果</returns>
        public static decimal ObjToDecimal(object expression, decimal defValue) {
            if (expression != null)
                return StrToDecimal(expression.ToString(), defValue);

            return defValue;
        }
        /// <summary>
        /// string型转换为decimal型
        /// </summary>
        /// <param name="expression">要转换的字符串</param>
        /// <param name="defValue">缺省值</param>
        /// <returns>转换后的decimal类型结果</returns>
        public static decimal StrToDecimal(String expression, decimal defValue) {
            if ((expression == null) || (expression.Length > 10))
                return defValue;

            decimal intValue = defValue;
            if (expression != null) {
                bool IsDecimal = Regex.IsMatch(expression, @"^([-]|[0-9])[0-9]*(\.\w*)?$");
                if (IsDecimal)
                    decimal.TryParse(expression, out intValue);
            }
            return intValue;
        }


        /// <summary>
        /// Object型转换为float型
        /// </summary>
        /// <param name="expression">要转换的字符串</param>
        /// <param name="defValue">缺省值</param>
        /// <returns>转换后的int类型结果</returns>
        public static float ObjToFloat(object expression, float defValue) {
            if (expression != null)
                return StrToFloat(expression.ToString(), defValue);

            return defValue;
        }
        /// <summary>
        /// string型转换为float型
        /// </summary>
        /// <param name="expression">要转换的字符串</param>
        /// <param name="defValue">缺省值</param>
        /// <returns>转换后的int类型结果</returns>
        public static float StrToFloat(String expression, float defValue) {
            if ((expression == null) || (expression.Length > 10))
                return defValue;

            float intValue = defValue;
            if (expression != null) {
                bool IsFloat = Regex.IsMatch(expression, @"^([-]|[0-9])[0-9]*(\.\w*)?$");
                if (IsFloat)
                    float.TryParse(expression, out intValue);
            }
            return intValue;
        }


        /// <summary>
        /// object型转换为bool型
        /// </summary>
        /// <param name="expression">要转换的字符串</param>
        /// <param name="defValue">缺省值</param>
        /// <returns>转换后的bool类型结果</returns>
        public static bool ObjToBool(object expression, bool defValue) {
            if (expression != null)
                return StrToBool(expression.ToString(), defValue);
            return defValue;
        }
        /// <summary>
        /// string型转换为bool型
        /// </summary>
        /// <param name="strValue">要转换的字符串</param>
        /// <param name="defValue">缺省值</param>
        /// <returns>转换后的bool类型结果</returns>
        public static bool StrToBool(String strValue, bool defValue) {
            if (strValue != null) {
                if (String.Compare(strValue, "true", true) == 0)
                    return true;
                else if (String.Compare(strValue, "false", true) == 0)
                    return false;
            }
            return defValue;
        }


        /// <summary>
        /// 获得 time.Year-time.Month-time.Day 00:00:00 点时间
        /// </summary>
        public static DateTime GetTimeZero(DateTime time) {
            return new DateTime(time.Year, time.Month, time.Day, 0, 0, 0);
        }
        /// <summary>
        /// 获得 time.Year-time.Month-time.Day 23:59:59 点时间
        /// </summary>
        public static DateTime GetTimeTwoFour(DateTime time) {
            return new DateTime(time.Year, time.Month, time.Day, 23, 59, 59);
        }

        /// <summary>
        /// 将对象转换为日期时间类型
        /// </summary>
        /// <param name="obj">要转换的对象</param>
        /// <param name="defValue">缺省值</param>
        /// <returns></returns>
        public static DateTime ObjToDateTime(object obj, DateTime defValue) {
            if (CheckData.IsObjectNull(obj))
                return defValue;
            string objStr = obj.ToString();
            if (CheckData.IsStringNull(objStr))
                return defValue;
            DateTime time;
            return DateTime.TryParse(objStr, out time) ? time : defValue;
        }

        /// <summary>
        /// 将对象转换为日期时间类型
        /// </summary>
        /// <param name="obj">要转换的字符串</param>
        /// <param name="defValue">缺省值</param>
        /// <returns></returns>
        public static SqlDateTime ObjToSqlDateTime(object obj, SqlDateTime defValue) {
            DateTime defTime = defValue.Value;
            DateTime resuTime = ObjToDateTime(obj, defTime);
            if (resuTime < SqlDateTime.MinValue.Value)
                return SqlDateTime.MinValue;
            else if (SqlDateTime.MaxValue.Value < resuTime)
                return SqlDateTime.MaxValue;
            return new SqlDateTime(resuTime);
        }
        #endregion
    }
}
