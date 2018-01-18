using System;
using System.Data.SqlTypes;
using CSharp.LibrayFunction;

namespace Test.ConsoleProgram.ITestCaseSonClass
{
    public class TestDateTime : AbsTestCase
    {
        public override string TestNameSign() {
            return @"测试 DateTime 的用法";
        }

        public override void TestMethod() {
            Console.WriteLine("SqlTypes SqlDateTime.MinValue");
            Console.WriteLine(SqlDateTime.MinValue.Value.ToString());
            Console.WriteLine("SqlTypes SqlDateTime.MaxValue");
            Console.WriteLine(SqlDateTime.MaxValue.Value.ToString());
            Console.WriteLine("new SqlDateTime(DateTime.MinValue)");
            Console.WriteLine(ConvertTool.ObjToSqlDateTime(DateTime.Now.ToString(), SqlDateTime.MaxValue).ToString());
        }

        private void PrintTestTimeString(string timestr) {
            Console.WriteLine(string.Empty);
            DateTime initTime = new DateTime(2000, 1, 1, 1, 1, 1, 1);
            Console.WriteLine("初始化时间 time: {0}", initTime);
            bool resubool = DateTime.TryParse(timestr, out initTime);

            Console.WriteLine("转化Is成功: {0} 字符串: {1}", resubool, timestr);
            Console.WriteLine("最终的时间: {0} 是否是最小时间: {1}", initTime, initTime.Equals(DateTime.MinValue));
        }
    }
}
