using System;
using System.Collections.Generic;
using YTS.Tools;

namespace Test.ConsoleProgram
{
    public class Libray
    {
        public Libray() { }

        /// <summary>
        /// 在这里面手动设置要测试的实例
        /// </summary>
        public List<CaseModel> GetALLCases()
        {
            List<CaseModel> list = new List<CaseModel>();
            list.AddRange(GetBases());
            return list;
        }

        /* ================================== ~华丽的间隔线~ ================================== */

        public List<CaseModel> GetBases()
        {
            return new List<CaseModel>()
            {
                new Base.HelloWorld(),
                new Base.BaseProgram(),
            };
        }
    }
}
