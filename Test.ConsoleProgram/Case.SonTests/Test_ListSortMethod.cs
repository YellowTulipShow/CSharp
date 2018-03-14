using System;
using System.Collections.Generic;
using System.Text;
using CSharp.LibrayFunction;

namespace Test.ConsoleProgram.Case.SonTests
{
    public class Test_ListSortMethod : CaseModel
    {
        public Test_ListSortMethod() {
            base.NameSign = @"测试学习: List<T>.Sort() 方法的学习运用";
            base.ExeEvent = Method;
        }

        public void Method() {
            List<string> list = GetTestDataSource();

            Print.WriteLine(@"测试数据源:");
            Print.WriteLine(JsonHelper.SerializeObject(list));

            count = 0;
            Print.WriteLine(@"排序过程:");
            list.Sort(ShowSrotDetails);

            Print.WriteLine(@"排序后的结果:");
            Print.WriteLine(JsonHelper.SerializeObject(list));

            Print.WriteLine(@"排序的次数: {0}", count);
        }

        private List<string> GetTestDataSource() {
            return new List<string>() {
                "sd怕",
                "Remark",
                "Email",
                "RealName",
                "TimeAdd",
                "NickName",
                "Password",
                "MobilePhone",
                "id",
            };
        }

        private static int count = 0;

        private static int ShowSrotDetails(string x, string y) {
            count++;
            Print.WriteLine(@"x: {0}  y: {1}", x, y);
            return Sort.String(x, y);
        }
    }
}
