using System;
using System.Collections.Generic;
using CSharp.LibrayFunction;
using CSharp.ApplicationData;

namespace Test.ConsoleProgram.Case.SonTests
{
    public class Test_Reflex : CaseModel
    {
        public Test_Reflex() {
            base.NameSign = @"测试 反射";
            base.ExeEvent = Method;
        }

        public void Method() {
            ModelArticles model = new ModelArticles();
            List<string> list = new List<string>();
            for (int i = 0; i < 5; i++) {
                list.Add(ReflexHelper.Name(() => model.Money));
            }
            Print.WriteLine("结束");
        }
    }
}
