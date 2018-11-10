using System;
using YTS.Tools;

namespace Test.ConsoleProgram.Tools
{
    public class Test_UseSystemLog : CaseModel
    {
        public Test_UseSystemLog() {
            this.NameSign = @"使用系统日志";
            this.ExeEvent = () => {
                SystemLog log = new SystemLog() {
                    Position = @"Test.ConsoleProgram.Tools.Test_UseSystemLog",
                    Type = SystemLog.LogType.Daily,
                };

                log.Message = @"测试使用方法";

                // 将当前的日志的内容写入文件
                string log_file_path = log.Write();

                Console.WriteLine("log_file_path: {0}", log_file_path);
                return true;
            };
        }
    }
}
