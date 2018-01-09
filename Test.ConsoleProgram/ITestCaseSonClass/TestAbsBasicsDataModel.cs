using System;
using CSharp.ApplicationData;
using CSharp.LibrayFunction;

namespace Test.ConsoleProgram.ITestCaseSonClass
{
    public class TestAbsBasicsDataModel : ITestCase
    {
        public string TestNameSign() {
            return @"测试 CSharp.LibrayFunction 中的基础数据模型 AbsBasicsDataModel";
        }

        public void TestMethod() {
            Console.WriteLine("测试 CloneModelData 克隆方法");
            AbsBasicsDataModel baseModel = new ModelArticles();
            Console.WriteLine("源 模型: ");
            Console.WriteLine(baseModel.ToString());
            Console.WriteLine("");

            ModelArticles clModel = (ModelArticles)baseModel.CloneModelData();
            Console.WriteLine("克隆 模型: ");
            Console.WriteLine(clModel.ToString());
            Console.WriteLine("");

            clModel.id = 4;
            clModel.Money = 78M;
            clModel.Remark = "备3注3内3容222";
            Console.WriteLine("克隆 模型 (更改值后): ");
            Console.WriteLine(clModel.ToString());
            Console.WriteLine("");

            Console.WriteLine("源 模型: ");
            Console.WriteLine(baseModel.ToString());
        }
    }
}
