using System;
using System.Data.SqlTypes;
using CSharp.LibrayFunction;

namespace Test.ConsoleProgram.Case.SonTests
{
    public class Test_DateTime : CaseModel
    {
        public Test_DateTime() {
            base.NameSign = @"测试 DateTime 的用法";
            base.ExeEvent = Method;
        }

        public void Method() {
            Print.WriteLine("SqlTypes SqlDateTime.MinValue");
            Print.WriteLine(SqlDateTime.MinValue.Value.ToString());
            Print.WriteLine("SqlTypes SqlDateTime.MaxValue");
            Print.WriteLine(SqlDateTime.MaxValue.Value.ToString());
            Print.WriteLine("new SqlDateTime(DateTime.MinValue)");
            Print.WriteLine(ConvertTool.ObjToSqlDateTime(DateTime.Now.ToString(), SqlDateTime.MaxValue).ToString());
        }

        private void PrintTestTimeString(string timestr) {
            Print.WriteLine(string.Empty);
            DateTime initTime = new DateTime(2000, 1, 1, 1, 1, 1, 1);
            Print.WriteLine("初始化时间 time: {0}", initTime);
            bool resubool = DateTime.TryParse(timestr, out initTime);

            Print.WriteLine("转化Is成功: {0} 字符串: {1}", resubool, timestr);
            Print.WriteLine("最终的时间: {0} 是否是最小时间: {1}", initTime, initTime.Equals(DateTime.MinValue));
        }
    }
}
