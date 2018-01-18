using System;
using CSharp.LibrayDataBase;
using CSharp.LibrayFunction;

namespace Test.ConsoleProgram.ITestCaseSonClass
{
    public class TestEnum : AbsTestCase
    {
        public override string TestNameSign() {
            return @"测试枚举类型";
        }

        public override void TestMethod() {
            foreach (int item in ConvertTool.EnumToInts<MSSFieldTypeCharCount>()) {
                Console.WriteLine(item);
            }
        }
    }
}
