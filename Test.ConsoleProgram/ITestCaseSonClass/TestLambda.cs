using System;
using System.Collections.Generic;
using System.Linq;
using CSharp.ApplicationData;
using CSharp.LibrayFunction;

namespace Test.ConsoleProgram.ITestCaseSonClass
{
    public class TestLambda : AbsTestCase
    {
        public override string TestNameSign() {
            return @"测试 Lambda 表达式 查询的速度";
        }

        public override void TestMethod() {
            ModelArticles[] ml = SourceList();
            Console.WriteLine(string.Format("数据源个数:{0}", ml.Length));

            Console.WriteLine(string.Format("YesLambda 个数:{0}", YesLambda(ml).Length));
            Console.WriteLine(string.Format("NoLambda 个数:{0}", NoLambda(ml).Length));
        }

        private ModelArticles[] SourceList() {
            List<ModelArticles> list = new List<ModelArticles>();
            for (int i = 1; i < 500000; i++) {
                if (i == 26) {
                    list.Add(null);
                    continue;
                }
                list.Add(new ModelArticles() {
                    id = new Random().Next(i * 5 + 7),
                    Money = new Random().Next(i * 6 - 4),
                    Remark = string.Format("备注{0}号",i),
                    TimeAdd = DateTime.Now
                });
            }
            return list.ToArray();
        }

        private bool whereIf(int v) {
            return 25 < v && v < 536;
        }

        private ModelArticles[] YesLambda(ModelArticles[] ml) {
            return ml.Where(m => CheckData.IsObjectNull(m) ? false : whereIf(m.id)).ToArray();
        }
        private ModelArticles[] NoLambda(ModelArticles[] ml) {
            List<ModelArticles> list = new List<ModelArticles>();
            foreach (ModelArticles model in ml) {
                if (CheckData.IsObjectNull(model) ? false : whereIf(model.id)) {
                    list.Add(model);
                }
            }
            return list.ToArray();
        }
    }
}
