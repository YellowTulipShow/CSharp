using System;
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
    }
}
