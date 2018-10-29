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
                returnString.Append(ObjectToString(obj));
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
        public static string[] ToArrayList(string strValue, IConvertible symbolSign) {
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
        /// 获取列表范围
        /// </summary>
        /// <typeparam name="T">数据类型</typeparam>
        /// <param name="source">数据源</param>
        /// <param name="length">每页数量</param>
        /// <param name="index">页面索引</param>
        /// <returns>获取 </returns>
        public static T[] GetIListRange<T>(IList<T> source, int index, int length) {
            /*
                10条 1页 开始: 1 结束: 10

                10条 2页 开始: 11 结束: 20

                10条 3页 开始: 21 结束: 30

                x条 n页 开始: (n-1)*x + 1 结束 n*x

                8条 1页 开始: 1 结束: 8

                8条 2页 开始: 9 结束: 16
             */

            length = length <= 1 ? 10 : length;
            int max_index = source.Count / length;
            int superfluous_count = source.Count % length;
            int exe_index = index < 1 ? 1 : index;
            int exe_count = length;
            if (exe_index == max_index + 1) {
                exe_count = superfluous_count;
            } else if (exe_index > max_index + 1) {
                exe_count = 0;
            } else {
                exe_count = length;
            }
            int startpoint = exe_count <= 0 ? 0 : (exe_index - 1) * length;

            List<T> list = new List<T>(source);
            return list.GetRange(startpoint, exe_count).ToArray();
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
        public static RT[] ListConvertType<RT, ST>(IList<ST> sourceList, Converter<ST, RT> convertMethod) {
            return ListConvertType(sourceList, convertMethod, false);
        }
        /// <summary>
        /// 'ST'类型数组 转 'RT'类型数据结果 排除指定的错误项
        /// </summary>
        public static RT[] ListConvertType<RT, ST>(IList<ST> sourceList, Converter<ST, RT> convertMethod, RT errorValue) {
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
        /// 将 不确定类型数据 转换为 指定数据类型值
        /// </summary>
        /// <param name="type">指定数据类型</param>
        /// <param name="ov">不确定类型数据</param>
        /// <returns>确定类型数据</returns>
        public static object ObjectToObject(Type type, object ov) {
            if (CheckData.IsObjectNull(type)) {
                return ov;
            }
            if (CheckData.IsObjectNull(ov)) {
                return type.IsValueType ? Activator.CreateInstance(type) : null;
            }

            if (CheckData.IsTypeEqualDepth(type, typeof(int), true)) {
                return ObjectToInt(ov, default(int));
            }
            if (CheckData.IsTypeEqualDepth(type, typeof(Enum), true)) {
                if (ov.GetType().IsEnum) {
                    return (int)ov;
                } else {
                    return ObjectToInt(ov, default(int));
                }
            }
            if (CheckData.IsTypeEqualDepth(type, typeof(float), true)) {
                return ObjectToFloat(ov, default(float));
            }
            if (CheckData.IsTypeEqualDepth(type, typeof(double), true)) {
                return ObjectToDouble(ov, default(double));
            }
            if (CheckData.IsTypeEqualDepth(type, typeof(DateTime), true)) {
                return ObjectToDateTime(ov, default(DateTime));
            }
            if (CheckData.IsTypeEqualDepth(type, typeof(bool), true)) {
                return ObjectToBool(ov, default(bool));
            }
            return ov;
        }

        /// <summary>
        /// 将对象转换为String类型
        /// 特定的数据类型将进行格式化处理
        /// 如为空, 则返回 string.Empty
        /// </summary>
        /// <param name="ov">通用object对象数据</param>
        /// <returns>格式化/ToString得到的string结果</returns>
        public static string ObjectToString(object ov) {
            if (CheckData.IsObjectNull(ov)) {
                return string.Empty;
            }
            Type vt = ov.GetType();
            if (CheckData.IsTypeEqualDepth(vt, typeof(string), true)) {
                return ov.ToString();
            }
            if (CheckData.IsTypeEqualDepth(vt, typeof(DateTime), true)) {
                return ((DateTime)ov).ToString(Tools.Const.Format.DATETIME_SECOND);
            }
            if (vt.IsEnum || CheckData.IsTypeEqualDepth(vt, typeof(Enum), true)) {
                return ((int)ov).ToString();
            }
            return ov.ToString();
        }
        /// <summary>
        /// 将字符串去除前后多余空格
        /// </summary>
        public static string StringToStringTrim(string source) {
            return CheckData.IsStringNull(source) ? string.Empty : source.Trim();
        }


        /// <summary>
        /// 将对象转换为Int32类型
        /// </summary>
        /// <param name="expression">要转换的字符串</param>
        /// <param name="defValue">缺省值</param>
        /// <returns>转换后的int类型结果</returns>
        public static int ObjectToInt(object expression, int defValue) {
            if (!CheckData.IsObjectNull(expression))
                return StringToInt(expression.ToString(), defValue);
            return defValue;
        }
        /// <summary>
        /// 将字符串转换为Int32类型
        /// </summary>
        /// <param name="expression">要转换的字符串</param>
        /// <param name="defValue">缺省值</param>
        /// <returns>转换后的int类型结果</returns>
        public static int StringToInt(String expression, int defValue) {
            if (String.IsNullOrEmpty(expression) || expression.Trim().Length >= 11 || !Regex.IsMatch(expression.Trim(), @"^([-]|[0-9])[0-9]*(\.\w*)?$"))
                return defValue;

            int rv;
            if (Int32.TryParse(expression, out rv))
                return rv;

            return Convert.ToInt32(StringToFloat(expression, defValue));
        }


        /// <summary>
        /// Object型转换为decimal型
        /// </summary>
        /// <param name="expression">要转换的字符串</param>
        /// <param name="defValue">缺省值</param>
        /// <returns>转换后的decimal类型结果</returns>
        public static decimal ObjectToDecimal(object expression, decimal defValue) {
            if (expression != null)
                return StringToDecimal(expression.ToString(), defValue);

            return defValue;
        }
        /// <summary>
        /// string型转换为decimal型
        /// </summary>
        /// <param name="expression">要转换的字符串</param>
        /// <param name="defValue">缺省值</param>
        /// <returns>转换后的decimal类型结果</returns>
        public static decimal StringToDecimal(String expression, decimal defValue) {
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
        /// <returns>转换后的float类型结果</returns>
        public static float ObjectToFloat(object expression, float defValue) {
            if (expression != null)
                return StringToFloat(expression.ToString(), defValue);

            return defValue;
        }
        /// <summary>
        /// string型转换为float型
        /// </summary>
        /// <param name="expression">要转换的字符串</param>
        /// <param name="defValue">缺省值</param>
        /// <returns>转换后的float类型结果</returns>
        public static float StringToFloat(String expression, float defValue) {
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
        /// Object型转换为double型
        /// </summary>
        /// <param name="expression">要转换的字符串</param>
        /// <param name="defvalue">缺省值</param>
        /// <returns>转换后的double类型结果</returns>
        public static double ObjectToDouble(object expression, double defvalue) {
            string s = ObjectToString(expression);
            return StringToDouble(s, defvalue);
        }
        /// <summary>
        /// string型转换为double型
        /// </summary>
        /// <param name="expression">要转换的字符串</param>
        /// <param name="defvalue">缺省值</param>
        /// <returns>转换后的double类型结果</returns>
        public static double StringToDouble(string expression, double defvalue) {
            if (CheckData.IsStringNull(expression)) {
                return defvalue;
            }
            double v = defvalue;
            return double.TryParse(expression, out v) ? v : defvalue;
        }


        /// <summary>
        /// object型转换为bool型
        /// </summary>
        /// <param name="expression">要转换的字符串</param>
        /// <param name="defValue">缺省值</param>
        /// <returns>转换后的bool类型结果</returns>
        public static bool ObjectToBool(object expression, bool defValue) {
            if (expression != null)
                return StringToBool(expression.ToString(), defValue);
            return defValue;
        }
        /// <summary>
        /// string型转换为bool型
        /// </summary>
        /// <param name="strValue">要转换的字符串</param>
        /// <param name="defValue">缺省值</param>
        /// <returns>转换后的bool类型结果</returns>
        public static bool StringToBool(String strValue, bool defValue) {
            if (strValue != null) {
                if (String.Compare(strValue, "true", true) == 0)
                    return true;
                else if (String.Compare(strValue, "false", true) == 0)
                    return false;
            }
            return defValue;
        }


        /// <summary>
        /// 获得 日期 00:00:00 点时间
        /// </summary>
        public static DateTime GetDateZeroHour(DateTime time) {
            return new DateTime(time.Year, time.Month, time.Day, 0, 0, 0);
        }
        /// <summary>
        /// 获得 日期 23:59:59 点时间
        /// </summary>
        public static DateTime GetDateTwoFourHour(DateTime time) {
            return new DateTime(time.Year, time.Month, time.Day, 23, 59, 59);
        }

        /// <summary>
        /// 将对象转换为日期时间类型
        /// </summary>
        /// <param name="obj">要转换的对象</param>
        /// <param name="defValue">缺省值</param>
        /// <returns></returns>
        public static DateTime ObjectToDateTime(object obj, DateTime defValue) {
            if (CheckData.IsObjectNull(obj)) {
                return defValue;
            }
            if (CheckData.IsTypeEqual(obj.GetType(), typeof(DateTime))) {
                return (DateTime)obj;
            }
            string s = obj.ToString();
            if (CheckData.IsStringNull(s)) {
                return defValue;
            }
            DateTime time;
            return DateTime.TryParse(s, out time) ? time : defValue;
        }

        /// <summary>
        /// 将对象转换为日期时间类型
        /// </summary>
        /// <param name="obj">要转换的字符串</param>
        /// <param name="defValue">缺省值</param>
        /// <returns></returns>
        public static SqlDateTime ObjectToSqlDateTime(object obj, SqlDateTime defValue) {
            DateTime defTime = defValue.Value;
            DateTime resuTime = ObjectToDateTime(obj, defTime);
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
