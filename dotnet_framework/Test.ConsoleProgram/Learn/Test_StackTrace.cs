using System;
using System.Diagnostics;
using YTS.Tools;

namespace Test.ConsoleProgram.Learn
{
    public class Test_StackTrace : CaseModel
    {
        public const string directory = @"/auto/Learn/Test_StackTrace";

        public Test_StackTrace() {
            this.NameSign = @"堆栈跟踪";
            this.SonCases = new CaseModel[] {
                Func_ToString(),
            };
        }

        public CaseModel Func_ToString() {
            return new CaseModel() {
                NameSign = @"转为字符串",
                ExeEvent = () => {
                    StackTrace st_def = new StackTrace();
                    StackTrace st_true = new StackTrace(true);
                    string[] strs = new string[] {
                        string.Format("默认: {0}", st_def.FrameCount),
                        st_def.ToString(),
                        "\n",
                        string.Format("开启文件名、行号、列号:", st_true.FrameCount),
                        st_true.ToString(),
                    };

                    string path = PathHelp.CreateUseFilePath(directory, @"Func_ToString.txt");
                    this.ClearAndWriteFile(path, ConvertTool.ToString(strs, "\n"));
                    return true;
                },
                SonCases = new CaseModel[] {
                    Func_StackFrame(),
                },
            };
        }

        public CaseModel Func_StackFrame() {
            return new CaseModel() {
                NameSign = @"一个堆栈的帧",
                ExeEvent = () => {
                    StackTrace stack = new StackTrace(true);
                    StackFrame frame = stack.GetFrame(0);
                    string[] strs = new string[] {
                            @"frame.GetFileColumnNumber():",
                            frame.GetFileColumnNumber().ToString(),
                            "\n",
                            @"frame.GetFileLineNumber():",
                            frame.GetFileLineNumber().ToString(),
                            "\n",
                            @"frame.GetFileName():",
                            frame.GetFileName().ToString(),
                            "\n",
                            @"frame.GetILOffset():",
                            frame.GetILOffset().ToString(),
                            "\n",
                            @"frame.GetMethod().Name:",
                            frame.GetMethod().Name.ToString(),
                            "\n",
                            @"frame.GetNativeOffset():",
                            frame.GetNativeOffset().ToString(),
                            "\n",
                            @"frame.ToString():",
                            frame.ToString().ToString(),
                        };
                    string path = PathHelp.CreateUseFilePath(directory, @"Func_StackFrame.txt");
                    this.ClearAndWriteFile(path, ConvertTool.ToString(strs, "\n"));
                    return true;
                },
            };
        }
    }
}
