using CSharp.LibrayFunction;
using System;

namespace CSharp.ConsoleProgram
{
    internal class Program
    {
        internal static void Main(string[] args)
        {
            TestMethod tFunRunTime = new TestMethod();
            tFunRunTime.AddEventHandlers(new TestMethod.EventHandler[] {
                new TestFunction().Init
            });

            ShowExecuteTime(tFunRunTime);
            while (IsRepeatExecute()) {
                ShowExecuteTime(tFunRunTime);
            }
        }

        private static bool IsRepeatExecute() {
            Print.WriteLine("如需重复执行, 请输入 按键 0 or + ! 否则退出!");
            string v = Print.GetReadContent();
            Print.WriteLine("====== 下一次执行内容: ======\n");
            return (v == "0" || v == "+") ? true : false;
        }

        private static void ShowExecuteTime(TestMethod tFunRunTime) {
            tFunRunTime.ExecuteEventHandler();
            double runtime = tFunRunTime.GetRunTimeTotalSeconds();
            Print.WriteLine(String.Format("\n运行时间:{0}\n", runtime));
        }
    }
}
