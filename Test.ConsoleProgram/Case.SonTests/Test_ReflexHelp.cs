using System;
using System.Reflection;
using YTS.Model;
using YTS.Model.Attribute;
using YTS.Model.Table.Attribute;
using YTS.Tools;

namespace Test.ConsoleProgram.Case.SonTests
{
    public class Test_ReflexHelp : CaseModel
    {
        public Test_ReflexHelp() {
            this.NameSign = @"测试 反射帮助类的方法";
            this.SonCases = new CaseModel[] {
                Func_Name(),
                Func_CloneModelData(),
                Func_AttributeFindOnly(),
            };
        }

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
                    TestModel two = (TestModel)one.CloneModelData();
                    
                    // 检查与原来是否一样
                    if (!two.id.Equals(one.id)) {
                        return false;
                    }
                    if (!two.Money.Equals(one.Money)) {
                        return false;
                    }
                    if (!two.Name.Equals(one.Name)) {
                        return false;
                    }
                    if (!two.Remark.Equals(one.Remark)) {
                        return false;
                    }

                    // 重新赋值
                    two.id = 6355555;
                    two.Money = 992929.00033f;
                    two.Name = "wefwefwef";
                    two.Remark = "咯微风";

                    // 再次检查与原来是否一样
                    if (two.id.Equals(one.id)) {
                        return false;
                    }
                    if (two.Money.Equals(one.Money)) {
                        return false;
                    }
                    if (two.Name.Equals(one.Name)) {
                        return false;
                    }
                    if (two.Remark.Equals(one.Remark)) {
                        return false;
                    }

                    // 都不一样, 证明成功
                    return true;
                },
            };
        }

        public CaseModel Func_AttributeFindOnly() {
            return new CaseModel() {
                NameSign = @"执行的一个特性查找",
                ExeEvent = () => {
                    Type type = typeof(TestModel);
                    Func<string, bool, bool, bool> method = (name, isNull_explain, isNull_column) => {
                        PropertyInfo pi_id = type.GetProperty(name);
                        ExplainAttribute explain = ReflexHelp.AttributeFindOnly<ExplainAttribute>(pi_id);
                        if (CheckData.IsObjectNull(explain) != isNull_explain) {
                            return false;
                        }
                        ColumnAttribute column = ReflexHelp.AttributeFindOnly<ColumnAttribute>(pi_id);
                        if (CheckData.IsObjectNull(column) != isNull_column) {
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
            };
        }
    }
}
