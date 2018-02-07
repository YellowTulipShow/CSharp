using CSharp.LibrayFunction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics;

namespace Test.ConsoleProgram
{
    internal class MainProram
    {
        internal static void Main(string[] args) {
            Console.OutputEncoding = Encoding.Unicode;
            do {
                ExecuteCaseText();
            } while (IsRepeatExecute());
        }

        private static bool IsRepeatExecute() {
            Print.WriteLine(string.Empty);
            Print.WriteLine(@"请输入命令: Q(退出) R(重复执行) C(清空屏幕)");
            ConsoleKeyInfo keyinfo = Console.ReadKey(false);
            Print.WriteLine(string.Empty);
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

        private static void ExecuteCaseText() {
            AbsCase[] absCaseArray = new CaseLibray().InitCaseSource();
            if (CheckData.IsSizeEmpty(absCaseArray)) {
                Console.WriteLine(@"(→_→) => 没有设置好的需要实例子弹, 怎么打仗? 快跑吧~ running~ running~ running~ ");
                return;
            }
            foreach (AbsCase caseitem in absCaseArray) {
                Print.WriteLine();
                AnalyticAbsCases(caseitem);
            }
        }

        private static void AnalyticAbsCases(AbsCase absCase) {
            StatisticsRunTime(absCase);
            Print.IndentationCharCount++;
            foreach (ICase item in absCase.SonCaseArray()) {
                StatisticsRunTime(item);
            }
            Print.IndentationCharCount--;
        }

        private static void StatisticsRunTime(ICase caseitem) {
            Print.WriteLine(@"===实例开始: {0} ===", caseitem.NameSign());

            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start(); // 开始

            Print.IndentationCharCount++;
            // 执行实例的主体方法内容
            caseitem.Method();
            Print.IndentationCharCount--;

            stopwatch.Stop(); // 结束
            TimeSpan runtimeSpan = stopwatch.Elapsed;

            Print.WriteLine(@"============================ 运行时间: {0}", runtimeSpan.TotalSeconds);
            Print.WriteLine();
        }
    }
}
