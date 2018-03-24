using System;
using CSharp.LibrayFunction;
using CSharp.LibrayDataBase;

namespace Test.ConsoleProgram.Case.SonTests
{
    public class Test_BLLDALSQLServer : CaseModel
    {
        public Test_BLLDALSQLServer() {
            base.NameSign = @"开始测试 BLLDALSQLServer 解析器";
            base.ExeEvent = FatherMainMethod;
            base.SonCases = new CaseModel[] {
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
            };
        }

        protected BLLUser bllUser = null;
        public void FatherMainMethod() {
            Print.WriteLine(@"初始化一个BLL实例 BLLUser 输出其Json格式数据");
            bllUser = new BLLUser();
            string jsonBLL = JsonHelper.SerializeObject(bllUser);
            //Print.WriteLine(jsonBLL);
        }

        public void InsertData() {
            bool result = bllUser.Insert(new ModelUser() {
                Email = CommonData.Random_String(10),
                TelePhone = CommonData.Random_String(CommonData.ASCII_Number(), 12),
                MobilePhone = CommonData.Random_String(CommonData.ASCII_Number(), 11),
                NickName = CommonData.Random_String(CommonData.ASCII_UpperEnglish(), 30),
                Password = CommonData.Random_String(100),
                RealName = CommonData.Random_String(CommonData.ASCII_UpperEnglish(), 10),
                Remark = CommonData.Random_String(200),
                Sex = CommonData.Random_Item(ConvertTool.EnumForeachArray<ModelUser.SexEnum>()),
                TimeAdd = CommonData.Random_DateTime(),
            });
            Print.WriteLine(result);
        }
        public void DeleteData() {
            bool result = bllUser.Delete(new WhereModel(DataChar.LogicChar.OR) {
                IsAllowFieldRepeat = true,
                FielVals = new FieldValueModel[] {
                    new FieldValueModel(DataChar.OperChar.EQUAL) {
                        Name = bllUser.ColName_id,
                        Value = @"5",
                    },
                    new FieldValueModel(DataChar.OperChar.EQUAL) {
                        Name = bllUser.ColName_id,
                        Value = @"3",
                    },
                    new FieldValueModel(DataChar.OperChar.EQUAL) {
                        Name = bllUser.ColName_id,
                        Value = @"6",
                    },
                },
            });
            Print.WriteLine(result);
        }
        public void UpdateData() {
            bool result = bllUser.Update(new FieldValueModel[] {
                new FieldValueModel(DataChar.OperChar.EQUAL) {
                    Name = bllUser.ColName_Remark,
                    Value = @"'''",
                },
            }, new WhereModel(DataChar.LogicChar.AND) {
                FielVals = new FieldValueModel[] {
                    new FieldValueModel(DataChar.OperChar.EQUAL) {
                        Name = bllUser.ColName_id,
                        Value = @"2",
                    },
                },
            });
            Print.WriteLine(result);
        }
        public void SelectData() {
            ModelUser modeluser = bllUser.GetModel(new FieldValueModel() {
                Name = bllUser.ColName_id,
                Value = @"2",
            });
            Print.WriteLine("id: " + modeluser.id);
            Print.WriteLine("Remark: " + modeluser.Remark);
            Print.WriteLine("RealName: " + modeluser.RealName);
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
        protected class BLLUser : BLLSQLServer<ModelUser>
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

            public BLLUser() : base(new DALSQLServer<ModelUser>()) { }

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
