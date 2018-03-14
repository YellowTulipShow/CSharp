using System;
using CSharp.LibrayDataBase;
using CSharp.LibrayFunction;

namespace Test.ConsoleProgram.Case.SonTests
{
    public class Test_EnumExplain : CaseModel
    {
        public Test_EnumExplain() {
            base.NameSign = @"测试枚举类型 的自定义扩展";
            base.ExeEvent = Method;
        }

        public void Method() {
            Print.WriteLine("测试枚举的 '名称' 们: ");
            foreach (object item in Enum.GetNames(typeof(LEKEY))) {
                Print.WriteLine(item);
            }
            foreach (int item in Enum.GetValues(typeof(LEKEY))) {
                Print.WriteLine(item);
            }

            Print.WriteLine("new Enum() : {0}", new LEKEY().GetName());
        }

        private enum basicsEnum
        {
            Basics = 0
        }

        private enum LEKEY
        {
            [Explain(@"键")]
            Key = 0,
            [Explain(@"值")]
            Value = 7
        }

        public CaseModel[] SonCaseArray() {
            return new CaseModel[] {
                new Test_GetName(),
                new Test_GetIntValue(),
                new Test_GetExplain(),
            };
        }

        public class Test_GetName : CaseModel
        {
            public Test_GetName() {
                base.NameSign = @"获得枚举名称";
                base.ExeEvent = Method;
            }

            public void Method() {
                Print.WriteLine("LEKEY.Key.GetName() 结果: ");
                Print.WriteLine(LEKEY.Key.GetName());
                Print.WriteLine("LEKEY.Value.GetName() 结果: ");
                Print.WriteLine(LEKEY.Value.GetName());
            }
        }
        public class Test_GetIntValue : CaseModel
        {
            public Test_GetIntValue() {
                base.NameSign = @"获得枚举 int 值";
                base.ExeEvent = Method;
            }

            public void Method() {
                Print.WriteLine("LEKEY.Key.GetIntValue() 结果: ");
                Print.WriteLine(LEKEY.Key.GetIntValue());
                Print.WriteLine("LEKEY.Value.GetIntValue() 结果: ");
                Print.WriteLine(LEKEY.Value.GetIntValue());
            }
        }
        public class Test_GetExplain : CaseModel
        {
            public Test_GetExplain() {
                base.NameSign = @"获得枚举 解释内容";
                base.ExeEvent = Method;
            }

            public void Method() {
                Print.WriteLine("LEKEY.Key.GetExplain().Text 结果: ");
                Print.WriteLine(LEKEY.Key.GetExplain().Text);
                Print.WriteLine("LEKEY.Value.GetExplain().Text 结果: ");
                Print.WriteLine(LEKEY.Value.GetExplain().Text);
            }
        }
    }
}
