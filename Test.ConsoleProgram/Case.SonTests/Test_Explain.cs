using System;
using System.Linq.Expressions;
using CSharp.LibrayFunction;

namespace Test.ConsoleProgram.Case.SonTests
{
    public class Test_Explain : AbsCase
    {
        public override string NameSign() {
            return @"测试 解析特性";
        }

        public override void Method() {
            //Print.WriteLine(@"测试 Model.Name");
            //Print.WriteLine(new Model().Name.GetExplain().Text);

            //Print.WriteLine(@"测试 Model.Sex");
            //Print.WriteLine(new Model().Sex.GetExplain().Text);

            //Print.WriteLine(@"测试 LEKEY.Key");
            //Print.WriteLine(LEKEY.Key.GetExplain().Text);
            //Print.WriteLine(@"测试 LEKEY.Value");
            //Print.WriteLine(LEKEY.Value.GetExplain().Text);


            string url = @"yellowtulipshow.site";
            Print.WriteLine("Url: {0}", url);
            Print.WriteLine(CheckData.IsURL(url));

            //string name = Name(() => LEKEY.Key);
            //Print.WriteLine(name);
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
