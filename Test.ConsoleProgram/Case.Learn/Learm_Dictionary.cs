using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Test.ConsoleProgram.Case.Learn
{
    public class Learm_Dictionary:AbsCase
    {
        public override string TestNameSign() {
            return @"学习 Dictionary<K,V> 用法";
        }

        public override void TestMethod() {
            // 初始化:
            Dictionary<string, int> dic = new Dictionary<string, int>() {
                { "key1", 343 },
                { "key2_ss", 41243 },
            };

            // 遍历:
            foreach (KeyValuePair<string, int> item in dic) {
                Console.WriteLine(@"Key: {0}", item.Key);
                Console.WriteLine(@"Value: {0}", item.Value);
            }
        }
    }
}
