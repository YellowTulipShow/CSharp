using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using YTS.Tools;

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
            CaseModel[] case_list = new Libray().InitCaseSource();
            if (CheckData.IsSizeEmpty(case_list)) {
                Console.WriteLine(CaseSourceNullErrorMsg);
                return;
            }
            if (AnalyticCaseModel(case_list)) {
                Console.WriteLine(@"[+] 测试成功, 完美!");
            } else {
                Console.WriteLine(@"[-] 测试含有错误已停止!");
            }
        }

        /// <summary>
        /// 解析执行实例模型
        /// </summary>
        private static bool AnalyticCaseModel(CaseModel[] cases) {
            foreach (CaseModel model in cases) {
                Stopwatch stopwatch = new Stopwatch();
                stopwatch.Start(); // 开始
                bool isby = model.ExeEvent();
                stopwatch.Stop(); // 结束
                TimeSpan runtimeSpan = stopwatch.Elapsed;
                if (isby) {
                    Console.WriteLine("[+] Name: [{0}] 成功 Success Time: {1}s", model.NameSign, runtimeSpan.TotalSeconds);
                } else {
                    Console.WriteLine("[-] Name: [{0}] 失败 Error Time: {1}s", model.NameSign, runtimeSpan.TotalSeconds);
                    return false;
                }

                // 执行含有子方法
                if (!CheckData.IsSizeEmpty(model.SonCases)) {
                    if (!AnalyticCaseModel(model.SonCases)) {
                        return false;
                    }
                }
            }
            return true;
        }
    }
}
