using System;
using CSharp.LibrayFunction;
using CSharp.LibrayDataBase;
using System.Security.Cryptography;
using CSharp.LibrayDataBase.MCSDataType;

namespace CSharp.ApplicationData
{

    /// <summary>
    /// 数据模型类: 用户
    /// </summary>
    [Explain(@"用户")]
    [Table]
    public class ModelUser : AbsModel_ID
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

        ///// <summary>
        ///// 性别
        ///// </summary>
        //[Explain(@"性别")]
        //[Column(MSSFieldTypeStruct.Int, CsTypeEnumSign = CsDTEnum.Enum)]
        //public int Sex {
        //    get { return _sex; }
        //    set {
        //        if (Enum.IsDefined(typeof(SexEnum), value)) {
        //            _sex = value;
        //        }
        //    }
        //}
        //private int _sex = SexEnum.Secrecy.GetIntValue();


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
    public class BLLUser : BLLSQLServer<ModelUser> {
        public BLLUser() : base(new DALSQLServer<ModelUser>()) { }

        //public override ModelUser DefaultDataModel() {
        //    return new ModelUser() {
        //        Email = @"1426689530@qq.com",
        //        MobilePhone = @"18563920971",
        //        NickName = @"YellowTulipShow",
        //        Password = @"zrqytspass",
        //        RealName = @"赵瑞青",
        //        Remark = @"System Developer",
        //        TelePhone = string.Empty,
        //        TimeAdd = DateTime.Now,
        //    };
        //}
    }
}
