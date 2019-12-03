using System;
using System.Collections;
using System.Collections.Generic;
using YTS.Tools;

namespace Test.ConsoleProgram.Tools
{
    public class Test_CheckData : CaseModel
    {
        public Test_CheckData() {
            this.NameSign = @"检查数据";
            this.SonCases = new CaseModel[] {
                Func_IsTypeEqual(),
                Func_IsTypeEqualDepth(),
            };
        }

        public class IsTypeEqualModel
        {
            public Type T1 = null;
            public Type T2 = null;
            public bool IsEqual = true;

            public IsTypeEqualModel(Type t1, Type t2, bool isEqual) {
                this.T1 = t1;
                this.T2 = t2;
                this.IsEqual = isEqual;
            }
        }

        public CaseModel Func_IsTypeEqual() {
            return new CaseModel() {
                NameSign = @"是否类型相同",
                ExeEvent = () => {
                    IsTypeEqualModel[] list = new IsTypeEqualModel[] {
                        new IsTypeEqualModel(typeof(object), typeof(object), true),
                        new IsTypeEqualModel(typeof(int), typeof(object), false),
                        new IsTypeEqualModel(typeof(object), typeof(int), false),
                        new IsTypeEqualModel(typeof(int), typeof(int), true),
                        new IsTypeEqualModel(typeof(string), typeof(int), false),
                        new IsTypeEqualModel(typeof(int), typeof(string), false),
                        new IsTypeEqualModel(typeof(Enum), typeof(int), false),
                        new IsTypeEqualModel(null, typeof(int), false),
                        new IsTypeEqualModel(typeof(int), null, false),
                        new IsTypeEqualModel(typeof(string), typeof(string), true),
                        new IsTypeEqualModel(typeof(string), typeof(DateTime), false),
                        new IsTypeEqualModel(typeof(DateTime), typeof(string), false),
                        new IsTypeEqualModel(typeof(DateTime), typeof(DateTime), true),
                        new IsTypeEqualModel(typeof(string[]), typeof(List<string>), false),
                        new IsTypeEqualModel(typeof(string[]), typeof(IList<string>), false),
                        new IsTypeEqualModel(typeof(string[]), typeof(IEnumerable), false),
                        new IsTypeEqualModel(typeof(string[]), typeof(IEnumerable<string>), false),
                        new IsTypeEqualModel(typeof(IEnumerable<string>), typeof(IEnumerable<string>), true),
                        new IsTypeEqualModel(typeof(float), typeof(float), true),
                        new IsTypeEqualModel(typeof(float), typeof(double), false),
                        new IsTypeEqualModel(typeof(double), typeof(double), true),
                    };
                    foreach (IsTypeEqualModel model in list) {
                        bool CalcResult = CheckData.IsTypeEqual(model.T1, model.T2);
                        if (CalcResult != model.IsEqual) {
                            Console.WriteLine("结果出错! T1:{0}  T2:{1}  Answer:{2}  CalcResult:{3}",
                                model.T1.FullName,
                                model.T2.FullName,
                                model.IsEqual,
                                CalcResult);
                            return false;
                        }
                    }
                    return true;
                },
            };
        }

        public CaseModel Func_IsTypeEqualDepth() {
            return new CaseModel() {
                NameSign = @"是否类型相同 深入递归",
                ExeEvent = () => {
                    IsTypeEqualModel[] list = new IsTypeEqualModel[] {
                        new IsTypeEqualModel(typeof(object), typeof(object), true),
                        new IsTypeEqualModel(typeof(int), typeof(object), true),
                        new IsTypeEqualModel(typeof(object), typeof(int), false),
                        new IsTypeEqualModel(typeof(int), typeof(int), true),
                        new IsTypeEqualModel(typeof(string), typeof(int), false),
                        new IsTypeEqualModel(typeof(int), typeof(string), false),
                        new IsTypeEqualModel(typeof(Enum), typeof(int), false),
                        new IsTypeEqualModel(typeof(Enum), typeof(ValueType), true),
                        new IsTypeEqualModel(typeof(ValueType), typeof(Enum), false),
                        new IsTypeEqualModel(null, typeof(int), false),
                        new IsTypeEqualModel(typeof(int), null, false),
                        new IsTypeEqualModel(typeof(string), typeof(string), true),
                        new IsTypeEqualModel(typeof(string), typeof(DateTime), false),
                        new IsTypeEqualModel(typeof(DateTime), typeof(string), false),
                        new IsTypeEqualModel(typeof(DateTime), typeof(DateTime), true),
                        new IsTypeEqualModel(typeof(string[]), typeof(List<string>), false),

                        // 这里就有疑问了:
                        // IList<T> list = new T[] { }; // 这句代码并没有报错, 是完全可用的
                        // https://docs.microsoft.com/zh-cn/dotnet/csharp/programming-guide/generics/generics-and-arrays
                        new IsTypeEqualModel(typeof(string[]), typeof(IList<string>), false),
                        new IsTypeEqualModel(typeof(string[]), typeof(IEnumerable), false),
                        new IsTypeEqualModel(typeof(string[]), typeof(IEnumerable<string>), false),
                        new IsTypeEqualModel(typeof(string[]), typeof(IEnumerable<int>), false),

                        new IsTypeEqualModel(typeof(float), typeof(float), true),
                        new IsTypeEqualModel(typeof(float), typeof(double), false),
                        new IsTypeEqualModel(typeof(double), typeof(double), true),
                    };
                    foreach (IsTypeEqualModel model in list) {
                        bool CalcResult = CheckData.IsTypeEqualDepth(model.T1, model.T2, true);
                        if (CalcResult != model.IsEqual) {
                            Console.WriteLine("结果出错! T1:{0}  T2:{1}  Answer:{2}  CalcResult:{3}",
                                model.T1.FullName,
                                model.T2.FullName,
                                model.IsEqual,
                                CalcResult);
                            return false;
                        }
                    }
                    return true;
                },
            };
        }
    }
}
