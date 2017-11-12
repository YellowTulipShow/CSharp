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

        public static bool IsRepeatExecute() {
            WriteLine("如需重复执行, 请输入 按键 0 ! 否则退出!");
            string v = Console.ReadLine();
            WriteLine("====== 下一次执行内容: ======\n");
            return (v == "0") ? true : false;
        }

        public static void EndProgram() {
            Console.ReadLine();
        }
    }
}
