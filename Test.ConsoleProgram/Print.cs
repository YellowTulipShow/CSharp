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
        /// <summary>
        /// 缩进符个数 默认 使用常量: INDENTATIONCHARCOUNTDEFAULTVALUE
        /// </summary>
        public static ushort IndentationCharCount = 0;
        public const ushort INDENTATIONCHARCOUNT_DEFAULTVALUE = 0;

        private static string IndentationCharString() {
            StringBuilder str = new StringBuilder();
            for (int i = 0; i < IndentationCharCount; i++) {
                str.Append(@"    ");
            }
            return str.ToString();
        }

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
            Console.WriteLine(IndentationCharString() + value);
        }
        public static void WriteLine(string format, params object[] arg) {
            Console.WriteLine(IndentationCharString() + format, arg);
        }
        #endregion
    }
}
