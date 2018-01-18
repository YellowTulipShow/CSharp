using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CSharp.LibrayFunction;

namespace Test.ConsoleProgram.ITestCaseSonClass
{
    public class TestConvertTool : AbsTestCase
    {
        public override string TestNameSign() {
            return @"测试 转化工具";
        }

        public override void TestMethod() {
            Console.WriteLine(@"ConvertTool 的类型转化工具");
        }

        public override ITestCase[] SonTestCase() {
            return new ITestCase[] {
                new StringToInt(),
                new StringToString(),
                new StringToFloat(),
                new StringToDecimal(),
                new StringToBoolean(),
            };
        }

        private static string Srource() {
            return @",15,8,3,r,2,ssss,55,5,4,34M,35,";
        }

        #region Son Test Case
        private class StringToInt : ITestCase
        {
            public string TestNameSign() {
                return @"测试 String 转 Int";
            }
            public void TestMethod() {
                string str = Srource();
                Console.WriteLine("SourceData: \n {0}", str);
                int[] array = ConvertTool.StringToIList<int>(str, ',', s => ConvertTool.ObjToInt(s, 0));
                Console.WriteLine("Result: \n {0}", JsonHelper.SerializeObject(array));
            }
        }
        private class StringToString : ITestCase
        {
            public string TestNameSign() {
                return @"测试 String 转 String";
            }
            public void TestMethod() {
                string str = Srource();
                Console.WriteLine("SourceData: \n {0}", str);
                string[] array = ConvertTool.StringToIList<string>(str, ',', s => s);
                Console.WriteLine("Result: \n {0}", JsonHelper.SerializeObject(array));
            }
        }
        private class StringToFloat : ITestCase
        {
            public string TestNameSign() {
                return @"测试 String 转 Int";
            }
            public void TestMethod() {
                string str = Srource();
                Console.WriteLine("SourceData: \n {0}", str);
                float[] array = ConvertTool.StringToIList<float>(str, ',', s => ConvertTool.ObjToFloat(s, 0));
                Console.WriteLine("Result: \n {0}", JsonHelper.SerializeObject(array));
            }
        }
        private class StringToDecimal : ITestCase
        {
            public string TestNameSign() {
                return @"测试 String 转 Decimal";
            }
            public void TestMethod() {
                string str = Srource();
                Console.WriteLine("SourceData: \n {0}", str);
                decimal[] array = ConvertTool.StringToIList<decimal>(str, ',', s => ConvertTool.ObjToDecimal(s, 0));
                Console.WriteLine("Result: \n {0}", JsonHelper.SerializeObject(array));
            }
        }
        private class StringToBoolean : ITestCase
        {
            public string TestNameSign() {
                return @"测试 String 转 Boolean";
            }
            public void TestMethod() {
            string str = @"true,false,flsea,0,1,sliw,trus";
            Console.WriteLine("SourceData: \n {0}", str);
            bool[] array_1 = ConvertTool.StringToIList<bool>(str, ',', s => ConvertTool.ObjToBool(s, true));
            Console.WriteLine("Default: true Result: \n {0}", JsonHelper.SerializeObject(array_1));
            bool[] array_2 = ConvertTool.StringToIList<bool>(str, ',', s => ConvertTool.ObjToBool(s, false));
            Console.WriteLine("Default: false Result: \n {0}", JsonHelper.SerializeObject(array_2));
            }
        }
        #endregion
    }
}
