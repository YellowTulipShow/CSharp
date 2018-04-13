using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Test.ConsoleProgram
{
    /// <summary>
    /// 打印 自定义 类
    /// </summary>
    public static class Print
    {
        public static string IndentationCharString = string.Empty;

        #region === use Console Method ===
        public static void Write(object value) {
            Console.Write(value);
        }
        public static void Write(string format, params object[] arg) {
            Console.Write(format, arg);
        }
        public static void WriteLine() {
            Console.WriteLine();
        }
        public static void WriteLine(object value) {
            ShowText(value.ToString());
        }
        public static void WriteLine(string format) {
            ShowText(format);
        }
        public static void WriteLine(string format, params object[] arg) {
            string val = string.Format(format, arg);
            ShowText(val);
        }
        private static void ShowText(string value) {
            //value = value.Replace("\n", IndentationCharString + "\n");
            Console.WriteLine(IndentationCharString + value);
        }
        #endregion
    }
}
