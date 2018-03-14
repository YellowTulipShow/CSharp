using System;
using System.Collections;
using CSharp.LibrayFunction;

namespace Test.ConsoleProgram.Case.SonTests
{
    public class Test_Random : CaseModel
    {
        public Test_Random() {
            base.NameSign = @"测试 随机";
            base.ExeEvent = Method;
        }

        public void Method() {
        }

        public CaseModel[] SonCaseArray() {
            return new CaseModel[] {
                new Test_RandomHelper_list(),
            };
        }

        public class Test_RandomHelper_list : CaseModel
        {
            public Test_RandomHelper_list() {
                base.NameSign = @"随机从列表中选出一个选项";
                base.ExeEvent = Method;
            }

            public void Method() {
                int[] int_list = new int[] { 51, 5, 48, 63, 2, 18, 4, 3, 2, 87, 15, 41, 1, 4, 8, 6, 3};
                for (int i = 0; i < int_list.Length; i++) {
                    int int_item = RandomHelper.RandomOneItem(int_list);
                    ShowResult(int_list.ToJson(), int_item);
                }

                string[] str_list = new string[] { "hello", "world", "zrq", "love", "wechatno", "show", "yellowtulip" };
                for (int i = 0; i < str_list.Length; i++) {
                    string str_item = RandomHelper.RandomOneItem(str_list);
                    ShowResult(str_list.ToJson(), str_item);
                }
            }

            private void ShowResult(string list, object obj) {
                //Print.WriteLine("集合: {0}", list);
                Print.WriteLine("随机的选项: {0}", obj);
            }
        }
    }
}
