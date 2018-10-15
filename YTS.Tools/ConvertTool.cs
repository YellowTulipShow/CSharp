using System;
using System.Linq;
using System.Data;
using System.Data.SqlTypes;
using System.Text;
using System.Text.RegularExpressions;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;

namespace YTS.Tools
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
                returnString.Append(ObjToString(obj));
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

        /// <summary>
        /// 截取 数组元素
        /// </summary>
        /// <typeparam name="T">数组的数据类型</typeparam>
        /// <param name="list">数据源</param>
        /// <param name="start_sign">开始下标标识</param>
        /// <param name="end_sign">结束下标标识, 结果不包含</param>
        /// <returns>结果</returns>
        public static T[] Interception<T>(this T[] list, int start_sign, int end_sign) {
            List<T> RL = new List<T>();
            if (start_sign > end_sign) {
                int zhong = start_sign;
                start_sign = end_sign;
                end_sign = zhong;
            }
            if (start_sign > list.Length) {
                start_sign = list.Length;
            }
            if (end_sign > list.Length) {
                end_sign = list.Length;
            }
            for (int i = start_sign; i < end_sign; i++) {
                try {
                    RL.Add(list[i]);
                } catch (Exception) {
                    continue;
                }
            }
            return RL.ToArray();
        }

        /// <summary>
        /// 获取列表范围
        /// </summary>
        /// <typeparam name="T">数据类型</typeparam>
        /// <param name="source">数据源</param>
        /// <param name="count">每页数量</param>
        /// <param name="index">页面索引</param>
        /// <returns>获取 </returns>
        public static T[] GetIListRange<T>(IList<T> source, int index, int count) {
            index = index < 1 ? 1 : index;
            count = count <= 1 ? 10 : count;

            int max_index = source.Count / count;

            int startindex = index;
            int rangecount = count;
            if (index >= max_index + 1) {
                startindex = max_index * count;
                rangecount = source.Count % count;
            }

            List<T> list = new List<T>(source);
            return list.GetRange(startindex, rangecount).ToArray();
            return list.GetRange(0, count).ToArray();
        }
        #endregion

        #region === Type Convert ===
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
        private static RT[] ListConvertType<RT, SLT, SIT>(SLT sourceslist, Converter<SIT, RT> convertMethod,
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
        public static RT[] ListConvertType<RT, ST>(ST[] sourceList, Converter<ST, RT> convertMethod) {
            return ListConvertType(sourceList, convertMethod, false);
        }
        /// <summary>
        /// 'ST'类型数组 转 'RT'类型数据结果 排除指定的错误项
        /// </summary>
        public static RT[] ListConvertType<RT, ST>(ST[] sourceList, Converter<ST, RT> convertMethod, RT errorValue) {
            return ListConvertType(sourceList, convertMethod, true, errorValue: errorValue);
        }
        /// <summary>
        /// DataTable表 转 'RT'类型数据结果
        /// </summary>
        public static RT[] ListConvertType<RT>(DataTable sourceList, Converter<DataRow, RT> convertMethod) {
            return ListConvertType(sourceList.Rows, convertMethod, false);
        }
        /// <summary>
        /// DataTable表 转 'RT'类型数据结果 排除指定的错误项
        /// </summary>
        public static RT[] ListConvertType<RT>(DataTable sourceList, Converter<DataRow, RT> convertMethod, RT errorValue) {
            return ListConvertType(sourceList.Rows, convertMethod, true, errorValue: errorValue);
        }
        /// <summary>
        /// Dictionary字典序列 转 'RT'类型数据结果
        /// </summary>
        public static RT[] ListConvertType<RT, STKey, STValue>(Dictionary<STKey, STValue> sourceList, Converter<KeyValuePair<STKey, STValue>, RT> convertMethod) {
            return ListConvertType(sourceList, convertMethod, false);
        }
        /// <summary>
        /// Dictionary字典序列 转 'RT'类型数据结果 排除指定的错误项
        /// </summary>
        public static RT[] ListConvertType<RT, STKey, STValue>(Dictionary<STKey, STValue> sourceList, Converter<KeyValuePair<STKey, STValue>, RT> convertMethod, RT errorValue) {
            return ListConvertType(sourceList, convertMethod, true, errorValue: errorValue);
        }
        #endregion

        #region === Data Type Convert ===
        /// <summary>
        /// 转换为 Json 格式的字符串
        /// </summary>
        /// <param name="obj">数据源</param>
        /// <returns>Json 字符串</returns>
        public static string ToJson(this object obj) {
            return JSON.SerializeObject(obj);
        }


        /// <summary>
        /// 将对象转换为String类型, 区别在于判断是否为Null
        /// </summary>
        public static string ObjToString(object source) {
            return CheckData.IsObjectNull(source) ? string.Empty : source.ToString();
        }
        /// <summary>
        /// 将字符串去除前后多余空格
        /// </summary>
        public static string StrToStrTrim(string source) {
            return CheckData.IsStringNull(source) ? string.Empty : source.Trim();
        }


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
        /// 汉字转换为Unicode编码 (网络代码)
        /// </summary>
        /// <param name="gb2312_str">要编码的汉字字符串</param>
        /// <returns>Unicode编码的的字符串</returns>
        public static string GB2312ToUnicode(string gb2312_str) {
            if (CheckData.IsStringNull(gb2312_str)) {
                return string.Empty;
            }
            byte[] bts = Encoding.Unicode.GetBytes(gb2312_str);
            string r = "";
            for (int i = 0; i < bts.Length; i += 2)
                r += "\\u" + bts[i + 1].ToString("x").PadLeft(2, '0') + bts[i].ToString("x").PadLeft(2, '0');
            return r;
        }
        /// <summary>
        /// 将Unicode编码转换为汉字字符串 (网络代码)
        /// </summary>
        /// <param name="unicode_str">Unicode编码字符串</param>
        /// <returns>汉字字符串</returns>
        public static string UnicodeToGB2312(string unicode_str) {
            if (CheckData.IsStringNull(unicode_str)) {
                return string.Empty;
            }
            string r = "";
            MatchCollection mc = Regex.Matches(unicode_str, @"\\u([\w]{2})([\w]{2})", RegexOptions.Compiled | RegexOptions.IgnoreCase);
            byte[] bts = new byte[2];
            foreach (Match m in mc) {
                bts[0] = (byte)int.Parse(m.Groups[2].Value, NumberStyles.HexNumber);
                bts[1] = (byte)int.Parse(m.Groups[1].Value, NumberStyles.HexNumber);
                r += Encoding.Unicode.GetString(bts);
            }
            return r;
        }


        /// <summary>
        /// 任意字符串转十六进制字符数据
        /// </summary>
        /// <param name="obj_string">任意字符串</param>
        /// <returns></returns>
        public static string StringToHexadecimal(string obj_string) {
            if (CheckData.IsStringNull(obj_string)) {
                return string.Empty;
            }
            return Regex.Replace(obj_string, @"[^0-9a-fA-F]", "", RegexOptions.IgnoreCase).ToLower();
        }
        /// <summary>
        /// 十进制Int32类型值 转 十六进制字符串
        /// </summary>
        /// <param name="decimal_value">十进制Int32类型值</param>
        /// <returns>十六进制字符串</returns>
        public static string DecimalToHexadecimal(Int32 decimal_value) {
            return decimal_value.ToString("x");
        }
        /// <summary>
        /// 十六进制字符串 转 十进制Int32类型值
        /// </summary>
        /// <param name="hexadecimal_string">十六进制字符串</param>
        /// <returns>十进制Int32类型值</returns>
        public static Int32 HexadecimalToDecimal(string hexadecimal_string, int deferrorval = 0) {
            hexadecimal_string = StringToHexadecimal(hexadecimal_string);
            if (CheckData.IsStringNull(hexadecimal_string)) {
                return deferrorval;
            }
            return Int32.Parse(hexadecimal_string, System.Globalization.NumberStyles.HexNumber);
        }
        #endregion

        /// <summary>
        /// 十六进制值转为Unicode格式字符串
        /// </summary>
        /// <param name="hexadecimal_string">十六进制字符串</param>
        /// <returns></returns>
        public static string UnicodeFormatString(string hexadecimal_string) {
            string result_str = hexadecimal_string; // string 引用地址的问题
            result_str = StringToHexadecimal(result_str);
            if (CheckData.IsStringNull(result_str)) {
                return string.Empty;
            }
            if (result_str.Length < 4) {
                int cha = 4 - result_str.Length;
                for (int i = 0; i < cha; i++) {
                    result_str = @"0" + result_str;
                }
            } else if (result_str.Length > 4) {
                result_str = result_str.Substring(result_str.Length - 4, 4);
            }
            return string.Format("\\u{0}", result_str);
        }

        /// <summary>
        /// 过滤禁用的字符
        /// </summary>
        /// <param name="source">需要处理的字符串</param>
        /// <param name="disable_chars">禁用字符列表</param>
        /// <returns>结果</returns>
        public static string FilterDisableChars(string source, char[] disable_chars) {
            if (CheckData.IsStringNull(source)) {
                return string.Empty;
            }
            if (CheckData.IsSizeEmpty(disable_chars)) {
                return source;
            }
            foreach (char c in disable_chars) {
                source = source.Replace(c.ToString(), "");
            }
            return source;
        }

        public static string FromASCIIByteArray(byte[] characters) {
            ASCIIEncoding encoding = new ASCIIEncoding();
            string constructedString = encoding.GetString(characters);
            return (constructedString);
        }
        public static string FromUnicodeByteArray(byte[] characters) {
            UnicodeEncoding encoding = new UnicodeEncoding();
            string constructedString = encoding.GetString(characters);
            return (constructedString);
        }
    }
}
