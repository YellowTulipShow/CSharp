using System;
using System.Collections.Generic;
using CSharp.LibrayFunction;
using CSharp.ApplicationData;

namespace Test.ConsoleProgram.Case.SonTests
{
    public class TestReflex : AbsCase
    {
        public override string TestNameSign() {
            return @"测试 反射";
        }

        public override void TestMethod() {
            ModelArticles model = new ModelArticles();
            List<string> list = new List<string>();
            for (int i = 0; i < 5; i++) {
                list.Add(ReflexHelper.Name(() => model.Money));
            }
            Console.WriteLine("结束");
        }
    }
}
