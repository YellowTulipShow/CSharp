using System;
using System.IO;
using System.Collections.Generic;
using YTS.Model;
using YTS.Tools;
using YTS.Tools.Model;

namespace Test.ConsoleProgram.Tools
{
    public class Test_PathHelp : CaseModel
    {
        public Test_PathHelp() {
            NameSign = @"路径地址";
            SonCases = new CaseModel[] {
                Func_IsAbsolute(),
                Func_ToAbsolute(),
                Func_UseFilePath(),
                Func_DirectoryFileInfo(),
            };
        }

        public CaseModel Func_IsAbsolute() {
            return new CaseModel() {
                NameSign = @"是否绝对路径",
                ExeEvent = () => {
                    KeyBoolean[] paths = new KeyBoolean[] {
                        new KeyBoolean() { Key = null, Value = false },
                        new KeyBoolean() { Key = string.Empty, Value = false },
                        new KeyBoolean() { Key = "/", Value = false },
                        new KeyBoolean() { Key = "folder", Value = false },
                        new KeyBoolean() { Key = "folder/son", Value = false },
                        new KeyBoolean() { Key = "/folder/son", Value = false },
                        new KeyBoolean() { Key = "/folder/son/name.txt", Value = false },
                        new KeyBoolean() { Key = "C:/", Value = false },
                        new KeyBoolean() { Key = "C:/Users/Administrator", Value = false },
                        new KeyBoolean() { Key = "C:", Value = false },
                        new KeyBoolean() { Key = "C:\\", Value = true },
                        new KeyBoolean() { Key = "C:\\Users\\Administrator", Value = true },
                        new KeyBoolean() { Key = "C:\\Users\\Administrator\\name.txt", Value = true },
                    };
                    foreach (KeyBoolean item in paths) {
                        if (PathHelp.IsAbsolute(item.Key) != item.Value) {
                            Console.WriteLine("item.Key: {0} item.Value: {1}", item.Key, item.Value);
                            return false;
                        }
                    }
                    return true;
                },
            };
        }

        public CaseModel Func_ToAbsolute() {
            return new CaseModel() {
                NameSign = @"转为绝对路径",
                ExeEvent = () => {
                    string basic_dir = AppDomain.CurrentDomain.BaseDirectory;
                    KeyString[] paths = new KeyString[] {
                        new KeyString() {
                            Key = null,
                            Value = string.Empty,
                        },
                        new KeyString() {
                            Key = string.Empty,
                            Value = string.Empty,
                        },
                        new KeyString() {
                            Key = "/",
                            Value = basic_dir,
                        },
                        new KeyString() {
                            Key = "/test/wlif/jiw/name.txt",
                            Value = basic_dir + "test\\wlif\\jiw\\name.txt",
                        },
                        new KeyString() {
                            Key = "//",
                            Value = basic_dir,
                        },
                        new KeyString() {
                            Key = "/test//wlif/jiw/name.txt",
                            Value = basic_dir + "test\\wlif\\jiw\\name.txt",
                        },
                        new KeyString() {
                            Key = "/test//wlif//jiw////name.txt",
                            Value = basic_dir + "test\\wlif\\jiw\\name.txt",
                        },
                        new KeyString() {
                            Key = basic_dir+"test\\wlif\\jiw\\name.txt",
                            Value = basic_dir + "test\\wlif\\jiw\\name.txt",
                        },
                        new KeyString() {
                            Key = "sdfaw/efae/fee/",
                            Value = basic_dir + "sdfaw\\efae\\fee\\",
                        },
                        new KeyString() {
                            Key = "ttt",
                            Value = basic_dir + "ttt",
                        },
                        new KeyString() {
                            Key = "qqq/",
                            Value = basic_dir + "qqq\\",
                        },
                        new KeyString() {
                            Key = "C:\\",
                            Value = "C:\\",
                        },
                        new KeyString() {
                            Key = "C://",
                            Value = "C:\\",
                        },
                    };
                    foreach (KeyString item in paths) {
                        string abs_path = PathHelp.ToAbsolute(item.Key);
                        if (!abs_path.Equals(item.Value)) {
                            Console.WriteLine("  item.Key: {0}", item.Key);
                            Console.WriteLine("  abs_path: {0}", abs_path);
                            Console.WriteLine("item.Value: {0}", item.Value);
                            return false;
                        }
                    }
                    return true;
                },
            };
        }

        public CaseModel Func_UseFilePath() {
            return new CaseModel() {
                NameSign = @"准备使用文件路径",
                ExeEvent = () => {
                    KeyString[] paths = new KeyString[] {
                        new KeyString() {
                            Key = null,
                            Value = string.Empty, // 为空, 默认跳过
                        },
                        new KeyString() {
                            Key = string.Empty, // 目录为空, 默认为 "/"
                            Value = "name.yts",
                        },
                        new KeyString() {
                            Key = "qqqq/sss/",
                            Value = string.Empty, // 为空, 默认跳过
                        },
                        new KeyString() {
                            Key = "ttt/rrr/",
                            Value = "name.yts",
                        },
                        new KeyString() {
                            Key = "uuu/iii/",
                            Value = "/eee/name.yts", // 过滤非法字符为: eeename.yts
                        },
                        new KeyString() {
                            Key = "/",
                            Value = "fff.yts",
                        },
                        new KeyString() {
                            Key = "/aaa",
                            Value = "aaa.yts",
                        },
                        new KeyString() {
                            Key = "/rrr/xxx",
                            Value = "aaa.yts",
                        },
                        new KeyString() {
                            Key = "/rrr/xxx/",
                            Value = "hhh.yts",
                        },
                        new KeyString() {
                            Key = "/vvv/yyy",
                            Value = "\\rr/www/aaa.yts", // 过滤非法字符为: rrwwwaaa.yts
                        },
                    };
                    foreach (KeyString item in paths) {
                        string absfilepath = PathHelp.CreateUseFilePath("/test_PathHelp/" + item.Key, item.Value);
                        if (CheckData.IsStringNull(absfilepath)) {
                            if (CheckData.IsStringNull(item.Key) || CheckData.IsStringNull(item.Value)) {
                                continue;
                            } else {
                                Console.WriteLine("结果为空, 两个参数都不为空, 不合理", item.Key);
                                Console.WriteLine("directory: {0}", item.Key);
                                Console.WriteLine(" filename: {0}", item.Value);
                                Console.WriteLine(" abs_file: {0}", absfilepath);
                                return false;
                            }
                        }
                        PathHelp.CreateFileExists(absfilepath);
                        if (!File.Exists(absfilepath)) {
                            Console.WriteLine("文件并没有被创建", item.Key);
                            Console.WriteLine("directory: {0}", item.Key);
                            Console.WriteLine(" filename: {0}", item.Value);
                            Console.WriteLine(" abs_file: {0}", absfilepath);
                            return false;
                        }
                    }
                    return true;
                },
            };
        }

        public CaseModel Func_DirectoryFileInfo() {
            return new CaseModel() {
                NameSign = @"文件夹文件遍历",
                ExeEvent = () => {
                    return true;
                    string abspath = @"D:\auto\circleoffriends";

                    DirectoryInfo dir = new DirectoryInfo(abspath);

                    // 最新的三条
                    DirectoryInfo[] sondir = PathHelp.UpToDateDirectorys(dir, 2, 3);

                    foreach (DirectoryInfo info in sondir) {
                        Console.WriteLine("Directory name: {0}, create time: {1}", info.Name, info.CreationTime);

                        FileInfo[] fis = PathHelp.PatternFileInfo(info, @".*\.(jpg|png|gif)");
                        foreach (FileInfo fi in fis) {
                            Console.WriteLine("  File name: {0}, create time: {1}", fi.Name, fi.CreationTime);
                        }

                        Console.WriteLine(string.Empty);
                    }

                    return true;
                },
            };
        }
    }
}
