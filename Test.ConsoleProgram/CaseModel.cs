using System;
using System.Text;
using System.Collections.Generic;
using YTS.Model;
using YTS.Tools;
using System.IO;

namespace Test.ConsoleProgram
{
    /// <summary>
    /// 测试实例模型
    /// </summary>
    public class CaseModel : AbsBasicDataModel
    {
        public CaseModel() { }

        /// <summary>
        /// 名称标志
        /// </summary>
        public string NameSign = @"实例模型初始名称";

        /// <summary>
        /// 执行事件
        /// </summary>
        public Func<bool> ExeEvent = null;

        /// <summary>
        /// 子类实例
        /// </summary>
        public CaseModel[] SonCases = new CaseModel[] { };

        public void WriterFileString(string abs_file_path, string content) {
            abs_file_path = ConvertTool.ToString(abs_file_path);
            if (!PathHelp.IsAbsolute(abs_file_path)) {
                Console.WriteLine("需要写入的文件名称错误: {0}", abs_file_path);
                return;
            }
            content = ConvertTool.ToString(content);
            File.Delete(abs_file_path);
            File.AppendAllText(abs_file_path, content);
            Console.WriteLine("清空并写入文件: {0}", abs_file_path);
        }
    }
    /*
     * 正则:
     * \s*public\s?(virtual|abstract)? (.*) (\w+)\(.*\) \{\s*.*\s*\}
     * 替换:
     *  public CaseModel Func_$3() {
            return new CaseModel() {
                NameSign = @"",
                ExeEvent = () => {
                    return true;
                },
            };
        }
     */
}
