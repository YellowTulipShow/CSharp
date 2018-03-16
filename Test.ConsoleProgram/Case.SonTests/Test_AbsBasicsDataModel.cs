using System;
using CSharp.ApplicationData;
using CSharp.LibrayFunction;

namespace Test.ConsoleProgram.Case.SonTests
{
    public class Test_AbsBasicsDataModel : CaseModel
    {
        public Test_AbsBasicsDataModel() {
            base.NameSign = @"测试 CSharp.LibrayFunction 中的基础数据模型 AbsBasicsDataModel";
            base.ExeEvent = Method;
        }

        public void Method() {
            Print.WriteLine("测试 CloneModelData 克隆方法");
            AbsBasicDataModel baseModel = new ModelArticles();
            Print.WriteLine("源 模型: ");
            Print.WriteLine(baseModel.ToString());
            Print.WriteLine("");

            ModelArticles clModel = (ModelArticles)baseModel.CloneModelData();
            Print.WriteLine("克隆 模型: ");
            Print.WriteLine(clModel.ToString());
            Print.WriteLine("");

            clModel.id = 4;
            clModel.Money = 78M;
            clModel.Remark = "备3注3内3容222";
            Print.WriteLine("克隆 模型 (更改值后): ");
            Print.WriteLine(clModel.ToString());
            Print.WriteLine("");

            Print.WriteLine("源 模型: ");
            Print.WriteLine(baseModel.ToString());
        }
    }
}
