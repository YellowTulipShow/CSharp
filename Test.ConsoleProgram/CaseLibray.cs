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
        private AbsCase[] InitTestCaseSource() {
            return new AbsCase[] {
                //new Case.SonTests.*****(),
                new Case.SonTests.TestConvertTool(),
            };
        }

        /// <summary>
        /// 自动获取测试实例-(不用更改)
        /// </summary>
        public ICase[] GetTestCase(bool isGetITestCase) {
            AbsCase[] initCases = InitTestCaseSource();
            if (!isGetITestCase)
                return initCases;

            List<ICase> list = new List<ICase>();
            foreach (AbsCase item in initCases) {
                if (CheckData.IsObjectNull(item))
                    continue;
                list.Add(item);
                ICase[] sonCase = item.SonTestCase();
                if (!CheckData.IsSizeEmpty(sonCase))
                    list.AddRange(sonCase);
            }
            return list.ToArray();
        }
    }
}
