﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using YTS.Tools;
using YTS.Tools.Const;

namespace YTS.SystemService
{
    /// <summary>
    /// 系统日志记录
    /// </summary>
    public static class SystemLog
    {
        private const String CONST_LOG_FOLER_PATH = "SystemLog";
        private const String CONST_NEW_LINE_SYMBOL = "\r\n";

        #region Model Data 模型数据信息
        /// <summary>
        /// 日志 类型枚举
        /// </summary>
        public enum LogTypeEnum
        {
            /// <summary>
            /// 日常
            /// </summary>
            Daily,
            /// <summary>
            /// 错误
            /// </summary>
            Error
        }

        /// <summary>
        /// 日志 数据模型
        /// </summary>
        public class LogModel
        {
            /// <summary>
            /// 日志类型 (默认为 LogEnum.Error 错误 )
            /// </summary>
            public LogTypeEnum Type { get { return type; } set { type = value; } }
            private LogTypeEnum type = LogTypeEnum.Error;

            /// <summary>
            /// 写入日志位置
            /// </summary>
            public String Position { get { return position; } set { position = value; } }
            private String position = String.Empty;

            /// <summary>
            /// 详情消息
            /// </summary>
            public String Message { get { return message; } set { message = value; } }
            private String message = String.Empty;

            /// <summary>
            /// 添加时间 (默认添加当前时间)
            /// </summary>
            public DateTime AddTime { get { return addTime; } set { addTime = value; } }
            private DateTime addTime = DateTime.Now;
        }

        /// <summary>
        /// 模型 转 字典
        /// </summary>
        private static Dictionary<String, String> ModelToDictionary(LogModel lgModel) {
            Dictionary<String, String> dic = new Dictionary<String, String>();
            dic.Add("Type", lgModel.Type.ToString());
            dic.Add("Position", lgModel.Position.ToString());
            dic.Add("Message", lgModel.Message.ToString());
            dic.Add("AddTime", lgModel.AddTime.ToString());
            return dic;
        }
        #endregion

        #region Set Content Info 设置内容信息
        /// <summary>
        /// 创建日志文件才写入
        /// </summary>
        private static String InitContentInfo() {
            LogModel logModel = new LogModel();
            String n_time = ReflexHelp.Name(() => logModel.AddTime);
            String n_type = ReflexHelp.Name(() => logModel.Type);
            String n_postion = ReflexHelp.Name(() => logModel.Position);
            String n_msg = ReflexHelp.Name(() => logModel.Message);

            StringBuilder cont = new StringBuilder();
            cont.Append(String.Format("#Software: YellowTulipShow System {0}", CONST_NEW_LINE_SYMBOL));
            cont.Append(String.Format("#Version: 1.0 {0}", CONST_NEW_LINE_SYMBOL));
            cont.Append(String.Format("#Date: {0} {1}", DateTime.Now.ToString(Format.DATETIME_SECOND), CONST_NEW_LINE_SYMBOL));
            cont.Append(String.Format("#Fields: {0}  {1}  {2}  {3}", n_time, n_type, n_postion, n_msg));
            cont.Append(CONST_NEW_LINE_SYMBOL);
            return cont.ToString();
        }
        /// <summary>
        /// 设置内容格式
        /// </summary>
        private static String SetContentFormat(LogModel lgModel) {
            StringBuilder reStr = new StringBuilder();
            reStr.Append(FourSpace(lgModel.AddTime.ToString(Format.DATETIME_MILLISECOND)));
            reStr.Append(FourSpace(lgModel.Type.ToString()));
            reStr.Append(FourSpace(lgModel.Position));
            reStr.Append(FourSpace(lgModel.Message));
            return String.Format("{0}{1}", reStr.ToString().Trim(), CONST_NEW_LINE_SYMBOL);

        }
        private static String FourSpace(String value) {
            int yu = value.Length % 4;
            StringBuilder space = new StringBuilder();
            for (int i = 0; i < 4 - yu; i++) {
                space.Append(" ");
            }
            return value + space.ToString();
        }
        #endregion

        #region Write Log Public Function 写入日志调用方法
        /// <summary>
        /// 写入一个日志
        /// </summary>
        /// <param name="lgModel">日志的数据模型</param>
        /// <returns>写入的文件绝对路径</returns>
        public static String Write(LogModel lgModel) {
            String path = GetLogFilePath(lgModel.Type, DateTime.Now);
            StringBuilder content = new StringBuilder();
            if (!File.Exists(path)) {
                content.Append(InitContentInfo());
            }
            content.Append(SetContentFormat(lgModel));
            File.AppendAllText(path, content.ToString());
            return path;
        }
        /// <summary>
        /// 写入一个日志 默认(日志类型 : 错误)
        /// </summary>
        /// <param name="position">出错地点</param>
        /// <param name="message">详情消息</param>
        /// <returns>写入的文件绝对路径</returns>
        public static String Write(String position, String message) {
            LogModel lgModel = new LogModel();
            lgModel.Type = SystemLog.LogTypeEnum.Error;
            lgModel.Position = position;
            lgModel.Message = message;
            lgModel.AddTime = DateTime.Now;
            return Write(lgModel);
        }
        /// <summary>
        /// 写入一个日志
        /// </summary>
        /// <param name="type">日志类型</param>
        /// <param name="position">出错地点</param>
        /// <param name="message">详情消息</param>
        /// <returns>写入的文件绝对路径</returns>
        public static String Write(SystemLog.LogTypeEnum type, String position, String message) {
            LogModel lgModel = new LogModel();
            lgModel.Type = type;
            lgModel.Position = position;
            lgModel.Message = message;
            lgModel.AddTime = DateTime.Now;
            return Write(lgModel);
        }
        #endregion

        #region Get File or Folder Path 获得文件或文件夹路径
        /// <summary>
        /// 获取日志文件路径 以指定时间计算
        /// </summary>
        private static String GetLogFilePath(LogTypeEnum typeEnum, DateTime datime) {
            String folderPath = String.Empty;
            String filePath = GeneratingPath(typeEnum, datime, out folderPath);
            InspectFolderPath(folderPath);
            String path = PathHelp.ToAbsolute(filePath);
            return path;
        }
        /// <summary>
        /// 生成文件路径
        /// </summary>
        private static String GeneratingPath(LogTypeEnum typeEnum, DateTime datime, out String folderPath) {
            StringBuilder FolderPath = new StringBuilder();
            FolderPath.Append(String.Format("/{0}/{1}Year-{2}Month", CONST_LOG_FOLER_PATH, datime.Year, datime.Month));
            FolderPath.Append(String.Format("/{0}Day-{1}Hour", datime.Day, datime.Hour));
            folderPath = FolderPath.ToString();
            FolderPath.Append(String.Format("/{0}Minute.log", datime.Minute));
            return FolderPath.ToString();
        }
        /// <summary>
        /// 检查文件夹是否存在
        /// </summary>
        private static void InspectFolderPath(String folderpath) {
            // 检测是否存在文件夹,以避免后面使用出现不可知错误
            String absolutelyFolderpath = PathHelp.ToAbsolute(folderpath);
            if (!Directory.Exists(absolutelyFolderpath)) {
                Directory.CreateDirectory(absolutelyFolderpath);
            }
        }
        #endregion
    }
}
