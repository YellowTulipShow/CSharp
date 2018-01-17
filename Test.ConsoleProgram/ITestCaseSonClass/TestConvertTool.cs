using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CSharp.LibrayFunction;

namespace Test.ConsoleProgram.ITestCaseSonClass
{
    public class TestConvertTool : ITestCase
    {
        public string TestNameSign() {
            return @"测试 转化工具";
        }

        public void TestMethod() {
            StringToInt();
            StringToString();
            StringToFloat();
            StringToDecimal();
            StringToBoolean();
        }

        private string Srource() {
            return @",15,8,3,r,2,ssss,55,5,4,34M,35,";
        }

        private void StringToInt() {
            string str = Srource();
            Console.WriteLine("SourceData: \n {0}", str);
            int[] array = ConvertTool.StringToIList<int>(str, ',', s => {
                return ConvertTool.ObjToInt(s, 0);
            });
            Console.WriteLine("Result: \n {0}", JsonHelper.SerializeObject(array));
        }

        private void StringToString() {
            string str = Srource();
            Console.WriteLine("SourceData: \n {0}", str);
            string[] array = ConvertTool.StringToIList<string>(str, ',', s => {
                return s;
            });
            Console.WriteLine("Result: \n {0}", JsonHelper.SerializeObject(array));
        }

        private void StringToFloat() {
            string str = Srource();
            Console.WriteLine("SourceData: \n {0}", str);
            float[] array = ConvertTool.StringToIList<float>(str, ',', s => {
                return ConvertTool.ObjToFloat(s, 0);
            });
            Console.WriteLine("Result: \n {0}", JsonHelper.SerializeObject(array));
        }

        private void StringToDecimal() {
            string str = Srource();
            Console.WriteLine("SourceData: \n {0}", str);
            decimal[] array = ConvertTool.StringToIList<decimal>(str, ',', s => {
                return ConvertTool.ObjToDecimal(s, 0);
            });
            Console.WriteLine("Result: \n {0}", JsonHelper.SerializeObject(array));
        }

        private void StringToBoolean() {
            string str = @"true,false,flsea,0,1,sliw,trus";
            Console.WriteLine("SourceData: \n {0}", str);
            bool[] array_1 = ConvertTool.StringToIList<bool>(str, ',', s => {
                return ConvertTool.ObjToBool(s, true);
            });
            Console.WriteLine("Default: true Result: \n {0}", JsonHelper.SerializeObject(array_1));
            bool[] array_2 = ConvertTool.StringToIList<bool>(str, ',', s => {
                return ConvertTool.ObjToBool(s, false);
            });
            Console.WriteLine("Default: false Result: \n {0}", JsonHelper.SerializeObject(array_2));
        }
    }
}
