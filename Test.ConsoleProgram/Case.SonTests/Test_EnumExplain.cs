using System;
using CSharp.LibrayDataBase;
using CSharp.LibrayFunction;

namespace Test.ConsoleProgram.Case.SonTests
{
    public class Test_EnumExplain : AbsCase
    {
        public override string NameSign() {
            return @"测试枚举类型 的自定义扩展";
        }

        public override void Method() {
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

        public override ICase[] SonCaseArray() {
            return new ICase[] {
                new Test_GetName(),
                new Test_GetIntValue(),
                new Test_GetExplain(),
            };
        }

        private class Test_GetName : ICase
        {
            public string NameSign() {
                return @"获得枚举名称";
            }
            public void Method() {
                Print.WriteLine("LEKEY.Key.GetName() 结果: ");
                Print.WriteLine(LEKEY.Key.GetName());
                Print.WriteLine("LEKEY.Value.GetName() 结果: ");
                Print.WriteLine(LEKEY.Value.GetName());
            }
        }
        private class Test_GetIntValue : ICase
        {
            public string NameSign() {
                return @"获得枚举 int 值";
            }
            public void Method() {
                Print.WriteLine("LEKEY.Key.GetIntValue() 结果: ");
                Print.WriteLine(LEKEY.Key.GetIntValue());
                Print.WriteLine("LEKEY.Value.GetIntValue() 结果: ");
                Print.WriteLine(LEKEY.Value.GetIntValue());
            }
        }
        private class Test_GetExplain : ICase
        {
            public string NameSign() {
                return @"获得枚举 解释内容";
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
