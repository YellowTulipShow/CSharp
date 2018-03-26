using System;
using System.Collections.Generic;
using System.Text;

namespace CSharp.LibrayFunction
{
    /// <summary>
    /// 常用数据
    /// </summary>
    public class CommonData
    {
        /// <summary>
        /// 创建一个全局的随机数生成器 可以确保在调用生成随机数时, 非重复!
        /// </summary>
        private static readonly Random R = new Random();

        #region ====== ASCII Code: ======
        /// <summary>
        /// ASCII 所有常用 字符 33-127
        /// </summary>
        public static char[] ASCII_ALL() {
            return ASCII_IndexRegion(33, 127);
        }
        /// <summary>
        /// ASCII 常用文本字符
        /// </summary>
        public static char[] ASCII_WordText() {
            List<char> charArr = new List<char>();
            charArr.AddRange(ASCII_Number());
            charArr.AddRange(ASCII_LowerEnglish());
            charArr.AddRange(ASCII_UpperEnglish());
            return charArr.ToArray();
        }
        /// <summary>
        /// 阿拉伯数字
        /// </summary>
        public static char[] ASCII_Number() {
            return ASCII_IndexRegion(48, 58);
        }
        /// <summary>
        /// 大写英文
        /// </summary>
        public static char[] ASCII_UpperEnglish() {
            return ASCII_IndexRegion(65, 91);
        }
        /// <summary>
        /// 小写英文
        /// </summary>
        public static char[] ASCII_LowerEnglish() {
            return ASCII_IndexRegion(97, 123);
        }
        /// <summary>
        /// 特别字符
        /// </summary>
        public static char[] ASCII_Special() {
            List<char> charArr = new List<char>();
            charArr.AddRange(ASCII_IndexRegion(33, 48));
            charArr.AddRange(ASCII_IndexRegion(58, 65));
            charArr.AddRange(ASCII_IndexRegion(91, 97));
            charArr.AddRange(ASCII_IndexRegion(123, 127));
            return charArr.ToArray();
        }
        /// <summary>
        /// ASCII 码指定范围获取对应的字符
        /// </summary>
        /// <param name="min">最小值(包含)</param>
        /// <param name="max">最大值(不包含)</param>
        public static char[] ASCII_IndexRegion(int min, int max) {
            List<char> cl = new List<char>();
            byte[] array = new byte[1];
            for (int i = min; i < max; i++) {
                array[0] = (byte)i; //ASCII码强制转换二进制
                string str = Encoding.ASCII.GetString(array);
                cl.Add(Convert.ToChar(str));
            }
            return cl.ToArray();
        }
        #endregion

        #region ====== Random: ======
        /// <summary>
        /// 随机字符串
        /// </summary>
        /// <param name="max_charlength">指定字符个数, 默认32个</param>
        /// <returns>拼接结果</returns>
        public static string Random_String(int max_charlength = 32) {
            return Random_String(ASCII_ALL(), max_charlength);
        }
        /// <summary>
        /// 拼接随机字符串
        /// </summary>
        /// <param name="source">指定字符进行拼接</param>
        /// <returns>拼接结果</returns>
        public static string Random_String(char[] source) {
            return Random_String(source, source.Length);
        }
        /// <summary>
        /// 拼接随机字符串
        /// </summary>
        /// <param name="source">指定字符进行拼接</param>
        /// <param name="max_charlength">指定字符个数</param>
        /// <returns>拼接结果</returns>
        public static string Random_String(char[] source, int max_charlength) {
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
        /// 随机选取其中一个选项
        /// </summary>
        /// <typeparam name="T">数据类型</typeparam>
        /// <param name="source">数据源</param>
        /// <returns>结果选项, 数据源为空返回:数据类型默认值</returns>
        public static T Random_Item<T>(T[] source) {
            if (CheckData.IsSizeEmpty(source)) {
                return default(T);
            }
            return source[R.Next(0, source.Length)];
        }

        /// <summary>
        /// 随机获取日期
        /// </summary>
        public static DateTime Random_DateTime() {
            int year = R.Next(1, 9999 + 1);
            int month = R.Next(1, 12 + 1);
            int day = R.Next(1, DateTime.DaysInMonth(year, month) + 1);
            int hour = R.Next(0, 23 + 1);
            int minute = R.Next(0, 59 + 1);
            int second = R.Next(0, 59 + 1);
            int millisecond = R.Next(0, 999 + 1);
            return new DateTime(year, month, day, hour, minute, second, millisecond);
        }
        #endregion
    }
}
