using System;
using System.Text;
using System.Collections.Generic;
using YTS.Model;
using YTS.Tools;

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
