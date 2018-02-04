using System;
using CSharp.LibrayFunction;

namespace Test.ConsoleProgram.Case.SonTests
{
    public class TestConvertTool : AbsCase
    {
        public override string TestNameSign() {
            return @"测试 转化工具";
        }

        public override void TestMethod() {
            Console.WriteLine(@"ConvertTool 的类型转化工具");
        }

        public override ICase[] SonTestCase() {
            return new ICase[] {
                new StringToInt(),
                new StringToInt_ErrorValue(),
                new StringToString(),
                new StringToString_ErrorValue(),
                new StringToFloat(),
                new StringToDecimal(),
                new StringToBoolean(),
            };
        }

        private static string Srource() {
            return ",15,8,3,-1,,,r,2,0,ssss,55,5,4,34M,350,0,";
        }

        #region Son Test Case
        private class StringToInt : ICase
        {
            public string TestNameSign() {
                return @"测试 String 转 Int";
            }
            public void TestMethod() {
                string str = Srource();
                Console.WriteLine("SourceData: \n {0}", str);
                int[] array = str.ToArrayList(',').ListConvertType(s => ConvertTool.ObjToInt(s, 0));
                Console.WriteLine("Result: \n {0}", JsonHelper.SerializeObject(array));
            }
        }
        private class StringToInt_ErrorValue : ICase
        {
            public string TestNameSign() {
                return @"测试 String 转 Int 加入检测 故意的排除值错误值: -1";
            }
            public void TestMethod() {
                string str = Srource();
                Console.WriteLine("SourceData: \n {0}", str);
                int[] array = str.ToArrayList(',').ListConvertType(s => ConvertTool.ObjToInt(s, 0), errorValue: -1);
                Console.WriteLine("Result: \n {0}", JsonHelper.SerializeObject(array));
            }
        }

        private class StringToString : ICase
        {
            public string TestNameSign() {
                return @"测试 String 转 String";
            }
            public void TestMethod() {
                string str = Srource();
                Console.WriteLine("SourceData: \n {0}", str);
                string[] array = str.ToArrayList(',').ListConvertType(s => s);
                Console.WriteLine("Result: \n {0}", JsonHelper.SerializeObject(array));
            }
        }
        private class StringToString_ErrorValue : ICase
        {
            public string TestNameSign() {
                return @"测试 String 转 String 加入检测 故意的排除值错误值: '0'";
            }
            public void TestMethod() {
                string str = Srource();
                Console.WriteLine("SourceData: \n {0}", str);
                string[] array = str.ToArrayList(',').ListConvertType(s => s, "0");
                Console.WriteLine("Result: \n {0}", JsonHelper.SerializeObject(array));
            }
        }

        private class StringToFloat : ICase
        {
            public string TestNameSign() {
                return @"测试 String 转 Int";
            }
            public void TestMethod() {
                string str = Srource();
                Console.WriteLine("SourceData: \n {0}", str);
                float[] array = str.ToArrayList(',').ListConvertType(s => ConvertTool.ObjToFloat(s, 0f));
                Console.WriteLine("Result: \n {0}", JsonHelper.SerializeObject(array));
            }
        }
        private class StringToDecimal : ICase
        {
            public string TestNameSign() {
                return @"测试 String 转 Decimal";
            }
            public void TestMethod() {
                string str = Srource();
                Console.WriteLine("SourceData: \n {0}", str);
                decimal[] array = str.ToArrayList(',').ListConvertType(s => ConvertTool.ObjToDecimal(s, 0m));
                Console.WriteLine("Result: \n {0}", JsonHelper.SerializeObject(array));
            }
        }
        private class StringToBoolean : ICase
        {
            public string TestNameSign() {
                return @"测试 String 转 Boolean";
            }
            public void TestMethod() {
            string str = @"true,false,flsea,0,1,sliw,trus";

            Console.WriteLine("SourceData: \n {0}", str);
            bool[] array_1 = str.ToArrayList(',').ListConvertType(s => ConvertTool.ObjToBool(s, true));
            Console.WriteLine("Default: true Result: \n {0}", JsonHelper.SerializeObject(array_1));

            bool[] array_2 = str.ToArrayList(',').ListConvertType(s => ConvertTool.ObjToBool(s, false));
            Console.WriteLine("Default: false Result: \n {0}", JsonHelper.SerializeObject(array_2));
            }
        }
        #endregion
    }
}
