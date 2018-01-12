using System;

using CSharp.ApplicationData;

namespace Test.ConsoleProgram.ITestCaseSonClass
{
    public class TestObject : ITestCase
    {
        public string TestNameSign() {
            return @"测试Object对象";
        }

        public void TestMethod() {
            object o = null;
            Console.WriteLine("输出 执行: Console.WriteLine(o);");
            Console.WriteLine("结果: {0}", o);

            Console.WriteLine(string.Empty);

            Console.WriteLine("测试 Object == null 的 .ToString()");
            try {
                Console.WriteLine(o.ToString());
            } catch (Exception ex) {
                Console.WriteLine("出错了: {0}", ex.Message);
            }

            Console.WriteLine(string.Empty);

            object oEquals_A = 3;
            object oEquals_B = 3;
            test_Equals(oEquals_A, oEquals_B);

            Console.WriteLine(string.Empty);

            object oRefEquals_A = 3;
            object oRefEquals_B = 3;
            test_ReferenceEquals(oRefEquals_A, oRefEquals_B);

            ModelArticles A = new ModelArticles() {
                id = 1,
                Money = 100,
                Remark = "Test A",
                VipDiscountRate = 80
            };
            ModelArticles B = (ModelArticles)A.CloneModelData();

            test_Equals(A, B);
            test_ReferenceEquals(A, B);

            Console.WriteLine(string.Empty);

            B = A;
            B.id = 999;
            test_Equals(A, B);
            test_ReferenceEquals(A, B);
        }

        private void test_Equals(object A, object B) {
            Console.WriteLine("测试 Object.Equals()");
            Console.WriteLine("oEquals A:{0} B:{1}", A, B);
            Console.WriteLine("结果: {0}", object.Equals(A, B));
        }
        private void test_ReferenceEquals(object A, object B) {
            Console.WriteLine("测试 Object.ReferenceEquals()");
            Console.WriteLine("oRefEquals A:{0} B:{1}", A, B);
            Console.WriteLine("结果: {0}", object.Equals(A, B));
        }
    }
}
