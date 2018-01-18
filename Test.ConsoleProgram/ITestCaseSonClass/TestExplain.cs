using System;
using System.Linq.Expressions;
using CSharp.LibrayFunction;

namespace Test.ConsoleProgram.ITestCaseSonClass
{
    public class TestExplain : AbsTestCase
    {
        public override string TestNameSign() {
            return @"测试 解析特性";
        }

        public override void TestMethod() {
            //Console.WriteLine(@"测试 Model.Name");
            //Console.WriteLine(new Model().Name.GetExplain().Text);

            //Console.WriteLine(@"测试 Model.Sex");
            //Console.WriteLine(new Model().Sex.GetExplain().Text);

            //Console.WriteLine(@"测试 LEKEY.Key");
            //Console.WriteLine(LEKEY.Key.GetExplain().Text);
            //Console.WriteLine(@"测试 LEKEY.Value");
            //Console.WriteLine(LEKEY.Value.GetExplain().Text);


            string url = @"yellowtulipshow.site";
            Console.WriteLine("Url: {0}", url);
            Console.WriteLine(CheckData.IsURL(url));

            //string name = Name(() => LEKEY.Key);
            //Console.WriteLine(name);
        }

        public static String Name<T>(Expression<Func<T>> memberExpression) {
            try {
                MemberExpression expressionBody = (MemberExpression)memberExpression.Body;
                return expressionBody.Member.Name;
            } catch (Exception) {
                return string.Empty;
            }
        }

        [Explain(@"临时模型")]
        protected class Model : AbsBasicsDataModel
        {
            [Explain(@"姓名")]
            public string Name { get { return _name; } set { _name = value; } }
            private string _name = string.Empty;

            [Explain(@"性别")]
            public int Sex = 8;
        }

        protected enum LEKEY
        {
            [Explain(@"键")]
            Key = 9,
            [Explain(@"值")]
            Value = 7
        }
    }
}
