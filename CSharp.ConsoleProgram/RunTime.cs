using CSharp.LibrayDataBase;
using CSharp.LibrayFunction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CSharp.ConsoleProgram
{
    /// <summary>
    /// 测试运行时间
    /// </summary>
    public class RunTime
    {
        public delegate void EventHandler();
        public event EventHandler eventHandler;
        public string PrintExecute()
        {
            System.Diagnostics.Stopwatch stopwatch = new System.Diagnostics.Stopwatch();
            stopwatch.Start();

            if (!CheckData.IsObjectNull(eventHandler)) {
                eventHandler();
            }

            stopwatch.Stop();
            return String.Format("\n运行时间:{0}\n", stopwatch.Elapsed.TotalSeconds);
        }
    }
}
