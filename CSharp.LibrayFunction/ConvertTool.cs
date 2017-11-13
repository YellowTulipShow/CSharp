using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

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
        public static String CombinationContent(params object[] objs) {
            StringBuilder returnString = new StringBuilder();
            foreach (object obj in objs) {
                returnString.Append(obj.ToString());
            }
            return returnString.ToString();
        }


        /// <summary>
        /// 数组列表转字符串
        /// </summary>
        /// <param name="arrayList">需要合并的字符串数组</param>
        /// <param name="Symbol">用于间隔内容的间隔符号</param>
        /// <returns></returns>
        public static String ArrayToString(String[] arrayList, Char Symbol) {
            StringBuilder strs = new StringBuilder();
            for (int i = 0; i < arrayList.Length; i++) {
                if (i != 0) {
                    strs.Append(Symbol);
                }
                strs.Append(arrayList[i]);
            }
            return strs.ToString();
        }
        /// <summary>
        /// 字符串转数组列表
        /// </summary>
        /// <param name="strValue">要转化的字符串</param>
        /// <param name="Symbol">用于分隔的间隔符号</param>
        /// <returns></returns>
        public static String[] StringToArray(String strValue, Char Symbol) {
            if (CheckData.IsStringNull(strValue))
                return new List<String>().ToArray();
            String[] strarr = strValue.Split(Symbol);
            return strarr;
        }


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
        /// String型转换为decimal型
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
        /// String型转换为float型
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
        /// 获得 time.Year time.Month time.Day 00:00:00 点时间
        /// </summary>
        public static DateTime GetTimeZero(DateTime time) {
            return new DateTime(time.Year, time.Month, time.Day, 0, 0, 0);
        }
        /// <summary>
        /// 获得 time.Year time.Month time.Day 23:59:59 点时间
        /// </summary>
        public static DateTime GetTimeTwoFour(DateTime time) {
            return new DateTime(time.Year, time.Month, time.Day, 23, 59, 59);
        }
        /// <summary>
        /// 将对象转换为日期时间类型
        /// </summary>
        /// <param name="obj">要转换的对象</param>
        /// <returns>转换后的int类型结果</returns>
        public static DateTime ObjectToDateTime(object obj) {
            return StrToDateTime(obj.ToString());
        }
        /// <summary>
        /// 将对象转换为日期时间类型
        /// </summary>
        /// <param name="obj">要转换的对象</param>
        /// <param name="defValue">缺省值</param>
        /// <returns>转换后的int类型结果</returns>
        public static DateTime ObjectToDateTime(object obj, DateTime defValue) {
            return StrToDateTime(obj.ToString(), defValue);
        }
        /// <summary>
        /// 将对象转换为日期时间类型
        /// </summary>
        /// <param name="str">要转换的字符串</param>
        /// <returns>转换后的int类型结果</returns>
        public static DateTime StrToDateTime(String str) {
            return StrToDateTime(str, DateTime.Now);
        }
        /// <summary>
        /// 将对象转换为日期时间类型
        /// </summary>
        /// <param name="str">要转换的字符串</param>
        /// <param name="defValue">缺省值</param>
        /// <returns>转换后的int类型结果</returns>
        public static DateTime StrToDateTime(String str, DateTime defValue) {
            if (!CheckData.IsStringNull(str)) {
                DateTime dateTime;
                if (DateTime.TryParse(str, out dateTime)) {
                    return dateTime;
                }
            }
            return defValue;
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
        /// String型转换为bool型
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
    }
}
