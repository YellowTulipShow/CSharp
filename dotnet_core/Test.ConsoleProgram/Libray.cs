﻿using System;
using System.Collections.Generic;
using YTSCharp.Tools;

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
            list.AddRange(Bases());
            return list;
        }

        /* ================================== ~华丽的间隔线~ ================================== */

        public List<CaseModel> Bases()
        {
            return new List<CaseModel>()
            {
                new Base.HelloWorld(),
            };
        }
    }
}
