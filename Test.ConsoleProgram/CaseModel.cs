using System;
using System.Text;
using System.Collections.Generic;
using YTS.Model;

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
        public Func<bool> ExeEvent = () => true;

        /// <summary>
        /// 子类实例
        /// </summary>
        public CaseModel[] SonCases = new CaseModel[] { };
    }
}
