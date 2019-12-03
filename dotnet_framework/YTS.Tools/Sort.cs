using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YTS.Tools
{
    /// <summary>
    /// System.Collections.Generic.List类的Sort()的排序方法
    /// </summary>
    public static class Sort
    {
        /// <summary>
        /// 插入排序
        /// </summary>
        /// <param name="intlist"></param>
        /// <returns></returns>
        private static int[] InsertionSort(int[] intlist) {
            int[] returnList = { };

            for (int j = 2; j < intlist.Length; j++) {
                int key = intlist[j];

                int i = j - 1;
                while (i > 0 && intlist[i] > key) {
                    intlist[i + 1] = intlist[i];
                    i = i - 1;
                }
                intlist[i + 1] = key;
            }

            return intlist;
        }


        #region === Comparison 泛型委托实现 ===
        /** 返回值含义: 
         * (x 小于 y) return 小于0
         * (x 等于 y) return 等于0
         * (x 大于 y) return 大于0
         */

        /// <summary>
        /// 排序 Int32 类型
        /// </summary>
        public static int Int(int x, int y) {
            return x == y ? 0 : x > y ? 1 : -1;
        }

        /// <summary>
        /// 排序 String 类型 实现 ASCII 码字符大小排序
        /// </summary>
        public static int String(string x, string y) {
            if (x.IsStringNull() || y.IsStringNull())
                return 0;
            bool resultBool = true;
            if (!x.Equals(y)) {
                ASCIIEncoding asciie = new ASCIIEncoding();
                byte[] byte_x = asciie.GetBytes(x.ToCharArray());
                byte[] byte_y = asciie.GetBytes(y.ToCharArray());
                resultBool = IsXGreaterThanY(byte_x, byte_y, 0);
                return resultBool ? 1 : -1;
            }
            return 0;
        }
        private static bool IsXGreaterThanY(byte[] byte_x, byte[] byte_y, int index) {
            if (byte_x.Length < index || byte_y.Length < index) {
                if (byte_x.Length < index) {
                    return true;
                }
                if (byte_y.Length < index) {
                    return false;
                }
            }
            return byte_x[index] == byte_y[index] ? IsXGreaterThanY(byte_x, byte_y, ++index) : byte_x[index] > byte_y[index];
        }
        #endregion
    }
}
