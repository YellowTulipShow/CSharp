using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace CSharp.LibrayFunction
{
    /// <summary>
    /// 测试 方法
    /// </summary>
    public class TestMethod
    {
        /// <summary>
        /// 运行事件的时间间隔
        /// </summary>
        private TimeSpan runtimeSpan = new TimeSpan();

        /// <summary>
        /// 执行事件列表
        /// </summary>
        private List<EventHandler> eventList = new List<EventHandler>();

        /// <summary>
        /// 定义无返回 无参数方法格式
        /// </summary>
        public delegate void EventHandler();

        /// <summary>
        /// 添加执行事件
        /// </summary>
        public void AddEventHandlers(EventHandler[] eventArr) {
            this.eventList.AddRange(eventArr);
        }

        /// <summary>
        /// 执行事件处理程序
        /// </summary>
        public void ExecuteEventHandler() {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start(); // 开始

            // 执行所有事件方法
            foreach (EventHandler eventM in this.eventList) {
                eventM();
            }

            stopwatch.Stop(); // 结束
            this.runtimeSpan = stopwatch.Elapsed;
        }

        /// <summary>
        /// 获得运行时间的整秒数和秒的小数部分值
        /// </summary>
        public double GetRunTimeTotalSeconds() {
            return this.runtimeSpan.TotalSeconds;
        }
    }
}
