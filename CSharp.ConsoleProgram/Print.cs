using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CSharp.ConsoleProgram
{
    /// <summary>
    /// 输出类
    /// </summary>
    public class Print
    {
        public static void WriteLine(object obj) {
            Console.WriteLine(obj);
        }

        public static String GetReadContent() {
            return Console.ReadLine();
        }
    }
}
