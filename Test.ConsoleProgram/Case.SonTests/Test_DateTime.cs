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
            base.SonCases = new CaseModel[] {
                //Method_GetMaxDayCount_TwoDayNum(),
                //Method_GetMaxDayCount_AllMonthDayNum(),
                TestTimeSpan(),
            };
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

        protected CaseModel Method_GetMaxDayCount_TwoDayNum() {
            return new CaseModel() {
                NameSign = @"闰年平年二月份的天数",
                ExeEvent = () => {
                    for (int i = 1; i < 9999; i++) {
                        int year = i;
                        int month = 2;
                        int day = CommonData.GetMaxDayCount(year, month);
                        string strfor = @"year: {0}  month: {1} day: {2}";
                        try {
                            DateTime time = new DateTime(year, month, day);
                            Print.WriteLine(strfor, year, month, day);
                        } catch (Exception) {
                            Print.WriteLine(strfor, year, month, @"错误值");
                        }
                    }
                },
            };
        }
        protected CaseModel Method_GetMaxDayCount_AllMonthDayNum() {
            return new CaseModel() {
                NameSign = @"各个月份的天数",
                ExeEvent = () => {
                    for (int i = 1; i <= 12; i++) {
                        int year = 2018;
                        int month = i;
                        int day = CommonData.GetMaxDayCount(year, month);
                        string strfor = @"year: {0} month: {1} day: {2}";
                        try {
                            DateTime time = new DateTime(year, month, day);
                            Print.WriteLine(strfor, year, month, day);
                        } catch (Exception) {
                            Print.WriteLine(strfor, year, month, @"错误值");
                        }
                    }
                },
            };
        }

        public CaseModel TestTimeSpan() {
            return new CaseModel() {
                NameSign = @"两个时间的差值",
                ExeEvent = () => {
                    DateTime EDT = DateTime.MinValue;
                    DateTime dt1 = ConvertTool.ObjToDateTime(@"2018-08-27 11:57:40.300", EDT);
                    DateTime dt2 = ConvertTool.ObjToDateTime(@"2018-08-27 11:57:40.500", EDT);
                    TimeSpan tS = dt2 - dt1;
                    Print.WriteLine("Days: {0}", tS.Days);
                    Print.WriteLine("Hours: {0}", tS.Hours);
                    Print.WriteLine("Milliseconds: {0}", tS.Milliseconds);
                    Print.WriteLine("Minutes: {0}", tS.Minutes);
                    Print.WriteLine("Seconds: {0}", tS.Seconds);
                    Print.WriteLine("Ticks: {0}", tS.Ticks);
                    Print.WriteLine("TotalDays: {0}", tS.TotalDays);
                    Print.WriteLine("TotalHours: {0}", tS.TotalHours);
                    Print.WriteLine("TotalMilliseconds: {0}", tS.TotalMilliseconds);
                    Print.WriteLine("TotalMinutes: {0}", tS.TotalMinutes);
                    Print.WriteLine("TotalSeconds: {0}", tS.TotalSeconds);

                    /*
正则解析

\s+//.*
^\s+public \w+ (\w+) { get; }
Print.WriteLine("$1: {0}", uri.$1);
                     */
                },
            };
        }
    }
}
