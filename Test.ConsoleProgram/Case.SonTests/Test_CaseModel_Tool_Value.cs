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
                    NameSign = @"随机字符串",
                    ExeEvent = () => {
                        Print.WriteLine(@"(" + CommonData.Random_String() + @")");
                    },
                },
                new CaseModel() {
                    NameSign = @"部分的 ASCII 码字符",
                    ExeEvent = Section_ASCII_String,
                },
            };
        }

        public void Section_ASCII_String() {
            PrintMethod(@"所有常用字符:", CommonData.ASCII_ALL);
            PrintMethod(@"特殊字符:", CommonData.ASCII_Special);
            PrintMethod(@"阿拉伯数字:", CommonData.ASCII_Number);
            PrintMethod(@"小写英文:", CommonData.ASCII_LowerEnglish);
            PrintMethod(@"大写英文:", CommonData.ASCII_UpperEnglish);
        }
        private delegate char[] charGet();
        private void PrintMethod(string name, charGet charget) {
            char[] sour = charget();
            Print.WriteLine(name);
            Print.WriteLine(ConvertTool.IListToString(sour, string.Empty) + @" 个数: " + sour.Length.ToString());
        }
    }
}
