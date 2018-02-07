using System;

using CSharp.ApplicationData;
using CSharp.LibrayDataBase;

namespace Test.ConsoleProgram.Case.SonTests
{
    public class Test_Attribute : AbsCase
    {
        public override string NameSign() {
            return @"测试学习'特性'";
        }

        public override void Method() {
            ModelArticles ar = new ModelArticles();

            Type tm = typeof(ModelArticles);

            Print.WriteLine(tm.IsDefined(typeof(TableAttribute), false));
        }
    }
}
