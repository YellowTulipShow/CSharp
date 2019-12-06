using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YTSCSharp.Tools;

namespace Test.ConsoleProgram
{
    public class MainProgram
    {
        public static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            Console.InputEncoding = Encoding.UTF8;
            Console.Title = @"YTS.Test 控制台程序";

            RunTemp run = new RunTemp();
            do
            {
                run.ExecuteCases();
            } while (run.IsRepeatExecute());
        }

        private class RunTemp
        {
            public RunTemp() { }

            /// <summary>
            /// 是否重复执行
            /// </summary>
            public bool IsRepeatExecute()
            {
                Console.WriteLine(string.Empty);
                Console.WriteLine(@"请输入命令: Q(退出) R(重复执行) C(清空屏幕)");
                ConsoleKeyInfo keyinfo = Console.ReadKey(false);
                Console.WriteLine(string.Empty);
                switch (keyinfo.Key)
                {
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

            /// <summary>
            /// 执行测试实例集合
            /// </summary>
            public void ExecuteCases()
            {
                List<CaseModel> case_list = new Libray().GetALLCases();
                if (CheckData.IsSizeEmpty(case_list))
                {
                    Console.WriteLine(@"(→_→) => 并没有需要测试实例子弹, 怎么打仗? 快跑吧~ running~ running~ running~ ");
                    return;
                }
                if (AnalyticItem(case_list, string.Empty))
                {
                    Console.WriteLine(@"[+] 测试成功, 完美!");
                }
                else
                {
                    Console.WriteLine(@"[-] 测试含有错误已停止!");
                }
            }

            /// <summary>
            /// 递归解析测试实例集合
            /// </summary>
            /// <param name="cases">测试实例集合</param>
            /// <param name="upper_layer_name">上层级名称</param>
            /// <returns>是否成功</returns>
            public bool AnalyticItem(List<CaseModel> cases, string upper_layer_name)
            {
                upper_layer_name = ConvertTool.ToString(upper_layer_name);
                foreach (CaseModel model in cases)
                {
                    bool isSuccess = AnalyticItem(model, upper_layer_name);
                    if (!isSuccess)
                    {
                        return false;
                    }
                }
                return true;
            }

            public bool AnalyticItem(CaseModel model, string upper_layer_name)
            {
                // 获取名称
                string name = model.NameSign;
                if (!CheckData.IsStringNull(upper_layer_name))
                {
                    name = string.Format("{0}: {1}", upper_layer_name, name);
                    // 加入一个空行, 与上一条进行区分
                    Console.WriteLine(string.Empty);
                }

                // 执行初始化准备函数
                double exe_time = RunHelp.GetRunTime(() =>
                {
                    model.onInit();
                });
                Console.WriteLine("[~] Name: [{0}] 初始化准备函数用时: Time: {1}s", name, exe_time);

                // 执行自身方法
                if (CheckData.IsObjectNull(model.ExeEvent))
                {
                    Console.WriteLine("[~] Name: [{0}] ExeEvent Is NULL", name);
                }
                else
                {
                    bool isSuccess = AnalyticItem(model.ExeEvent, name);
                    if (!isSuccess)
                    {
                        return false;
                    }
                }

                // 执行含有子方法
                if (CheckData.IsSizeEmpty(model.SonCases))
                {
                    return true;
                }
                return AnalyticItem(model.SonCases, name);
            }

            /// <summary>
            /// 单个解析测试实例
            /// </summary>
            /// <param name="method">执行方法</param>
            /// <param name="name">执行名称</param>
            /// <returns>是否成功</returns>
            public bool AnalyticItem(Func<bool> method, string name)
            {
                bool isby = false;
                double exe_time = RunHelp.GetRunTime(() =>
                {
                    isby = method();
                });
                if (isby)
                {
                    Console.WriteLine("[+] Name: [{0}] 成功 Success Time: {1}s", name, exe_time);
                    return true;
                }
                else
                {
                    Console.WriteLine("[-] Name: [{0}] 失败 Error Time: {1}s", name, exe_time);
                    return false;
                }
            }
        }
    }
}
