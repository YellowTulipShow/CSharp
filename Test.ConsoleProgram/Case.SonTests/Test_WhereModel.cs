using System;
using System.Collections.Generic;
using CSharp.ApplicationData;
using CSharp.LibrayDataBase;
using CSharp.LibrayFunction;

namespace Test.ConsoleProgram.Case.SonTests
{
    public class Test_WhereModel : CaseModel
    {
        public Test_WhereModel() {
            this.NameSign = @"条件模型搭建";
            this.ExeEvent = () => { };
            this.SonCases = new CaseModel[] {
                new CaseModel(){
                    NameSign = @"Json 查看结果实现",
                    ExeEvent = JsonShow,
                },
                new CaseModel(){
                    NameSign = @"检查模型是否可以使用",
                    ExeEvent = CheckModelIsCanUse,
                },
                new CaseModel(){
                    NameSign = @"SQL 解析条件模型",
                    ExeEvent = SQL_Parser_WhereModel,
                },
            };
        }
        public void JsonShow() {
            string json = JsonHelper.SerializeObject(DataSrouce_1());
            Print.WriteLine(json);
            Print.WriteLine(new WhereModel());
        }
        public WhereModel[] DataSrouce_1() {
            return new WhereModel[] {
                new WhereModel(DataChar.LogicChar.OR) {
                    Wheres = new WhereModel[] {
                        new WhereModel(DataChar.LogicChar.OR) {
                            FielVals = new FieldValueModel[] {
                                new FieldValueModel(DataChar.OperChar.IN) {
                                    Name = "id",
                                    Value = "8987"
                                },
                                new FieldValueModel(DataChar.OperChar.IN) {
                                    Name = "id",
                                    Value = "8987"
                                },
                                new FieldValueModel(DataChar.OperChar.IN) {
                                    Name = "id",
                                    Value = "8987"
                                },
                            },
                        },
                        new WhereModel(DataChar.LogicChar.OR) {
                            FielVals = new FieldValueModel[] {
                                new FieldValueModel(DataChar.OperChar.IN) {
                                    Name = "id",
                                    Value = "8987"
                                },
                                new FieldValueModel(DataChar.OperChar.IN) {
                                    Name = "id",
                                    Value = "8987"
                                },
                                new FieldValueModel(DataChar.OperChar.IN) {
                                    Name = "id",
                                    Value = "8987"
                                },
                            },
                        },
                        new WhereModel(DataChar.LogicChar.OR) {
                            FielVals = new FieldValueModel[] {
                                new FieldValueModel(DataChar.OperChar.IN) {
                                    Name = "id",
                                    Value = "8987"
                                },
                                new FieldValueModel(DataChar.OperChar.IN) {
                                    Name = "id",
                                    Value = "8987"
                                },
                                new FieldValueModel(DataChar.OperChar.IN) {
                                    Name = "id",
                                    Value = "8987"
                                },
                            },
                        },
                    },
                    FielVals = new FieldValueModel[] {
                        new FieldValueModel(DataChar.OperChar.IN) {
                            Name = "id",
                            Value = "8987"
                        },
                        new FieldValueModel(DataChar.OperChar.IN) {
                            Name = "id",
                            Value = "8987"
                        },
                        new FieldValueModel(DataChar.OperChar.IN) {
                            Name = "id",
                            Value = "8987"
                        },
                    },
                },
            };
        }

        public void CheckModelIsCanUse() {
            FieldValueModel fvmodel_1 = new FieldValueModel() {
                Name = "wwww",
                Value = "5468"
            };
            Print.WriteLine("FV 两个给: " + FieldValueModel.CheckIsCanUse(fvmodel_1).ToString());
            FieldValueModel fvmodel_2 = new FieldValueModel() {
                Value = "5468"
            };
            Print.WriteLine("FV 只给Value: " + FieldValueModel.CheckIsCanUse(fvmodel_2).ToString());
            FieldValueModel fvmodel_3 = new FieldValueModel() {
                Name = "wwww",
            };
            Print.WriteLine("FV 只给Name: " + FieldValueModel.CheckIsCanUse(fvmodel_3).ToString());



            WhereModel model_1 = null;
            Print.WriteLine("1: 赋值Null: " + WhereModel.CheckIsCanUse(model_1).ToString());

            WhereModel model_2 = new WhereModel();
            Print.WriteLine("2: 只new: " + WhereModel.CheckIsCanUse(model_2).ToString());

            WhereModel model_3 = new WhereModel() {
                Wheres = new WhereModel[] { model_2 },
            };
            Print.WriteLine("3: 只给了Where 但给的是 model_2: " + WhereModel.CheckIsCanUse(model_3).ToString());

            WhereModel model_4 = new WhereModel() {
                FielVals = new FieldValueModel[] { fvmodel_2 },
            };
            Print.WriteLine("4: 只给了FielVals, 但 fvmodel_2 没有name: " + WhereModel.CheckIsCanUse(model_4).ToString());

            WhereModel model_5 = new WhereModel() {
                FielVals = new FieldValueModel[] { fvmodel_3 },
            };
            Print.WriteLine("5: 只给了FielVals, 并且 fvmodel_3 有name: " + WhereModel.CheckIsCanUse(model_5).ToString());


            WhereModel model_6 = new WhereModel() {
                Wheres = new WhereModel[] { model_5 }
            };
            Print.WriteLine("6: 只给了Wheres, 并且 给的是 model_5: " + WhereModel.CheckIsCanUse(model_6).ToString());



            FieldOrderModel fomodel_1 = new FieldOrderModel() {
                Name = "wwww",
                IsAsc = true,
            };
            Print.WriteLine("FO 两个给: " + FieldOrderModel.CheckIsCanUse(fomodel_1).ToString());
            FieldOrderModel fomodel_2 = new FieldOrderModel() {
                IsAsc = false,
            };
            Print.WriteLine("FO 只给IsAsc: " + FieldOrderModel.CheckIsCanUse(fomodel_2).ToString());
            FieldOrderModel fomodel_3 = new FieldOrderModel() {
                Name = "wwww",
            };
            Print.WriteLine("FO 只给Name: " + FieldOrderModel.CheckIsCanUse(fomodel_3).ToString());
        }


        private string[] DBS_ClNs() {
            ModelArticles articleM = new ModelArticles();
            string ClName_id = ReflexHelper.Name(() => articleM.id);
            string ClName_Content = ReflexHelper.Name(() => articleM.Content);
            string ClName_Money = ReflexHelper.Name(() => articleM.Money);
            string ClName_Remark = ReflexHelper.Name(() => articleM.Remark);
            string ClName_TimeAdd = ReflexHelper.Name(() => articleM.TimeAdd);
            return new string[] {
                ClName_id, ClName_Content, ClName_Money, ClName_Remark, ClName_TimeAdd
            };
        }
        private string[] DBS_RVs() {
            return new string[] {
                "https://graph", "qq", "com/oauth2", "0/show?which", "Login", "display", "pc", "client", "id", "100312028", "response", "type", "code", "display", "pc", "state", "1521251649", "redirect", "uri", "https", "3A", "2F", "2Fpassport", "baidu", "com", "2Fphoenix", "2Faccount", "2Fafterauth", "3Fmkey", "3D5011b7915f279c2178dd803a7cf01c43", "scope", "get", "user", "info", "get", "other", "info", "add", "t", "add", "share"
            };
        }
        private DataChar.LogicChar[] DBS_Logics() {
            return new DataChar.LogicChar[] {
                DataChar.LogicChar.AND,
                DataChar.LogicChar.OR,
            };
        }
        private DataChar.OperChar[] DBS_Opers() {
            return new DataChar.OperChar[] {
                DataChar.OperChar.EQUAL,
                DataChar.OperChar.EQUAL_NOT,
                DataChar.OperChar.LIKE,
                DataChar.OperChar.IN,
                DataChar.OperChar.IN_NOT,
                DataChar.OperChar.BigTHAN,
                DataChar.OperChar.BigTHAN_EQUAL,
                DataChar.OperChar.SmallTHAN,
                DataChar.OperChar.SmallTHAN_EQUAL,
            };
        }
        public void SQL_Parser_WhereModel() {
            string[] ClNs = DBS_ClNs();
            string[] RVs = DBS_RVs();
            DataChar.LogicChar[] Logics = DBS_Logics();
            DataChar.OperChar[] Opers = DBS_Opers();

            Random random = new Random();

            List<FieldOrderModel> FOmlist = new List<FieldOrderModel>();
            List<FieldValueModel> FVmlist = new List<FieldValueModel>();
            for (int i = 0; i < ClNs.Length - 1; i++) {
                FOmlist.Add(new FieldOrderModel() {
                    Name = ClNs[random.Next(0, ClNs.Length)],
                    IsAsc = random.Next(0, 2) == 0,
                });
                FVmlist.Add(new FieldValueModel(Opers[random.Next(0, Opers.Length)]) {
                    Name = ClNs[random.Next(0, ClNs.Length)],
                    Value = RVs[random.Next(0, RVs.Length)],
                });
            }


            Print.WriteLine("解析 排序模型 测试:");
            Print.WriteLine(JsonHelper.SerializeObject(FOmlist));
            Print.WriteLine(CreateSQL.ParserFieldOrderModel(FOmlist.ToArray()));
            Print.WriteLine("测试结束");

            Print.WriteLine(string.Empty);

            Print.WriteLine("解析 字段值模型 测试: ");
            Print.WriteLine(JsonHelper.SerializeObject(FVmlist));
            Print.WriteLine(JsonHelper.SerializeObject(CreateSQL.ParserFieldValueModel(FVmlist.ToArray())));
            Print.WriteLine("测试结束");

            Print.WriteLine(string.Empty);

            Print.WriteLine("解析 条件模型 测试: ");
            WhereModel wm = DBS_WM(4);
            Print.WriteLine(JsonHelper.SerializeObject(wm));
            Print.WriteLine(CreateSQL.ParserWhereModel(wm));
            Print.WriteLine("测试结束");
        }
        private WhereModel DBS_WM(int level) {
            if (level <= 0) {
                return null;
            }

            string[] ClNs = DBS_ClNs();
            string[] RVs = DBS_RVs();
            DataChar.LogicChar[] Logics = DBS_Logics();
            DataChar.OperChar[] Opers = DBS_Opers();

            Random random = new Random();

            WhereModel wModel = new WhereModel(Logics[random.Next(0, Logics.Length)]);
            List<FieldValueModel> FVmlist = new List<FieldValueModel>();
            for (int i = 0; i < random.Next(0, level); i++) {
                FVmlist.Add(new FieldValueModel(Opers[random.Next(0, Opers.Length)]) {
                    Name = ClNs[random.Next(0, ClNs.Length)],
                    Value = RVs[random.Next(0, RVs.Length)],
                });
            }
            wModel.FielVals = FVmlist.ToArray();
            List<WhereModel> whereMs = new List<WhereModel>();
            for (int i = 0; i < random.Next(0, level); i++) {
                WhereModel im = DBS_WM(level - 1);
                if (CheckData.IsObjectNull(im)) {
                    break;
                }
                whereMs.Add(im);
            }
            wModel.Wheres = whereMs.ToArray();

            return wModel;
        }
    }
}
