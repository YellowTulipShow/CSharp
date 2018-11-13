using System;
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
            /// 日常
            /// </summary>
            [Explain(@"日常")]
            Daily = 0,
            /// <summary>
            /// 错误
            /// </summary>
            [Explain(@"错误")]
            Error = 1,
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
            string root = @"SystemLog";
            string date = string.Format("{0}Year/{1}Month/{2}Day", this.AddTime.Year, this.AddTime.Month, this.AddTime.Day);
            string folder = string.Format("/{0}/{1}/{2}", root, this.Type.ToString(), date);
            string file = string.Format("{0}Hour.log", this.AddTime.Hour);
            return PathHelp.CreateUseFilePath(folder, file);
        }

        /// <summary>
        /// 获取格式化后内容
        /// </summary>
        private string GetFormatContent() {
            string[] content = new string[] {
                ConvertTool.ToString(this.AddTime),
                this.Type.ToString(),
                this.Position,
                this.Message,
            };
            return ConvertTool.ToString(content, @"  >>  ") + "\r\n";
        }
        #endregion
    }
}
