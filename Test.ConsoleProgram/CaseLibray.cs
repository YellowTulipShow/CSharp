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

                //new Case.Learn.Learn_Dictionary(),
                //new Case.Learn.Learn_Object(),
                //new Case.Learn.Learn_RegularExpression(),

                //new Case.SonTests.Test_AbsBasicsDataModel(),
                //new Case.SonTests.Test_Attribute(),
                //new Case.SonTests.Test_CheckData(),
                //new Case.SonTests.Test_ConvertTool(),
                //new Case.SonTests.Test_DateTime(),
                new Case.SonTests.Test_Enum(),
                //new Case.SonTests.Test_NewtonsoftJson(),
                //new Case.SonTests.Test_Random(),
                //new Case.SonTests.Test_Reflex(),
                //new Case.SonTests.Test_Lambda(),
                //new Case.SonTests.Test_WhereModel(),
            };
        }
    }
}
