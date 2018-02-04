using System;
using CSharp.ApplicationData;
using CSharp.LibrayFunction;

namespace Test.ConsoleProgram.Case.SonTests
{
    public class Test_Generic_Type : AbsCase
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
