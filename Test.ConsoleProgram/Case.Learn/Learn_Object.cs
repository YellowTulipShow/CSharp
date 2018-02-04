using System;
using CSharp.LibrayFunction;

namespace Test.ConsoleProgram.Case.Learn
{
    public class Learn_Object : AbsCase
    {
        public override string TestNameSign() {
            return @"试验测试Object对象";
        }

        public override void TestMethod() {
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

            Model A = new Model() {
                id = 1,
                Money = 100,
                Remark = "Test A"
            };
            Model B = (Model)A.CloneModelData();

            test_Equals(A, B);
            test_ReferenceEquals(A, B);

            Console.WriteLine(string.Empty);

            B = A;
            B.id = 999;
            test_Equals(A, B);
            test_ReferenceEquals(A, B);

            object a = 9;
            if (a is int) {

            }
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

        private class Model : AbsBasicsDataModel
        {
            /// <summary>
            /// 自增ID
            /// </summary>
            [Explain(@"自增ID")]
            public int id { get { return _id; } set { _id = value; } }
            private int _id = 0;

            /// <summary>
            /// 备注
            /// </summary>
            [Explain(@"备注")]
            public string Remark { get { return _remark; } set { _remark = value; } }
            private string _remark = String.Empty;

            /// <summary>
            /// 添加时间
            /// </summary>
            [Explain(@"添加时间")]
            public DateTime TimeAdd { get { return _timeAdd; } set { _timeAdd = value; } }
            private DateTime _timeAdd = DateTime.Now;

            /// <summary>
            /// 金额
            /// </summary>
            [Explain(@"金额")]
            public decimal Money { get { return _money; } set { _money = value; } }
            private decimal _money = 0M;
        }
    }
}
