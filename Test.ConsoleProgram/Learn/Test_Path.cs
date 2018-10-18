using System;
using System.IO;
using YTS.Tools;

namespace Test.ConsoleProgram.Learn
{
    public class Test_Path : CaseModel
    {
        public Test_Path() {
            NameSign = @"学习-路径";
            SonCases = new CaseModel[] {
                //Func_Combine(),
            };
        }

        public CaseModel Func_Combine() {
            return new CaseModel() {
                NameSign = @"组合字符串",
                ExeEvent = () => {
                    char[] file_name_err_chars = Path.GetInvalidFileNameChars();
                    char[] path_err_chars = Path.GetInvalidPathChars();
                    Console.WriteLine("file_err_chars: {0}", JSON.Serializer(file_name_err_chars));
                    Console.WriteLine("path_err_chars: {0}", JSON.Serializer(path_err_chars));
                    string path = Path.Combine(new string[] {
                        //AppDomain.CurrentDomain.BaseDirectory,
                        "C:\\SSS\\",
                        "/asdjif",
                        "ffff",
                        "/qqqqq"
                    });
                    Console.WriteLine("path: {0}", path);
                    Console.WriteLine("Directory.Exists(path): {0}", Directory.Exists(path));
                    Console.WriteLine("After Create Path:");
                    if (!Directory.Exists(path)) {
                        DirectoryInfo info = Directory.CreateDirectory(path);
                        Console.WriteLine("Directory.Exists(path): {0}", Directory.Exists(path));
                    }
                    return true;
                },
            };
        }
    }
}
