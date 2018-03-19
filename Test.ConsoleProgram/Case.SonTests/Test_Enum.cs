using System;
using CSharp.LibrayDataBase;
using CSharp.LibrayFunction;

namespace Test.ConsoleProgram.Case.SonTests
{
    public class Test_Enum : CaseModel
    {
        public Test_Enum() {
            base.NameSign = @"测试枚举类型 的自定义扩展";
            base.ExeEvent = () => { };
            base.SonCases = new CaseModel[] {
                new CaseModel() {
                    NameSign = @"获得枚举名称",
                    ExeEvent = GetName_Method,
                },
                new CaseModel() {
                    NameSign = @"获得枚举 int 值",
                    ExeEvent = GetIntValue_Method,
                },
                new CaseModel() {
                    NameSign =  @"获得枚举 解释内容",
                    ExeEvent = GetExplain_Method,
                },
                new CaseModel() {
                    NameSign =  @"反向生成枚举值",
                    ExeEvent = GetExplain_Method,
                },
            };
        }

        private enum LEKEY
        {
            [Explain(@"键")]
            Key = 0,
            [Explain(@"值")]
            Value = 7
        }
        public void GetName_Method() {
            Print.WriteLine("LEKEY.Key.GetName() 结果: ");
            Print.WriteLine(LEKEY.Key.GetName());
            Print.WriteLine("LEKEY.Value.GetName() 结果: ");
            Print.WriteLine(LEKEY.Value.GetName());
        }
        public void GetIntValue_Method() {
            Print.WriteLine("LEKEY.Key.GetIntValue() 结果: ");
            Print.WriteLine(LEKEY.Key.GetIntValue());
            Print.WriteLine("LEKEY.Value.GetIntValue() 结果: ");
            Print.WriteLine(LEKEY.Value.GetIntValue());
        }
        public void GetExplain_Method() {
            Print.WriteLine("LEKEY.Key.GetExplain().Text 结果: ");
            Print.WriteLine(LEKEY.Key.GetExplain().Text);
            Print.WriteLine("LEKEY.Value.GetExplain().Text 结果: ");
            Print.WriteLine(LEKEY.Value.GetExplain().Text);
        }
        

    }
}
