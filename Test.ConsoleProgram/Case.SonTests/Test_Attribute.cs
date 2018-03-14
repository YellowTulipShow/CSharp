using System;

using CSharp.ApplicationData;
using CSharp.LibrayDataBase;

namespace Test.ConsoleProgram.Case.SonTests
{
    public class Test_Attribute : CaseModel
    {
        public Test_Attribute() {
            base.NameSign = @"测试学习'特性'";
            base.ExeEvent = Method;
        }

        public void Method() {
            ModelArticles ar = new ModelArticles();

            Type tm = typeof(ModelArticles);

            Print.WriteLine(tm.IsDefined(typeof(TableAttribute), false));
        }
    }
}
