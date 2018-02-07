using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Data;

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
        public static string IListToString(IList list, IConvertible symbolSign) {
            try {
                if (CheckData.IsObjectNull(list) || CheckData.IsObjectNull(symbolSign))
                    return string.Empty;
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
                        strs.Append(symbolSign.ToString());
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
        /// 字符串转字符串数组
        /// </summary>
        /// <param name="strValue">要转化的字符串</param>
        /// <param name="Symbol">用于分隔的间隔符号</param>
        public static string[] ToArrayList(this string strValue, IConvertible symbolSign) {
            try {
                if (CheckData.IsStringNull(strValue) || CheckData.IsObjectNull(symbolSign))
                    throw new Exception();
                string[] strarr = strValue.Split(symbolSign.ToString().ToCharArray());
                return strarr;
            } catch (Exception) {
                return new string[] { };
            }
        }
        #endregion

        #region === Type Convert ===
        /// <summary>
        /// 委托: 实现转换算法
        /// </summary>
        /// <typeparam name="RT">结果返回值-数据类型</typeparam>
        /// <typeparam name="ST">数据源数组-数据类型</typeparam>
        /// <param name="sourceitem">数据来源</param>
        public delegate RT ConvertTypeDelegate<RT, ST>(ST sourceitem);
        /// <summary>
        /// 数组列表之间的类型数据转换
        /// </summary>
        /// <typeparam name="RT">结果返回值-数据类型</typeparam>
        /// <typeparam name="SLT">数据源-数据类型</typeparam>
        /// <typeparam name="SIT">数据源单个选项类型</typeparam>
        /// <param name="sourceslist">数据源数组</param>
        /// <param name="convertMethod">用户实现转换算法</param>
        /// <param name="isClearErrorValue">是否清除指定的错误值</param>
        /// <param name="errorValue">需要排除的错误值</param>
        private static RT[] ListConvertType<RT, SLT, SIT>(SLT sourceslist, ConvertTypeDelegate<RT, SIT> convertMethod,
            bool isClearErrorValue, RT errorValue = default(RT)) where SLT : IEnumerable {
            if (CheckData.IsObjectNull(sourceslist))
                return new RT[] { };
            List<RT> list = new List<RT>();
            isClearErrorValue = isClearErrorValue && !CheckData.IsObjectNull(errorValue);
            foreach (SIT item in sourceslist) {
                if (CheckData.IsObjectNull(item))
                    continue;
                RT value = convertMethod(item);
                if (CheckData.IsObjectNull(value)) {
                    continue;
                } else if (isClearErrorValue && errorValue.Equals(value)) {
                    continue;
                }
                list.Add(value);
            }
            return list.ToArray();
        }

        /// <summary>
        /// 'ST'类型数组 转 'RT'类型数据结果
        /// </summary>
        public static RT[] ListConvertType<RT, ST>(this ST[] sourceList, ConvertTypeDelegate<RT, ST> convertMethod) {
            return ListConvertType(sourceList, convertMethod, false);
        }
        /// <summary>
        /// 'ST'类型数组 转 'RT'类型数据结果 排除指定的错误项
        /// </summary>
        public static RT[] ListConvertType<RT, ST>(this ST[] sourceList, ConvertTypeDelegate<RT, ST> convertMethod, RT errorValue) {
            return ListConvertType(sourceList, convertMethod, true, errorValue: errorValue);
        }
        /// <summary>
        /// DataTable表 转 'RT'类型数据结果
        /// </summary>
        public static RT[] ListConvertType<RT>(this DataTable sourceList, ConvertTypeDelegate<RT, DataRow> convertMethod) {
            return ListConvertType(sourceList.Rows, convertMethod, false);
        }
        /// <summary>
        /// DataTable表 转 'RT'类型数据结果 排除指定的错误项
        /// </summary>
        public static RT[] ListConvertType<RT>(this DataTable sourceList, ConvertTypeDelegate<RT, DataRow> convertMethod, RT errorValue) {
            return ListConvertType(sourceList.Rows, convertMethod, true, errorValue: errorValue);
        }
        /// <summary>
        /// Dictionary字典序列 转 'RT'类型数据结果
        /// </summary>
        public static RT[] ListConvertType<RT, STKey, STValue>(this Dictionary<STKey, STValue> sourceList, ConvertTypeDelegate<RT, KeyValuePair<STKey, STValue>> convertMethod) {
            return ListConvertType(sourceList, convertMethod, false);
        }
        /// <summary>
        /// Dictionary字典序列 转 'RT'类型数据结果 排除指定的错误项
        /// </summary>
        public static RT[] ListConvertType<RT, STKey, STValue>(this Dictionary<STKey, STValue> sourceList, ConvertTypeDelegate<RT, KeyValuePair<STKey, STValue>> convertMethod, RT errorValue) {
            return ListConvertType(sourceList, convertMethod, true, errorValue: errorValue);
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
            if (!CheckData.IsObjectNull(expression))
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


        /// <summary>
        /// 获取枚举类型所有Int值
        /// </summary>
        /// <typeparam name="E">枚举类型</typeparam>
        public static int[] EnumToInts<E>() {
            try {
                Array arr = Enum.GetValues(typeof(E));
                return arr.Cast<int>().ToArray();
            } catch (Exception) {
                return new int[] { };
            }
        }
        #endregion
    }
}
