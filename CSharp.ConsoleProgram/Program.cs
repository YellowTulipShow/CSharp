using System;

namespace CSharp.ConsoleProgram
{
    class Program
    {
        static void Main(string[] args)
        {
            RunTime tFunRunTime = new RunTime();
            tFunRunTime.eventHandler += TestExecute;
            Print.WriteLine(tFunRunTime.PrintExecute());
            while (Print.IsRepeatExecute()) {
                Print.WriteLine(tFunRunTime.PrintExecute());
            }
        }
        private static void TestExecute() {
            new TestFunction().Init();
        }
    }
}
