using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace YTS.Tools
{
    /// <summary>
    /// 随机数据:
    /// </summary>
    public static class RandomData
    {
        /// <summary>
        /// 创建一个全局的随机数生成器 可以确保在调用生成随机数时, 非重复!
        /// </summary>
        public static readonly Random R = new Random();

        #region ====== List: ======
        /// <summary>
        /// 随机选取其中一个选项
        /// </summary>
        /// <typeparam name="T">数据类型</typeparam>
        /// <param name="source">数据源</param>
        /// <returns>结果选项, 数据源为空返回:数据类型默认值</returns>
        public static T Item<T>(IList<T> source) {
            if (CheckData.IsSizeEmpty(source)) {
                return default(T);
            }
            return source[R.Next(0, source.Count)];
        }

        /// <summary>
        /// 打乱序列列表的元素顺序
        /// </summary>
        /// <typeparam name="T">数组类型</typeparam>
        /// <param name="slist">数据源序列</param>
        /// <param name="need_num">需要的结果数量 如小于等于零取值序列个数</param>
        /// <returns>结果序列</returns>
        public static T[] Sample<T>(IList<T> slist, int need_num = 0) {
            if (CheckData.IsSizeEmpty(slist)) {
                return new T[] { };
            }
            List<T> rlist = new List<T>();
            int slen_size = slist.Count;
            if (need_num <= 0) {
                need_num = slen_size;
            }
            if (need_num > slen_size) {
                rlist.AddRange(Sample(slist, need_num - slen_size));
                need_num = slen_size;
            }
            while (need_num > 0) {
                int index = GetInt(0, need_num);
                need_num -= 1;
                rlist.Add(slist[index]);
                T C = slist[index];
                slist[index] = slist[need_num];
                slist[need_num] = C;
            }
            return rlist.ToArray();
        }
        #endregion

        #region ====== Basic Data Type: ======
        /// <summary>
        /// 随机获取布尔值
        /// </summary>
        public static bool GetBoolean() {
            return Item(new bool[] { true, false });
        }

        /// <summary>
        /// 随机获取 Int 值
        /// </summary>
        /// <returns>取自: int.MinValue+1 和 int.MaxValue-1 并包括</returns>
        public static int GetInt() {
            return GetInt(int.MinValue + 1, int.MaxValue);
        }

        /// <summary>
        /// 随机获取 Int 值
        /// </summary>
        /// <param name="max">最大值(不取该上界值)</param>
        /// <returns>取自: 0 和 max-1 并包括</returns>
        public static int GetInt(int max) {
            return GetInt(0, max);
        }

        /// <summary>
        /// 随机获取 Int 值
        /// </summary>
        /// <param name="min">最小值</param>
        /// <param name="max">最大值(不取该上界值)</param>
        /// <returns>取自: min 和 max-1 并包括</returns>
        public static int GetInt(int min, int max) {
            if (min > max) {
                int zhong = min;
                min = max;
                max = zhong;
            }
            return R.Next(min, max);
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
        #endregion

        #region ====== String: ======
        /// <summary>
        /// 拼接随机字符串
        /// </summary>
        /// <param name="source">指定字符进行拼接</param>
        /// <param name="max_charlength">指定字符个数</param>
        /// <returns>拼接结果</returns>
        public static string GetString(char[] source, int sum_length) {
            if (CheckData.IsSizeEmpty(source)) {
                return string.Empty;
            }
            StringBuilder strbu = new StringBuilder();
            for (int i = 0; i < sum_length; i++) {
                strbu.Append(Item(source));
            }
            return strbu.ToString();
        }
        /// <summary>
        /// 随机字符串
        /// </summary>
        /// <param name="max_charlength">指定字符个数, 默认32个</param>
        /// <returns>拼接结果</returns>
        public static string GetString(int sum_length = 32) {
            return GetString(CommonData.ASCII_ALL(), sum_length);
        }
        /// <summary>
        /// 拼接随机字符串
        /// </summary>
        /// <param name="source">指定字符进行拼接</param>
        /// <returns>拼接结果</returns>
        public static string GetString(char[] source) {
            return GetString(source, source.Length);
        }
        #endregion

        #region ====== Chinese String: ======
        /// <summary>
        /// 获取中文随机字符串
        /// </summary>
        /// <param name="length">长度</param>
        public static string GetChineseString(int sum_length = 5) {
            StringBuilder result_str = new StringBuilder();
            for (int i = 0; i < sum_length; i++) {
                int hexa_int_min_sign = CommonData.Unicode_Chinese_MIN_Decimal();
                int hexa_int_max_sign = CommonData.Unicode_Chinese_MAX_Decimal();
                int random_value = R.Next(hexa_int_min_sign, hexa_int_max_sign + 1);
                string random_heax_string = ConvertTool.DecimalToHexadecimal(random_value);
                string unicode_format_str = ConvertTool.UnicodeFormatString(random_heax_string);
                string chinese_char = ConvertTool.UnicodeToGB2312(unicode_format_str);
                result_str.Append(chinese_char);
            }
            return result_str.ToString();
        }
        #endregion

        #region ====== Date Time: ======
        /// <summary>
        /// 随机获取日期, 指定时间范围区间
        /// </summary>
        public static DateTime GetDateTime(DateTime min, DateTime max) {
            if (min > max) {
                DateTime zhong = min;
                min = max;
                max = zhong;
            }
            int upstatue = 0;
            int r_Year = TimeRangeSelect(ref upstatue, 1, 9999 + 1, min.Year, max.Year);
            int r_Month = TimeRangeSelect(ref upstatue, 1, 12 + 1, min.Month, max.Month);
            int r_Day = TimeRangeSelect(ref upstatue, 1, CommonData.GetMaxDayCount(r_Year, r_Month) + 1, min.Day, max.Day);
            int r_Hour = TimeRangeSelect(ref upstatue, 0, 23 + 1, min.Hour, max.Hour);
            int r_Minute = TimeRangeSelect(ref upstatue, 0, 59 + 1, min.Minute, max.Minute);
            int r_Second = TimeRangeSelect(ref upstatue, 0, 59 + 1, min.Second, max.Second);
            int r_Millisecond = TimeRangeSelect(ref upstatue, 0, 999 + 1, min.Millisecond, max.Millisecond);
            return new DateTime(r_Year, r_Month, r_Day, r_Hour, r_Minute, r_Second, r_Millisecond);
        }
        private static int TimeRangeSelect(ref int upstatue, int min, int max, int start, int end) {
            if (upstatue == 4) {
                return R.Next(min, max);
            }

            int minvalue = (upstatue == 3) ? min : start;
            int maxvalue = (upstatue == 2) ? max - 1 : end;
            if (minvalue > maxvalue) {
                int zhong = minvalue;
                minvalue = maxvalue;
                maxvalue = zhong;
            }
            int result = R.Next(minvalue, maxvalue + 1);

            int selfstatus = 0;
            if (minvalue == result && result == maxvalue) {
                selfstatus = 1;
            }
            if (minvalue == result && result < maxvalue) {
                selfstatus = (upstatue == 3) ? 4 : 2;
            }
            if (minvalue < result && result == maxvalue) {
                selfstatus = (upstatue == 2) ? 4 : 3;
            }
            if (minvalue < result && result < maxvalue) {
                selfstatus = 4;
            }
            upstatue = (selfstatus < upstatue) ? upstatue : selfstatus;
            return result;
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
        #endregion

        #region ====== IP Address: ======
        /// <summary>
        /// 随机获取IPv4地址
        /// </summary>
        public static string IPv4() {
            List<int> list = new List<int>();
            for (int i = 0; i < 4; i++) {
                list.Add(GetInt(0, 255 + 1));
            }
            return ConvertTool.ToString(list, '.');
        }
        #endregion

        #region ====== Telephone: ======
        private readonly static string[] TelPrefixs = "134,135,136,137,138,139,150,151,152,157,158,159,130,131,132,155,156,133,153,180,181,182,183,185,186,176,187,188,189,177,178".Split(',');
        /// <summary>
        /// 获得十一位电话号
        /// </summary>
        public static string Telephone_ElevenBit() {
            return Item(TelPrefixs) + GetString(CommonData.ASCII_Number(), 8);
        }
        #endregion

        #region ====== Color: ======
        /// <summary>
        /// RGB颜色 六位字符串
        /// </summary>
        /// <param name="isNeedAdd_Hashtag">是否需要加 '#' 号</param>
        public static string RGBColor_SixDigitString(bool isNeedAdd_Hashtag = true) {
            char[] clist = CommonData.ASCII_Hexadecimal();
            StringBuilder str = new StringBuilder();
            if (isNeedAdd_Hashtag) {
                str.Append('#');
            }
            for (int i = 0; i < 6; i++) {
                str.Append(Item(clist));
            }
            return str.ToString();
        }
        public static Color ColorRGB() {
            const int min = 0;
            const int max = 255 + 1;
            int red = RandomData.GetInt(min, max);
            int green = RandomData.GetInt(min, max);
            int blue = RandomData.GetInt(min, max);
            Color nc = Color.FromArgb(red, green, blue);
            return nc;
        }
        public static Color ColorRGBA() {
            const int min = 0;
            const int max = 255 + 1;
            int alpha = RandomData.GetInt(min, max);
            Color nc = Color.FromArgb(alpha, ColorRGB());
            return nc;
        }
        #endregion
    }
}
