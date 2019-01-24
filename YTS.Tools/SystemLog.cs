using System;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace YTS.Tools
{
    /// <summary>
    /// 系统日志记录
    /// </summary>
    public class SystemLog : AbsBasicDataModel
    {
        public SystemLog() { }

        #region === Model ===
        /// <summary>
        /// 日志 类型枚举
        /// </summary>
        public enum LogType
        {
            /// <summary>
            /// 记录
            /// </summary>
            [Explain(@"记录")]
            Record = 0,
            /// <summary>
            /// 错误
            /// </summary>
            [Explain(@"错误")]
            Error = 1,
            /// <summary>
            /// 异常
            /// </summary>
            [Explain(@"异常")]
            Exception = 2,
        }
        /// <summary>
        /// 日志类型 (默认为 LogEnum.Error 错误)
        /// </summary>
        public LogType Type { get { return type; } set { type = value; } }
        private LogType type = LogType.Error;

        /// <summary>
        /// 写入日志位置
        /// </summary>
        public string Position { get { return position; } set { position = value; } }
        private string position = string.Empty;

        /// <summary>
        /// 详情消息
        /// </summary>
        public string Message { get { return message; } set { message = value; } }
        private string message = string.Empty;

        /// <summary>
        /// 添加时间 (默认添加当前时间)
        /// </summary>
        public DateTime AddTime { get { return addTime; } set { addTime = value; } }
        private DateTime addTime = DateTime.Now;
        #endregion

        /// <summary>
        /// 写入一个日志
        /// </summary>
        /// <param name="lgModel">日志的数据模型</param>
        /// <returns>写入的日志文件绝对路径</returns>
        public string Write() {
            string path = GetLogFilePath();
            string formatcontent = GetFormatContent();
            Encoding encode = YTS.Tools.Const.Format.FILE_ENCODING;
            File.AppendAllText(path, formatcontent, encode);
            return path;
        }

        /// <summary>
        /// 获取日志文件路径 以指定时间计算
        /// </summary>
        private string GetLogFilePath() {
            string directory = string.Format("/Logs/{0}/{1}Year/{2}Month/{3}Day",
                this.Type.ToString(),
                this.AddTime.Year,
                this.AddTime.Month,
                this.AddTime.Day);
            string file = string.Format("{0}Hour.log", this.AddTime.Hour);
            return PathHelp.CreateUseFilePath(directory, file);
        }

        /// <summary>
        /// 获取格式化后内容
        /// </summary>
        protected virtual string GetFormatContent() {
            string[] strs = new string[] {
                ConvertTool.ToString(this.AddTime),
                this.Type.ToString(),
                this.Position,
                this.Message,
            };
            return ConvertTool.ToString(strs, @"  >>  ") + "\n";
        }

        /// <summary>
        /// 写入一个异常报错日志
        /// </summary>
        /// <param name="ex">表示在应用程序执行期间发生的错误。</param>
        public static void Write(Exception ex) {
            SystemLog log = new SystemLogException() {
                Type = LogType.Exception,
                AddTime = DateTime.Now,
                Position = new StackTrace(true).ToString(),
                Message = ex.Message,
                IsStackTraceList = true,
            };
            log.Write();
        }
    }

    /// <summary>
    /// 异常版系统日志
    /// </summary>
    public class SystemLogException : SystemLog
    {
        public SystemLogException() { }

        /// <summary>
        /// 是否开启堆栈跟踪列表, 默认不开启
        /// </summary>
        public bool IsStackTraceList { get { return _is_stack_trace_list; } set { _is_stack_trace_list = value; } }
        private bool _is_stack_trace_list = false;

        /// <summary>
        /// 格式化日志信息, 其中判断使用堆栈跟踪
        /// </summary>
        /// <returns>格式化后文本内容</returns>
        protected override string GetFormatContent() {
            if (!this.IsStackTraceList) {
                return base.GetFormatContent();
            }
            string[] strs = new string[] {
                ConvertTool.ToString(this.AddTime),
                this.Type.ToString(),
                this.Message,
            };
            string[] strs2 = new string[] {
                ConvertTool.ToString(strs, @"  >>  "),
                @"堆栈跟踪:",
                this.Position,
            };
            return ConvertTool.ToString(strs2, "\n") + "\n";
        }
    }
}
