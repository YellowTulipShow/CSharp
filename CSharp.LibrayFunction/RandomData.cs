using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CSharp.LibrayFunction
{
    public static class RandomData
    {
        /// <summary>
        /// 创建一个全局的随机数生成器 可以确保在调用生成随机数时, 非重复!
        /// </summary>
        public static readonly Random R = new Random();

        /// <summary>
        /// 随机获取布尔值
        /// </summary>
        public static bool GetBoolean() {
            return GetItem(new bool[] { true, false });
        }

        /// <summary>
        /// 随机选取其中一个选项
        /// </summary>
        /// <typeparam name="T">数据类型</typeparam>
        /// <param name="source">数据源</param>
        /// <returns>结果选项, 数据源为空返回:数据类型默认值</returns>
        public static T GetItem<T>(T[] source) {
            if (CheckData.IsSizeEmpty(source)) {
                return default(T);
            }
            return source[R.Next(0, source.Length)];
        }

        /// <summary>
        /// 拼接随机字符串
        /// </summary>
        /// <param name="source">指定字符进行拼接</param>
        /// <param name="max_charlength">指定字符个数</param>
        /// <returns>拼接结果</returns>
        public static string GetString(char[] source, int max_charlength) {
            if (CheckData.IsSizeEmpty(source)) {
                return string.Empty;
            }
            StringBuilder strbu = new StringBuilder();
            for (int i = 0; i < max_charlength; i++) {
                strbu.Append(source[R.Next(0, source.Length)]);
            }
            return strbu.ToString();
        }
        /// <summary>
        /// 随机字符串
        /// </summary>
        /// <param name="max_charlength">指定字符个数, 默认32个</param>
        /// <returns>拼接结果</returns>
        public static string GetString(int max_charlength = 32) {
            return GetString(CommonData.ASCII_ALL(), max_charlength);
        }
        /// <summary>
        /// 拼接随机字符串
        /// </summary>
        /// <param name="source">指定字符进行拼接</param>
        /// <returns>拼接结果</returns>
        public static string GetString(char[] source) {
            return GetString(source, source.Length);
        }

        /// <summary>
        /// 随机获取日期, 指定时间范围区间
        /// </summary>
        public static DateTime GetDateTime(DateTime mintime, DateTime maxtime) {
            if (mintime > maxtime) {
                DateTime zhong = mintime;
                mintime = maxtime;
                maxtime = zhong;
            }
            TimeSpan ts = maxtime - mintime;
            int add_hour = ts.Hours == 0 ? 0 : R.Next(0, ts.Hours + 1);
            int add_minute = ts.Minutes == 0 ? 0 : R.Next(0, ts.Minutes + 1);
            int add_second = ts.Seconds == 0 ? 0 : R.Next(0, ts.Seconds + 1);
            int add_millisecond = ts.Milliseconds == 0 ? 0 : R.Next(0, ts.Milliseconds + 1);

            DateTime rt = mintime.AddHours(add_hour);
            int year = rt.Year;
            int month = rt.Month;
            int day = rt.Day;
            int hour = rt.Hour;
            int minute = rt.Minute;
            int second = rt.Second;
            int millisecond = rt.Millisecond;

            if (add_hour < ts.Hours) {
                minute = R.Next(0, 60);
                second = R.Next(0, 60);
                millisecond = R.Next(0, 1000);
            } else {
                rt = rt.AddMinutes(add_minute);
                if (add_minute < ts.Minutes) {
                    minute = rt.Minute;
                    second = R.Next(0, 60);
                    millisecond = R.Next(0, 1000);
                } else {
                    rt = rt.AddSeconds(add_second);
                    if (add_second < ts.Seconds) {
                        minute = rt.Minute;
                        second = rt.Second;
                        millisecond = R.Next(0, 1000);
                    } else {
                        rt.AddMilliseconds(add_millisecond);
                        if (add_millisecond < ts.Milliseconds) {
                            minute = rt.Minute;
                            second = rt.Second;
                            millisecond = rt.Millisecond;
                        }
                    }
                }
            }
            year = rt.Year;
            month = rt.Month;
            day = rt.Day;
            hour = rt.Hour;
            //minute = rt.Minute;
            //second = rt.Second;
            //millisecond = rt.Millisecond;

            return new DateTime(year, month, day, hour, minute, second, millisecond);
        }
        /// <summary>
        /// 随机获取日期
        /// </summary>
        public static DateTime GetDateTime() {
            return GetDateTime(DateTime.MinValue, DateTime.MaxValue);
        }
        /// <summary>
        /// 随机获取日期, 指定最大时间区间
        /// </summary>
        public static DateTime GetDateTime(DateTime maxtime) {
            return GetDateTime(DateTime.MinValue, maxtime);
        }

        /// <summary>
        /// 随机获取 Int 值
        /// </summary>
        /// <param name="minval">最小值, 默认为 [int.MinValue + 1]</param>
        /// <param name="maxval">最大值, 默认为 [int.MaxValue] 计算时不取其值</param>
        /// <returns></returns>
        public static int GetInt(int minval = int.MinValue + 1, int maxval = int.MaxValue) {
            if (minval > maxval) {
                int zhong = minval;
                minval = maxval;
                maxval = zhong;
            }
            return R.Next(minval, maxval);
        }

        /// <summary>
        /// 随机获取 double 值, 范围在 0.0 和 0.1 之间, 
        /// </summary>
        public static double GetDouble(int multiplication_number = 1000) {
            double result = R.NextDouble() * multiplication_number;
            if (GetBoolean()) {
                result *= -1;
            }
            return result;
        }

        /// <summary>
        /// 获取中文随机字符串
        /// </summary>
        /// <param name="length">长度</param>
        public static string GetChineseString(int length = 5) {
            StringBuilder result_str = new StringBuilder();
            for (int i = 0; i < length; i++) {
                int hexa_int_min_sign = CommonData.Unicode_Chinese_MIN_Decimal();
                int hexa_int_max_sign = CommonData.Unicode_Chinese_MAX_Decimal();
                int random_value = R.Next(hexa_int_min_sign, hexa_int_max_sign + 1);
                string random_heax_string = ConvertTool.DecimalToHexadecimal(random_value);
                string unicode_format_str = ConvertTool.Unicode_Format_String(random_heax_string);
                string chinese_char = ConvertTool.UnicodeToGB2312(unicode_format_str);
                result_str.Append(chinese_char);
            }
            return result_str.ToString();
        }
    }
}
