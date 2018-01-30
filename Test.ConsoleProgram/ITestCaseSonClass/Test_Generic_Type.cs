using System;
using CSharp.ApplicationData;
using CSharp.LibrayFunction;

namespace Test.ConsoleProgram.ITestCaseSonClass
{
    public class Test_Generic_Type : AbsTestCase
    {
        public override string TestNameSign() {
            return @"测试 泛型";
        }

        public override void TestMethod() {
            ModelArticles model = new ModelArticles();
            PrintTypeValue(model);
        }

        private T PrintTypeValue<T>(T value) {
            Console.WriteLine(value);
            return value;
        }
    }
}
