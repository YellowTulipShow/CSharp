using System;
using System.Collections.Generic;
using CSharp.LibrayFunction;

namespace Test.ConsoleProgram
{
    public class CaseLibray
    {
        public CaseLibray() { }

        /// <summary>
        /// 在这里面手动设置要测试的实例
        /// </summary>
        public AbsCase[] InitCaseSource() {
            return new AbsCase[] {
                //new Case.SonTests.*****(),
                new Case.SonTests.TestConvertTool(),
            };
        }
    }
}
