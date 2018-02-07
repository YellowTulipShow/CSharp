using CSharp.LibrayFunction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Test.ConsoleProgram
{
    internal class MainProram
    {
        internal static void Main(string[] args) {
            Console.OutputEncoding = Encoding.UTF8;
            do {
                ExecuteCaseText();
            } while (IsRepeatExecute());
        }

        private static void ExecuteCaseText() {
            ICase[] iCaseArray = AnalyticAbsCases(new CaseLibray().InitCaseSource(), true);

            if (CheckData.IsSizeEmpty(iCaseArray)) {
                Console.WriteLine(@"(→_→) => 没有设置好的需要实例子弹, 怎么打仗? 快跑吧~ running~ running~ running~ ");
            }

            foreach (ICase caseitem in iCaseArray) {
                Console.WriteLine(string.Empty);
                Console.WriteLine(@"===实例开始: {0} ===", caseitem.TestNameSign());

                DelegateTimeTest dtt = new DelegateTimeTest();
                dtt.SetEventHandlers(caseitem.TestMethod);
                dtt.ExecuteEventHandler();

                Console.WriteLine(@"============================ 运行时间: {0}", dtt.GetRunTimeTotalSeconds());
                Console.WriteLine(string.Empty);
            }
        }

        private static ICase[] AnalyticAbsCases(AbsCase[] absCases, bool isGetSonICase) {
            if (!isGetSonICase) {
                return absCases;
            }
            List<ICase> list = new List<ICase>();
            foreach (AbsCase item in absCases) {
                if (CheckData.IsObjectNull(item)) {
                    continue;
                }
                list.Add(item);
                ICase[] sonCase = item.SonTestCase();
                if (!CheckData.IsSizeEmpty(sonCase)) {
                    list.AddRange(sonCase);
                }
            }
            return list.ToArray();
        }

        private static bool IsRepeatExecute() {
            Console.WriteLine(string.Empty);
            Console.WriteLine(@"请输入命令: Q(退出) R(重复执行) C(清空屏幕)");
            ConsoleKeyInfo keyinfo = Console.ReadKey(false);
            Console.WriteLine(string.Empty);
            switch (keyinfo.Key) {
                case ConsoleKey.Q:
                    return false;
                case ConsoleKey.R:
                    return true;
                case ConsoleKey.C:
                    Console.Clear();
                    return IsRepeatExecute();
                default:
                    return IsRepeatExecute();
            }
        }
    }
}
