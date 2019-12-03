using System;
using System.IO;
using System.Text;
using YTS.Tools;

namespace Test.ConsoleProgram.Learn
{
    public class Test_Path : CaseModel
    {
        public Test_Path() {
            NameSign = @"学习-路径";
            SonCases = new CaseModel[] {
                Func_Combine(),
                Func_Abs_Abs_Rel(),
                Func_Abs_Rel_Abs(),
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

        public CaseModel Func_Abs_Abs_Rel() {
            return new CaseModel() {
                NameSign = @"绝对 + 绝对 => 相对路径",
                ExeEvent = () => {
                    // 原文：https://blog.csdn.net/lishuangquan1987/article/details/53678215
                    string p1 = @"C:\A\B\C\D\E\F\G\H\";
                    string p2 = @"C:\A\B\M\N\";
                    Uri u1 = new Uri(p1, UriKind.Absolute);
                    Uri u2 = new Uri(p2, UriKind.Absolute);
                    // u2相对于u1的uri
                    Uri u3 = u1.MakeRelativeUri(u2);

                    Console.WriteLine("p1: {0}", p1);
                    Console.WriteLine("p2: {0}", p1);
                    Console.WriteLine("u3.OriginalString: {0}", u3.OriginalString);
                    return u3.OriginalString == @"../../../../../../M/N/";
                },
            };
        }

        public CaseModel Func_Abs_Rel_Abs() {
            return new CaseModel() {
                NameSign = @"绝对 + 绝对 => 相对路径",
                ExeEvent = () => {
                    // 原文：https://blog.csdn.net/lishuangquan1987/article/details/53678215
                    string p3 = @"C:\A\B\C\D\E\F\G\H\";
                    string p4 = @"..\..\..\X\Y\Z\test.txt";
                    Uri uri_p3 = new Uri(p3, UriKind.Absolute);
                    Uri uri_P4 = new Uri(p4, UriKind.Relative);
                    Uri uri_result = new Uri(uri_p3, uri_P4);

                    Console.WriteLine("p3: {0}", p3);
                    Console.WriteLine("p4: {0}", p4);
                    Console.WriteLine("p4相对于P3的绝对路径是: uri_result.LocalPath {0}", uri_result.LocalPath);
                    return uri_result.LocalPath == @"C:\A\B\C\D\E\X\Y\Z\test.txt";
                },
            };
        }
    }
}
