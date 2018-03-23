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
        private static readonly string CaseSourceNullErrorMsg = @"(→_→) => 没有设置好的需要实例子弹, 怎么打仗? 快跑吧~ running~ running~ running~ ";

        internal static void Main(string[] args) {
            Console.OutputEncoding = Encoding.Unicode;
            do {
                ExecuteCaseText();
            } while (IsRepeatExecute());
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

        private static void ExecuteCaseText() {
            CaseModel[] absCaseArray = new Libray().InitCaseSource();
            if (CheckData.IsSizeEmpty(absCaseArray)) {
                Console.WriteLine(CaseSourceNullErrorMsg);
                return;
            }
            foreach (CaseModel caseitem in absCaseArray) {
                AnalyticAbsCases(caseitem, new string[] {});
            }
        }

        private static void AnalyticAbsCases(CaseModel absCase, string[] tabs_arr) {
            StatisticsRunTime(absCase, tabs_arr);
            if (CheckData.IsSizeEmpty(absCase.SonCases) || tabs_arr.Length > 50) {
                return;
            }
            for (int i = 0; i < absCase.SonCases.Length; i++) {
                List<string> tabs_now = new List<string>(tabs_arr);
                tabs_now.Add(@"    ");
                AnalyticAbsCases(absCase.SonCases[i], tabs_now.ToArray());
            }
        }

        private static void StatisticsRunTime(CaseModel caseitem, string[] tabs_arr) {
            string father_symbol = ConvertTool.IListToString(tabs_arr, string.Empty);
            Print.IndentationCharString = string.Format(@"{0}┌ ", father_symbol);
            Print.WriteLine(caseitem.NameSign);

            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start(); // 开始

            Print.IndentationCharString = string.Format(@"{0}│    ", father_symbol);
            caseitem.ExeEvent();

            stopwatch.Stop(); // 结束
            TimeSpan runtimeSpan = stopwatch.Elapsed;

            Print.IndentationCharString = string.Format(@"{0}└ ", father_symbol);
            Print.WriteLine(string.Format("运行时间: {0}", runtimeSpan.TotalSeconds));
            Print.WriteLine();
        }
    }
}
