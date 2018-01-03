using CSharp.LibrayFunction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Test.ConsoleProgram
{
    internal class MainProram
    {
        internal static void Main(string[] args) {
            do {
                ExecuteCaseText();
            } while (IsRepeatExecute());
        }
        internal static void ExecuteCaseText() {
            ITestCase[] tcs = new TestCaseLibray().GetTestCase();

            if (CheckData.IsSizeEmpty(tcs)) {
                Console.WriteLine(@"没有可以测试执行的实例~~~");
            }

            foreach (ITestCase iTC in tcs) {
                Console.WriteLine("");
                Console.WriteLine("===测试开始: {0} ===", iTC.TestNameSign());

                DelegateTimeTest dtt = new DelegateTimeTest();
                dtt.AddEventHandlers(new DelegateTimeTest.EventHandler[] {
                    iTC.TestMethod
                });
                dtt.ExecuteEventHandler();

                Console.WriteLine("============================ 运行时间: {0}", dtt.GetRunTimeTotalSeconds());
                Console.WriteLine("");
            }
        }

        private static bool IsRepeatExecute() {
            Console.WriteLine("");
            Console.WriteLine("请输入命令: Q(退出) R(重复执行)");
            ConsoleKeyInfo keyinfo = Console.ReadKey(false);
            Console.WriteLine("");
            switch (keyinfo.Key) {
                case ConsoleKey.Q:
                    return false;
                case ConsoleKey.R:
                    return true;
                default:
                    return IsRepeatExecute();
            }
        }
    }
}
