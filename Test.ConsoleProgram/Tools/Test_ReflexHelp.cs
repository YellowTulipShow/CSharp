using System;
using System.Reflection;
using YTS.Engine.DataBase;
using YTS.Model.DB;
using YTS.Tools;

namespace Test.ConsoleProgram.Tools
{
    public class Test_ReflexHelp : CaseModel
    {
        public Test_ReflexHelp() {
            this.NameSign = @"静态反射帮助类";
            this.SonCases = new CaseModel[] {
                Func_Name(),
                Func_CloneModelData(),
                Func_AttributeFindOnly(),
            };
        }


        [EntityTable]
        public class TestModel : AbsBasicDataModel
        {
            /// <summary>
            /// ID标识
            /// </summary>
            [Explain(@"ID标识")]
            [Column]
            public int id { get { return _id; } set { _id = value; } }
            private int _id = 0;

            /// <summary>
            /// 金额
            /// </summary>
            [Column]
            public float Money { get { return _Money; } set { _Money = value; } }
            private float _Money = 0.0f;

            /// <summary>
            /// 名称
            /// </summary>
            [Explain(@"名称")]
            public string Name { get { return _Name; } set { _Name = value; } }
            private string _Name = string.Empty;

            /// <summary>
            /// 备注
            /// </summary>
            public string Remark { get { return _Remark; } set { _Remark = value; } }
            private string _Remark = string.Empty;
        }

        public class A : TestModel { }

        public class B : A { }

        public CaseModel Func_Name() {
            return new CaseModel() {
                NameSign = @"获取模型单个属性名称",
                ExeEvent = () => {
                    TestModel model = new TestModel();
                    string jx_name = ReflexHelp.Name(() => model.Remark);
                    return @"Remark" == jx_name;
                },
            };
        }

        public CaseModel Func_CloneModelData() {
            return new CaseModel() {
                NameSign = @"深度克隆模型数据",
                ExeEvent = () => {
                    TestModel one = new TestModel() {
                        id = 3453,
                        Money = 2328.324f,
                        Name = "UJEIJi",
                        Remark = "来滴覅网"
                    };

                    // 执行克隆
                    TestModel two = ReflexHelp.CloneProperties(one);

                    // 检查与原来是否一样
                    if (!two.id.Equals(one.id)) {
                        Console.WriteLine("与原来: id 不一样");
                        return false;
                    }
                    if (!two.Money.Equals(one.Money)) {
                        Console.WriteLine("与原来: Money 不一样");
                        return false;
                    }
                    if (!two.Name.Equals(one.Name)) {
                        Console.WriteLine("与原来: Name 不一样");
                        return false;
                    }
                    if (!two.Remark.Equals(one.Remark)) {
                        Console.WriteLine("与原来: Remark 不一样");
                        return false;
                    }

                    // 重新赋值
                    two.id = 6355555;
                    two.Money = 992929.00033f;
                    two.Name = "wefwefwef";
                    two.Remark = "咯微风";

                    // 再次检查与原来是否一样
                    if (two.id.Equals(one.id)) {
                        Console.WriteLine("重新赋值: id 一样");
                        return false;
                    }
                    if (two.Money.Equals(one.Money)) {
                        Console.WriteLine("重新赋值: Money 一样");
                        return false;
                    }
                    if (two.Name.Equals(one.Name)) {
                        Console.WriteLine("重新赋值: Name 一样");
                        return false;
                    }
                    if (two.Remark.Equals(one.Remark)) {
                        Console.WriteLine("重新赋值: Remark 一样");
                        return false;
                    }

                    // 都不一样, 证明成功
                    return true;
                },
            };
        }

        public CaseModel Func_AttributeFindOnly() {
            return new CaseModel() {
                NameSign = @"查找元数据某一种特性",
                ExeEvent = () => {
                    Type type = typeof(TestModel);
                    Func<string, bool, bool, bool> method = (name, isNull_explain, isNull_column) => {
                        PropertyInfo pi_id = type.GetProperty(name);
                        ExplainAttribute explain = ReflexHelp.AttributeFindOnly<ExplainAttribute>(pi_id);
                        if (CheckData.IsObjectNull(explain) != isNull_explain) {
                            Console.WriteLine("name: {0} isNull_explain: {1} 错误", name, isNull_explain);
                            return false;
                        }
                        ColumnAttribute column = ReflexHelp.AttributeFindOnly<ColumnAttribute>(pi_id);
                        if (CheckData.IsObjectNull(column) != isNull_column) {
                            Console.WriteLine("name: {0} isNull_column: {1} 错误", name, isNull_column);
                            return false;
                        }
                        return true;
                    };
                    if (!method("id", false, false)) {
                        return false;
                    }
                    if (!method("Money", true, false)) {
                        return false;
                    }
                    if (!method("Name", false, true)) {
                        return false;
                    }
                    if (!method("Remark", true, true)) {
                        return false;
                    }
                    return true;
                },

                SonCases = new CaseModel[] {
                    Func_AttributeFindOnly_Inherit(),
                },
            };
        }

        public CaseModel Func_AttributeFindOnly_Inherit() {
            return new CaseModel() {
                NameSign = @"继承查找",
                ExeEvent = () => {
                    Func<TestModel, bool, bool, bool> method = (model, isFindInherit, isNull) => {
                        // isFindInherit = true; // 查找继承链
                        Type type = model.GetType();
                        EntityTableAttribute et_attr = ReflexHelp.AttributeFindOnly<EntityTableAttribute>(type, isFindInherit);
                        // isNull = true; // 是否允许为空
                        if (CheckData.IsObjectNull(et_attr) != isNull) {
                            Console.WriteLine("Type: {0} isFindInherit: {1} 不符合 isNull: {2}", type.Name, isFindInherit, isNull);
                            return false;
                        }
                        return true;
                    };

                    if (!method(new TestModel(), true, false)) {
                        return false;
                    }
                    if (!method(new TestModel(), false, false)) {
                        return false;
                    }
                    if (!method(new A(), true, false)) {
                        return false;
                    }
                    if (!method(new A(), false, true)) {
                        return false;
                    }
                    if (!method(new B(), true, false)) {
                        return false;
                    }
                    if (!method(new B(), false, true)) {
                        return false;
                    }
                    return true;
                },
            };
        }
    }
}
