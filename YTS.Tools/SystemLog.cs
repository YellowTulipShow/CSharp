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
        /// 日志类型 (默认为 LogEnum.Error 错误 )
        /// </summary>
        public LogType Type { get { return type; } set { type = value; } }
        private LogType type = LogType.Error;
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
        /// 是否开启堆栈跟踪列表, 默认不开启
        /// </summary>
        public bool IsStackTraceList { get { return _is_stack_trace_list; } set { _is_stack_trace_list = value; } }
        private bool _is_stack_trace_list = false;

        /// <summary>
        /// 添加时间 (默认添加当前时间)
        /// </summary>
        public DateTime AddTime { get { return addTime; } set { addTime = value; } }
        private DateTime addTime = DateTime.Now;
        #endregion

        #region === Function ===
        /// <summary>
        /// 写入一个日志
        /// </summary>
        /// <param name="lgModel">日志的数据模型</param>
        /// <returns>写入的日志文件绝对路径</returns>
        public string Write() {
            string path = GetLogFilePath();
            File.AppendAllText(path, GetFormatContent(), YTS.Tools.Const.Format.FILE_ENCODING);
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
        private string GetFormatContent() {
            if (this.IsStackTraceList) {
                string info = ConvertTool.ToString(new string[] {
                    ConvertTool.ToString(this.AddTime),
                    this.Type.ToString(),
                    this.Message,
                }, @"  >>  ");
                return ConvertTool.ToString(new string[] {
                    info,
                    @"堆栈跟踪:",
                    this.Position,
                }, "\n") + "\n";
            }
            string[] content = new string[] {
                ConvertTool.ToString(this.AddTime),
                this.Type.ToString(),
                this.Position,
                this.Message,
            };
            return ConvertTool.ToString(content, @"  >>  ") + "\n";
        }
        #endregion

        /// <summary>
        /// 写入一个日志
        /// </summary>
        /// <param name="ex"></param>
        public static void Write(Exception ex) {
            SystemLog log = new SystemLog() {
                Type = LogType.Exception,
                AddTime = DateTime.Now,
                Position = new StackTrace(true).ToString(),
                Message = ex.Message,
                IsStackTraceList = true,
            };
            log.Write();
        }
    }
}
