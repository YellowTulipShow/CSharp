using System;
using System.Collections.Generic;
using System.Text;
using CSharp.LibrayFunction;

namespace Test.ConsoleProgram.Case.SonTests
{
    public class TestListSortMethod : AbsCase
    {
        public override string TestNameSign() {
            return @"测试学习: List<T>.Sort() 方法的学习运用";
        }
        public override void TestMethod() {
            List<string> list = GetTestDataSource();

            Console.WriteLine(@"测试数据源:");
            Console.WriteLine(JsonHelper.SerializeObject(list));

            count = 0;
            Console.WriteLine(@"排序过程:");
            list.Sort(ShowSrotDetails);

            Console.WriteLine(@"排序后的结果:");
            Console.WriteLine(JsonHelper.SerializeObject(list));

            Console.WriteLine(@"排序的次数: {0}", count);
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
            Console.WriteLine(@"x: {0}  y: {1}", x, y);
            return Sort.String(x, y);
        }
    }
}
