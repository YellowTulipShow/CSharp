using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Test.ConsoleProgram.Case.Learn
{
    public class Learn_Dictionary : CaseModel
    {
        public Learn_Dictionary() {
            base.NameSign = @"学习 Dictionary<K,V> 用法";
            base.ExeEvent = Method;
        }

        public void Method() {
            // 初始化:
            Dictionary<string, int> dic = new Dictionary<string, int>() {
                { "key1", 343 },
                { "key2_ss", 41243 },
            };

            // 遍历:
            foreach (KeyValuePair<string, int> item in dic) {
                Print.WriteLine(@"Key: {0}", item.Key);
                Print.WriteLine(@"Value: {0}", item.Value);
            }
        }
    }
}
