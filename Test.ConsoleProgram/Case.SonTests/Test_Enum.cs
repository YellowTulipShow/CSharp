using System;
using System.Collections.Generic;
using System.Reflection;
using CSharp.LibrayDataBase;
using CSharp.LibrayFunction;

namespace Test.ConsoleProgram.Case.SonTests
{
    public class Test_Enum : CaseModel
    {
        public Test_Enum() {
            base.NameSign = @"测试枚举类型 的自定义扩展";
            base.ExeEvent = () => { };
            base.SonCases = new CaseModel[] {
                //new CaseModel() {
                //    NameSign = @"获得枚举名称",
                //    ExeEvent = GetName_Method,
                //},
                //new CaseModel() {
                //    NameSign = @"获得枚举 int 值",
                //    ExeEvent = GetIntValue_Method,
                //},
                //new CaseModel() {
                //    NameSign =  @"获得枚举 解释内容",
                //    ExeEvent = GetExplain_Method,
                //},
                //new CaseModel() {
                //    NameSign =  @"反向生成枚举值",
                //    ExeEvent = CreateEnumValue,
                //},

                Analysis_Enum(),
            };
        }

        public enum LEKEY
        {
            [Explain(@"键")]
            Key = 0,
            [Explain(@"值")]
            Value = 7,
        }

        public CaseModel Analysis_Enum() {
            return new CaseModel() {
                NameSign = @"解析枚举类型值",
                ExeEvent = () => {
                    //EnumInfo[] model EnumGetInfo<E>();
                    foreach (EnumInfo model in EnumInfo.EnumGetInfo<LEKEY>()) {
                        Print.WriteLine("Name: {0} IntValue: {1} Explain: {2}", model.Name, model.IntValue, model.Explain);
                    }
                },
            };
        }

        #region old
        public void GetName_Method() {
            Print.WriteLine("LEKEY.Key.GetName() 结果: ");
            Print.WriteLine(LEKEY.Key.GetName());
            Print.WriteLine("LEKEY.Value.GetName() 结果: ");
            Print.WriteLine(LEKEY.Value.GetName());
        }
        public void GetIntValue_Method() {
            Print.WriteLine("LEKEY.Key.GetIntValue() 结果: ");
            Print.WriteLine(LEKEY.Key.GetIntValue());
            Print.WriteLine("LEKEY.Value.GetIntValue() 结果: ");
            Print.WriteLine(LEKEY.Value.GetIntValue());
        }
        public void GetExplain_Method() {
            Print.WriteLine("LEKEY.Key.GetExplain().Text 结果: ");
            Print.WriteLine(LEKEY.Key.GetExplain().Text);
            Print.WriteLine("LEKEY.Value.GetExplain().Text 结果: ");
            Print.WriteLine(LEKEY.Value.GetExplain().Text);
        }


        [Table]
        public class TestTypeModel : AbsModelNull
        {
            public override string GetTableName() {
                return @"dt_TestTypeModel";
            }

            /// <summary>
            /// 性别值
            /// </summary>
            [Explain(@"性别值")]
            public enum SexEnum
            {
                /// <summary>
                /// 保密
                /// </summary>
                [Explain(@"保密")]
                Secrecy = 0,
                /// <summary>
                /// 男
                /// </summary>
                [Explain(@"男")]
                Male = 1,
                /// <summary>
                /// 女
                /// </summary>
                [Explain(@"女")]
                Female = 2,
            }
            /// <summary>
            /// 性别
            /// </summary>
            [Explain(@"性别")]
            [Column(MSQLServerDTParser.DTEnum.Int)]
            public SexEnum Sex {
                get { return _sex; }
                set {
                    if (Enum.IsDefined(typeof(SexEnum), value)) {
                        _sex = value;
                    }
                }
            }
            private SexEnum _sex = SexEnum.Secrecy;


            /// <summary>
            /// 真实姓名
            /// </summary>
            [Explain(@"真实姓名")]
            [Column(MSQLServerDTParser.DTEnum.NVarChar, CharLength = 30, SortIndex = 10)]
            public string RealName { get { return _realName; } set { _realName = value; } }
            private string _realName = string.Empty;
        }
        public void CreateEnumValue() {
            ColumnModelParser<TestTypeModel> modelParser = new ColumnModelParser<TestTypeModel>();

            TestTypeModel ttm = modelParser.CreateDefaultModel();
            foreach (ColumnItemModel item in modelParser.ColumnInfoArray) {
                Print.WriteLine(item.Property.Name);
                Print.WriteLine(item.Explain.Text);

                object vvv = @"责任区";
                if (item.Property.PropertyType.BaseType == typeof(Enum)) {
                    //vvv = new Random().Next(1, 3);
                    vvv = 1;
                }

                Print.WriteLine(item.Property.PropertyType.Name);
                ttm = modelParser.SetModelValue(item, ttm, vvv);

                Print.WriteLine(string.Empty);
            }

            Print.WriteLine(ttm.Sex.ToString());
            Print.WriteLine(ttm.ToJson());
            //Enum.Parse()
        }
        #endregion

        public static object List { get; set; }
    }
}
