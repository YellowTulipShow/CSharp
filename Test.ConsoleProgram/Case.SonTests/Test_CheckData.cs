using System;
using System.Collections.Generic;
using CSharp.LibrayFunction;

namespace Test.ConsoleProgram.Case.SonTests
{
    public class Test_CheckData : AbsCase
    {
        public override string NameSign() {
            return @"测试 检查 数据类 CheckData";
        }

        public override void Method() {
        }

        public override ICase[] SonCaseArray() {
            return new ICase[] {
                new TestIsObjectNull(),
                new TestIsStringNull(),
                new TestIsSizeEmpty(),
            };
        }

        #region Son Test Case
        private class TestIsObjectNull : ICase
        {
            public string NameSign() {
                return @"测试 IsObjectNull";
            }
            public void Method() {
                object obj = null;
                Print.WriteLine("obj 为 null : {0}", obj.IsObjectNull());
                obj = new List<string>() { };
                Print.WriteLine("obj new 后 : {0}", obj.IsObjectNull());
            }
        }
        private class TestIsStringNull : ICase
        {
            public string NameSign() {
                return @"测试 IsObjectNull";
            }
            public void Method() {
                string str = null;
                Print.WriteLine("str 为 null : {0}", str.IsStringNull());
                str = @"testsaefawegarg";
                Print.WriteLine("str new 后 : {0}", str.IsStringNull());
            }
        }
        private class TestIsSizeEmpty : ICase
        {
            public string NameSign() {
                return @"测试 '集合' 的数量";
            }
            public void Method() {
                List<string> listT = null;
                Print.WriteLine("listT 为 null : {0}", listT.IsSizeEmpty());
                listT = new List<string>() { };
                Print.WriteLine("listT new 后 : {0}", listT.IsSizeEmpty());
                listT = new List<string>() { "222", "34343", "fwefwe" };
                Print.WriteLine("listT new 后 填值 : {0}", listT.IsSizeEmpty());

                int[] array = null;
                Print.WriteLine("array 为 null : {0}", CheckData.IsSizeEmpty(array));
                array = new int[] { };
                Print.WriteLine("array new 后 : {0}", CheckData.IsSizeEmpty(array));
                array = new int[] { 25, 35, 84, 36, 83, 2, 2, 1, 5 };
                Print.WriteLine("array new 后 填值 : {0}", CheckData.IsSizeEmpty(array));

                Dictionary<string, int> dictionary = null;
                Print.WriteLine("dictionary 为 null : {0}", CheckData.IsSizeEmpty(dictionary));
                dictionary = new Dictionary<string, int>() { };
                Print.WriteLine("dictionary new 后 : {0}", CheckData.IsSizeEmpty(dictionary));
                dictionary = new Dictionary<string, int>() { { "key1", 23 }, { "2sliw", 43 } };
                Print.WriteLine("dictionary new 后 填值 : {0}", CheckData.IsSizeEmpty(dictionary));
            }
        }
        #endregion

    }
}
