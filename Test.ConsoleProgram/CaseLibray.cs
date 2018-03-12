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
        public CaseModel[] InitCaseSource() {
            return new CaseModel[] {
                // new Case.SonTests.*****(),
                new Test_AbsCase(),
                new Test_AbsCase(),
            };
        }
    }
}
