using System;

using CSharp.ApplicationData;
using CSharp.LibrayDataBase;

namespace Test.ConsoleProgram.ITestCaseSonClass
{
    public class TestAttribute : ITestCase
    {
        public string TestNameSign() {
            return @"测试学习'特性'";
        }

        public void TestMethod() {
            ModelArticles ar = new ModelArticles();

            Type tm = typeof(ModelArticles);

            Console.WriteLine(tm.IsDefined(typeof(TableAttribute), false));
        }
    }
}
