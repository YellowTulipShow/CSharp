using System;
using CSharp.LibrayFunction;

namespace Test.ConsoleProgram.Case.SonTests
{
    public class Test_CaseModel_Tool_Value : CaseModel
    {
        public Test_CaseModel_Tool_Value() {
            this.NameSign = @"CaseModel 的自带工具的测试";
            this.ExeEvent = () => { };
            this.SonCases = new CaseModel[] {
                new CaseModel() {
                    NameSign = @"",
                    ExeEvent = () => { },
                },
            };
        }
    }
}
