using System;
using CSharp.LibrayDataBase;
using CSharp.LibrayFunction;

namespace Test.ConsoleProgram.ITestCaseSonClass
{
    public class TestEnum : ITestCase
    {
        public string TestNameSign() {
            return @"测试枚举类型";
        }

        public void TestMethod() {
            foreach (int item in ConvertTool.EnumToInts<MSSFieldTypeCharCount>()) {
                Console.WriteLine(item);
            }
        }
    }
}
