using System;
using System.Collections.Generic;
using CSharp.LibrayFunction;

namespace Test.ConsoleProgram.Case.SonTests
{
    public class Test_CheckData : CaseModel
    {
        public Test_CheckData() {
            base.NameSign = @"测试 检查 数据类 CheckData";
            base.ExeEvent = Method;
        }

        public void Method() {
        }

        public CaseModel[] SonCaseArray() {
            return new CaseModel[] {
                new TestIsObjectNull(),
                new TestIsStringNull(),
                new TestIsSizeEmpty(),
            };
        }

        #region Son Test Case
        public class TestIsObjectNull : CaseModel
        {
            public TestIsObjectNull() {
                base.NameSign = @"测试 IsObjectNull";
                base.ExeEvent = Method;
            }

            public void Method() {
                object obj = null;
                Print.WriteLine("obj 为 null : {0}", obj.IsObjectNull());
                obj = new List<string>() { };
                Print.WriteLine("obj new 后 : {0}", obj.IsObjectNull());
            }
        }
        public class TestIsStringNull : CaseModel
        {
            public TestIsStringNull() {
                base.NameSign = @"测试 IsObjectNull";
                base.ExeEvent = Method;
            }

            public void Method() {
                string str = null;
                Print.WriteLine("str 为 null : {0}", str.IsStringNull());
                str = @"testsaefawegarg";
                Print.WriteLine("str new 后 : {0}", str.IsStringNull());
            }
        }
        public class TestIsSizeEmpty : CaseModel
        {
            public TestIsSizeEmpty() {
                base.NameSign = @"测试 '集合' 的数量";
                base.ExeEvent = Method;
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
