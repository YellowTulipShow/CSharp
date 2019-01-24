using System;
using System.Diagnostics;
using YTS.Tools;

namespace Test.ConsoleProgram.Tools
{
    public class Test_SystemLog : CaseModel
    {
        public Test_SystemLog() {
            this.NameSign = @"系统日志";
            this.SonCases = new CaseModel[] {
                Func_Use(),
                Func_Exception(),
            };
        }

        public CaseModel Func_Use() {
            return new CaseModel() {
                NameSign = @"日常使用",
                ExeEvent = () => {
                    SystemLog log = new SystemLog() {
                        Position = @"Test.ConsoleProgram.Tools.Test_UseSystemLog",
                        Type = SystemLog.LogType.Record,
                    };

                    log.Message = @"测试使用方法";

                    // 将当前的日志的内容写入文件
                    string log_file_path = log.Write();

                    Console.WriteLine("log_file_path: {0}", log_file_path);
                    return true;
                },
            };
        }

        public CaseModel Func_Exception() {
            return new CaseModel() {
                NameSign = @"异常",
                ExeEvent = () => {
                    try {
                        YTS.Model.AjaxResult ajax = null;
                        Console.WriteLine(ajax.Msg);
                    } catch (Exception ex) {
                        SystemLog log = new SystemLog() {
                            AddTime = DateTime.Now,
                            Message = @"维权及其减少时浏览量",
                            Position = ex.StackTrace,
                            Type = SystemLog.LogType.Exception,
                        };

                        SystemLog.Write(ex);
                        SystemLog.Write(ex);
                        log.Write();
                        log.Write();
                        log.Write();
                        SystemLog.Write(ex);
                        log.Write();
                        log.Write();
                    }
                    return true;
                },
            };
        }
    }
}
