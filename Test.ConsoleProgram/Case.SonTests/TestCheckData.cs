using System;
using System.Collections.Generic;
using CSharp.LibrayFunction;

namespace Test.ConsoleProgram.Case.SonTests
{
    public class TestCheckData : AbsCase
    {
        public override string TestNameSign() {
            return @"测试 检查 数据类 CheckData";
        }

        public override void TestMethod() {
        }

        public override ICase[] SonTestCase() {
            return new ICase[] {
                new TestIsObjectNull(),
                new TestIsStringNull(),
                new TestIsSizeEmpty(),
            };
        }

        #region Son Test Case
        private class TestIsObjectNull : ICase
        {
            public string TestNameSign() {
                return @"测试 IsObjectNull";
            }
            public void TestMethod() {
                object obj = null;
                Console.WriteLine("obj 为 null : {0}", obj.IsObjectNull());
                obj = new List<string>() { };
                Console.WriteLine("obj new 后 : {0}", obj.IsObjectNull());
            }
        }
        private class TestIsStringNull : ICase
        {
            public string TestNameSign() {
                return @"测试 IsObjectNull";
            }
            public void TestMethod() {
                string str = null;
                Console.WriteLine("str 为 null : {0}", str.IsStringNull());
                str = @"testsaefawegarg";
                Console.WriteLine("str new 后 : {0}", str.IsStringNull());
            }
        }
        private class TestIsSizeEmpty : ICase
        {
            public string TestNameSign() {
                return @"测试 '集合' 的数量";
            }
            public void TestMethod() {
                List<string> listT = null;
                Console.WriteLine("listT 为 null : {0}", listT.IsSizeEmpty());
                listT = new List<string>() { };
                Console.WriteLine("listT new 后 : {0}", listT.IsSizeEmpty());
                listT = new List<string>() { "222", "34343", "fwefwe" };
                Console.WriteLine("listT new 后 填值 : {0}", listT.IsSizeEmpty());

                int[] array = null;
                Console.WriteLine("array 为 null : {0}", CheckData.IsSizeEmpty(array));
                array = new int[] { };
                Console.WriteLine("array new 后 : {0}", CheckData.IsSizeEmpty(array));
                array = new int[] { 25, 35, 84, 36, 83, 2, 2, 1, 5 };
                Console.WriteLine("array new 后 填值 : {0}", CheckData.IsSizeEmpty(array));

                Dictionary<string, int> dictionary = null;
                Console.WriteLine("dictionary 为 null : {0}", CheckData.IsSizeEmpty(dictionary));
                dictionary = new Dictionary<string, int>() { };
                Console.WriteLine("dictionary new 后 : {0}", CheckData.IsSizeEmpty(dictionary));
                dictionary = new Dictionary<string, int>() { { "key1", 23 }, { "2sliw", 43 } };
                Console.WriteLine("dictionary new 后 填值 : {0}", CheckData.IsSizeEmpty(dictionary));
            }
        }
        #endregion

    }
}
