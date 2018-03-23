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
        #region ====== Random: ======
        /// <summary>
        /// 随机字符串
        /// </summary>
        /// <param name="max_charlength">指定字符个数, 默认500个</param>
        public static string Random_String(int max_charlength = 500) {
            return Random_String(ASCII_ALL(), max_charlength);
        }
        /// <summary>
        /// 随机字符串
        /// </summary>
        /// <param name="source">指定数据源</param>
        /// <returns></returns>
        public static string Random_String(char[] source) {
            return Random_String(source, source.Length);
        }
        /// <summary>
        /// 随机字符串
        /// </summary>
        /// <param name="source">指定数据源</param>
        /// <param name="max_charlength">指定字符个数</param>
        /// <returns></returns>
        public static string Random_String(char[] source, int max_charlength) {
            Random random = new Random();
            StringBuilder strbu = new StringBuilder();
            for (int i = 0; i < max_charlength; i++) {
                strbu.Append(source[random.Next(0, source.Length)]);
            }
            return strbu.ToString();
        }
        #endregion

        #region ====== ASCII Code: ======
        /// <summary>
        /// ASCII 所有常用 字符 33-127
        /// </summary>
        public static char[] ASCII_ALL() {
            return ASCII_IndexRegion(33, 127);
        }
        /// <summary>
        /// 阿拉伯数字
        /// </summary>
        public static char[] ASCII_Number() {
            return ASCII_IndexRegion(48, 58);
        }
        /// <summary>
        /// 小写英文
        /// </summary>
        public static char[] ASCII_LowerEnglish() {
            return ASCII_IndexRegion(65, 91);
        }
        /// <summary>
        /// 大写英文
        /// </summary>
        public static char[] ASCII_UpperEnglish() {
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
    }
}
