using System;
using System.Collections;
using System.Collections.Generic;
using CSharp.ApplicationData;
using CSharp.LibrayDataBase;
using CSharp.LibrayFunction;

namespace Test.ConsoleProgram.Case.SonTests
{
    public class Test_CheckData : CaseModel
    {
        public Test_CheckData() {
            base.NameSign = @"测试 检查 数据类 CheckData";
            base.ExeEvent = Method;
            base.SonCases = SonCaseArray();
        }

        public void Method() {
        }

        public CaseModel[] SonCaseArray() {
            return new CaseModel[] {
                //new TestIsObjectNull(),
                //new TestIsStringNull(),
                //new TestIsSizeEmpty(),
                TestDataType(),
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

        public CaseModel TestDataType() {
            return new CaseModel() {
                NameSign = @"检查数据数据类型",
                ExeEvent = () => {
                    Print.WriteLine("CheckData.IsTypeValue<T>(V)");
                    Print.WriteLine("T: object V: Int R: {0}", CheckData.IsTypeValue<object>(879));
                    Print.WriteLine("T: Int V: Int R: {0}", CheckData.IsTypeValue<int>(879));
                    Print.WriteLine("T: Int V: String R: {0}", CheckData.IsTypeValue<int>("sdfsdf"));
                    Print.WriteLine("T: DateTime V: DateTime R: {0}", CheckData.IsTypeValue<DateTime>(DateTime.Now));

                    Print.WriteLine("\n\n");
                    Print.WriteLine("CheckData.IsTypeValue<T>(V, true)");
                    Print.WriteLine("T: object V: Int R: {0}", CheckData.IsTypeValue<object>(879, true));
                    Print.WriteLine("T: Int V: Int R: {0}", CheckData.IsTypeValue<int>(879, true));
                    Print.WriteLine("T: Int V: String R: {0}", CheckData.IsTypeValue<int>("sdfsdf", true));
                    Print.WriteLine("T: DateTime V: DateTime R: {0}", CheckData.IsTypeValue<DateTime>(DateTime.Now, true));
                    Print.WriteLine("T: List<string> V: DateTime R: {0}", CheckData.IsTypeValue<List<string>>(DateTime.Now, true));
                    Print.WriteLine("T: IList V: DateTime R: {0}", CheckData.IsTypeValue<IList>(DateTime.Now, true));
                    Print.WriteLine("T: Array V: new string[] {{ }} R: {0}", CheckData.IsTypeValue<Array>(new string[] { }, true));
                    Print.WriteLine("T: IList V: new string[] {{ }} R: {0}", CheckData.IsTypeValue<IList>(new string[] { }, true));
                    Print.WriteLine("T: string V: new string[] {{ }} R: {0}", CheckData.IsTypeValue<string>(new string[] { }, true));
                    Print.WriteLine("T: AbsBasicDataModel V: new ModelUser() R: {0}", CheckData.IsTypeValue<AbsBasicDataModel>(new ModelUser(), true));
                    Print.WriteLine("T: AbsModel_Remark V: new ModelUser() R: {0}", CheckData.IsTypeValue<AbsModel_Remark>(new ModelUser(), true));
                },
            };
        }
    }
}
