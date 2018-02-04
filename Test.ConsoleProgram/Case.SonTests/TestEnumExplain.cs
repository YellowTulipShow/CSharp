using System;
using CSharp.LibrayDataBase;
using CSharp.LibrayFunction;

namespace Test.ConsoleProgram.Case.SonTests
{
    public class TestEnumExplain : AbsCase
    {
        public override string TestNameSign() {
            return @"测试枚举类型 的自定义扩展";
        }

        public override void TestMethod() {
            Console.WriteLine("测试枚举的 '名称' 们: ");
            foreach (object item in Enum.GetNames(typeof(LEKEY))) {
                Console.WriteLine(item);
            }
            foreach (int item in Enum.GetValues(typeof(LEKEY))) {
                Console.WriteLine(item);
            }

            Console.WriteLine("new Enum() : {0}", new LEKEY().GetName());
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

        public override ICase[] SonTestCase() {
            return new ICase[] {
                new Test_GetName(),
                new Test_GetIntValue(),
                new Test_GetExplain(),
            };
        }

        private class Test_GetName : ICase
        {
            public string TestNameSign() {
                return @"获得枚举名称";
            }
            public void TestMethod() {
                Console.WriteLine("LEKEY.Key.GetName() 结果: ");
                Console.WriteLine(LEKEY.Key.GetName());
                Console.WriteLine("LEKEY.Value.GetName() 结果: ");
                Console.WriteLine(LEKEY.Value.GetName());
            }
        }
        private class Test_GetIntValue : ICase
        {
            public string TestNameSign() {
                return @"获得枚举 int 值";
            }
            public void TestMethod() {
                Console.WriteLine("LEKEY.Key.GetIntValue() 结果: ");
                Console.WriteLine(LEKEY.Key.GetIntValue());
                Console.WriteLine("LEKEY.Value.GetIntValue() 结果: ");
                Console.WriteLine(LEKEY.Value.GetIntValue());
            }
        }
        private class Test_GetExplain : ICase
        {
            public string TestNameSign() {
                return @"获得枚举 解释内容";
            }
            public void TestMethod() {
                Console.WriteLine("LEKEY.Key.GetExplain().Text 结果: ");
                Console.WriteLine(LEKEY.Key.GetExplain().Text);
                Console.WriteLine("LEKEY.Value.GetExplain().Text 结果: ");
                Console.WriteLine(LEKEY.Value.GetExplain().Text);
            }
        }
    }
}
