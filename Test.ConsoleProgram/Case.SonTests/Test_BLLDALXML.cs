using System;
using CSharp.LibrayFunction;
using CSharp.LibrayDataBase;

namespace Test.ConsoleProgram.Case.SonTests
{
    public class Test_BLLDALXML : CaseModel
    {
        public Test_BLLDALXML() {
            base.NameSign = @"开始测试 BLLDALXML 解析器";
            base.ExeEvent = FatherMainMethod;
            base.SonCases = new CaseModel[] {
                Exe_GetAbsFilePath(),

                new CaseModel() {
                    NameSign = @"添加数据",
                    ExeEvent = InsertData,
                },
                new CaseModel() {
                    NameSign = @"删除数据",
                    ExeEvent = DeleteData,
                },
                new CaseModel() {
                    NameSign = @"更改数据",
                    ExeEvent = UpdateData,
                },
                new CaseModel() {
                    NameSign = @"查询数据",
                    ExeEvent = SelectData,
                },
                new CaseModel() {
                    NameSign = @"全部数据条数",
                    ExeEvent = ALLRecordCount,
                },
                SelectALLData(),
            };
        }

        protected BLLUser bllUser = null;
        public void FatherMainMethod() {
            Print.WriteLine(@"初始化一个BLL实例 BLLUser 输出其Json格式数据");
            bllUser = new BLLUser();
            //Print.WriteLine(JsonHelper.SerializeObject(bllUser));
        }

        public CaseModel Exe_GetAbsFilePath() {
            return new CaseModel() {
                NameSign = "输出文件路径",
                ExeEvent = () => {
                    Print.WriteLine(this.bllUser.GetFileAbsPath());
                },
            };
        }

        public void InsertData() {
            bool result = bllUser.Insert(new ModelUser() {
                Email = RandomData.GetString(RandomData.GetInt(5, 10)),
                TelePhone = RandomData.GetString(CommonData.ASCII_Number(), 12),
                MobilePhone = RandomData.GetString(CommonData.ASCII_Number(), 11),
                NickName = RandomData.GetString(CommonData.ASCII_UpperEnglish(), RandomData.GetInt(5, 16)),
                Password = RandomData.GetString(30),
                RealName = RandomData.GetChineseString(RandomData.GetInt(2, 4)),
                Remark = RandomData.GetString(RandomData.GetInt(10, 81)),
                Sex = RandomData.GetItem(ConvertTool.EnumForeachArray<ModelUser.SexEnum>()),
                TimeAdd = RandomData.GetDateTime(),
            });
            Print.WriteLine(result);
        }
        public void DeleteData() {
            bool result = bllUser.Delete(new WhereModel(DataChar.LogicChar.OR) {
                IsAllowFieldRepeat = true,
                FielVals = new FieldValueModel[] {
                    new FieldValueModel(DataChar.OperChar.EQUAL) {  Name = bllUser.ColName_id, Value = @"5", },
                    new FieldValueModel(DataChar.OperChar.EQUAL) {
                        Name = bllUser.ColName_id, Value = @"3",
                    },
                    new FieldValueModel(DataChar.OperChar.EQUAL) {
                        Name = bllUser.ColName_id, Value = @"6",
                    },
                },
            });
            Print.WriteLine(result);
        }
        public void UpdateData() {
            bool result = bllUser.Update(new FieldValueModel[] {
                new FieldValueModel(DataChar.OperChar.EQUAL) {
                    Name = bllUser.ColName_Remark,
                    Value = @"'846'&(&'",
                },
            }, new WhereModel(DataChar.LogicChar.AND) {
                FielVals = new FieldValueModel[] {
                    new FieldValueModel(DataChar.OperChar.EQUAL) {
                        Name = bllUser.ColName_id,
                        Value = @"34",
                    },
                },
            });
            Print.WriteLine(result);
        }
        public void SelectData() {
            for (int i = 0; i < 5; i++) {
                SelectModelUser(new FieldValueModel() {
                    Name = bllUser.ColName_id,
                    Value = RandomData.GetInt(1, 55).ToString(),
                });
            }
        }
        public CaseModel SelectALLData() {
            return new CaseModel() {
                NameSign = @"全部数据记录",
                ExeEvent = () => {
                    ModelUser[] model_list = bllUser.Select();
                    foreach (ModelUser item in model_list) {
                        Print.WriteLine(JsonHelper.SerializeObject(item));
                    }
                },
            };
        }
        private void SelectModelUser(FieldValueModel fieldValueWhereModel) {
            ModelUser modeluser = bllUser.GetModel(fieldValueWhereModel);
            if (CheckData.IsObjectNull(modeluser)) {
                Print.WriteLine("没有查到 id = {0} 的数据", fieldValueWhereModel.Value);
            } else {
                //Print.WriteLine("id: " + modeluser.id);
                //Print.WriteLine("RealName: " + modeluser.RealName);
                //Print.WriteLine("Sex: " + modeluser.Sex.GetName());
                Print.WriteLine(JsonHelper.SerializeObject(modeluser));
            }
        }
        public void ALLRecordCount() {
            int recordCount = bllUser.GetRecordCount(null);
            Print.WriteLine("数据条数: {0}", recordCount);
        }

        #region === 测试模型 ===
        /// <summary>
        /// 数据模型类: 用户
        /// </summary>
        [Explain(@"用户")]
        [Table]
        protected class ModelUser : AbsModel_ID
        {
            public ModelUser() { }

            public override string GetTableName() {
                return @"dt_User";
            }

            #region === Model ===
            /// <summary>
            /// 昵称
            /// </summary>
            [Explain(@"昵称")]
            [Column(MSQLServerDTParser.DTEnum.NVarChar, CharLength = 50, SortIndex = 11)]
            public string NickName { get { return _nickName; } set { _nickName = value; } }
            private string _nickName = string.Empty;

            /// <summary>
            /// 密码
            /// </summary>
            [Explain(@"密码")]
            [Column(MSQLServerDTParser.DTEnum.NVarChar, CharLength = 36, SortIndex = 12)]
            public string Password { get { return _password; } set { _password = value; } }
            private string _password = string.Empty;

            /// <summary>
            /// 邮箱
            /// </summary>
            [Explain(@"邮箱")]
            [Column(MSQLServerDTParser.DTEnum.NVarChar, CharLength = 300, SortIndex = 13)]
            public string Email { get { return _email; } set { _email = value; } }
            private string _email = string.Empty;

            /// <summary>
            /// 电话座机
            /// </summary>
            [Explain(@"电话座机")]
            [Column(MSQLServerDTParser.DTEnum.NVarChar, CharLength = 12, SortIndex = 14)]
            public string TelePhone { get { return _telePhone; } set { _telePhone = value; } }
            private string _telePhone = string.Empty;

            /// <summary>
            /// 移动电话手机
            /// </summary>
            [Explain(@"移动电话手机")]
            [Column(MSQLServerDTParser.DTEnum.NVarChar, CharLength = 11, SortIndex = 15)]
            public string MobilePhone { get { return _mobilePhone; } set { _mobilePhone = value; } }
            private string _mobilePhone = string.Empty;

            /// <summary>
            /// 真实姓名
            /// </summary>
            [Explain(@"真实姓名")]
            [Column(MSQLServerDTParser.DTEnum.NVarChar, CharLength = 30, SortIndex = 10)]
            public string RealName { get { return _realName; } set { _realName = value; } }
            private string _realName = string.Empty;

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
            public SexEnum Sex { get { return _sex; } set { _sex = value; } }
            private SexEnum _sex = SexEnum.Secrecy;
            #endregion
        }
        /// <summary>
        /// 数据逻辑类: 用户
        /// </summary>
        protected class BLLUser : BLLXML<DALXML<ModelUser>, ModelUser>
        {
            private static readonly ModelUser defModel = new ModelUser();
            public readonly string ColName_id = ReflexHelper.Name(() => defModel.id);
            public readonly string ColName_Email = ReflexHelper.Name(() => defModel.Email);
            public readonly string ColName_MobilePhone = ReflexHelper.Name(() => defModel.MobilePhone);
            public readonly string ColName_NickName = ReflexHelper.Name(() => defModel.NickName);
            public readonly string ColName_Password = ReflexHelper.Name(() => defModel.Password);
            public readonly string ColName_RealName = ReflexHelper.Name(() => defModel.RealName);
            public readonly string ColName_Remark = ReflexHelper.Name(() => defModel.Remark);
            public readonly string ColName_Sex = ReflexHelper.Name(() => defModel.Sex);
            public readonly string ColName_TelePhone = ReflexHelper.Name(() => defModel.TelePhone);
            public readonly string ColName_TimeAdd = ReflexHelper.Name(() => defModel.TimeAdd);

            public BLLUser() : base(new DALXML<ModelUser>()) { }

            public override ModelUser[] DefaultDataModel() {
                return new ModelUser[] {
                    new ModelUser() {
                        Email = @"1426689530@qq.com",
                        MobilePhone = @"18563920971",
                        NickName = @"YellowTulipShow",
                        Password = @"zrqytspass",
                        RealName = @"赵瑞青",
                        Remark = @"System Developer",
                        Sex = ModelUser.SexEnum.Male,
                        TelePhone = string.Empty,
                        TimeAdd = DateTime.Now,
                    },
                };
            }
        }
        #endregion
    }
}
