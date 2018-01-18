using System;
using CSharp.LibrayFunction;
using CSharp.LibrayDataBase;
using System.Security.Cryptography;

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
        /// 真实姓名
        /// </summary>
        [Explain(@"真实姓名")]
        [Column(MSSFieldTypeCharCount.NVarChar, 30)]
        public string RealName { get { return _realName; } set { _realName = value; } }
        private string _realName = string.Empty;

        /// <summary>
        /// 昵称
        /// </summary>
        [Explain(@"昵称")]
        [Column(MSSFieldTypeCharCount.NVarChar, 50)]
        public string NickName { get { return _nickName; } set { _nickName = value; } }
        private string _nickName = string.Empty;

        /// <summary>
        /// 密码
        /// </summary>
        [Explain(@"密码")]
        [Column(MSSFieldTypeCharCount.NVarChar, 36)]
        public string Password { get { return _password; } set { _password = value; } }
        private string _password = string.Empty;

        /// <summary>
        /// 邮箱
        /// </summary>
        [Explain(@"邮箱")]
        [Column(MSSFieldTypeCharCount.NVarChar, 300)]
        public string Email { get { return _email; } set { _email = value; } }
        private string _email = string.Empty;

        /// <summary>
        /// 电话座机
        /// </summary>
        [Explain(@"电话座机")]
        [Column(MSSFieldTypeCharCount.NVarChar, 12)]
        public string TelePhone { get { return _telePhone; } set { _telePhone = value; } }
        private string _telePhone = string.Empty;

        /// <summary>
        /// 移动电话手机
        /// </summary>
        [Explain(@"移动电话手机")]
        [Column(MSSFieldTypeCharCount.NVarChar, 11)]
        public string MobilePhone { get { return _mobilePhone; } set { _mobilePhone = value; } }
        private string _mobilePhone = string.Empty;
        #endregion
    }

    /// <summary>
    /// 数据逻辑类: 用户
    /// </summary>
    public class BLLUser : BLLSQLServer<ModelUser> {
        public BLLUser() : base(new DALSQLServer<ModelUser>()) { }

        public override ModelUser DefaultDataModels() {
            return new ModelUser() {
                Email = @"1426689530@qq.com",
                MobilePhone = @"18563920971",
                NickName = @"YellowTulipShow",
                Password = @"zrqytspass",
                RealName = @"赵瑞青",
                Remark = @"System Developer",
                TelePhone = string.Empty,
                TimeAdd = DateTime.Now,
            };
        }
    }

    /// <summary>
    /// 数据模型类: 组别
    /// </summary>
    [Explain(@"组别")]
    [Table]
    public class ModelGroup : AbsModel_ID
    {
        public ModelGroup() { }

        public override string GetTableName() {
            return @"dt_Group";
        }

        #region === Model ===
        /// <summary>
        /// 组别名称
        /// </summary>
        [Explain(@"组别名称")]
        [Column(MSSFieldTypeCharCount.NVarChar, 30)]
        public string Name { get { return _name; } set { _name = value; } }
        private string _name = string.Empty;

        /// <summary>
        /// 组别类型枚举
        /// </summary>
        [Explain(@"组别类型")]
        public enum GroupTypeEnum
        {
            /// <summary>
            /// 用户组类型
            /// </summary>
            [Explain(@"用户组")]
            UserGroup = 0,
            /// <summary>
            /// 系统组类型(最高权限组)
            /// </summary>
            [Explain(@"系统组(最高权限组)")]
            SystemGroup = 1
        }
        /// <summary>
        /// 组别类型-(使用枚举 enum GroupTypeEnum 赋值)
        /// </summary>
        [Explain(@"组别类型")]
        [Column(MSSFieldTypeStruct.Int)]
        public int GroupType {
            get { return _groupType; }
            set { _groupType = value; }
        }
        private int _groupType = (int)GroupTypeEnum.UserGroup;
        #endregion
    }
}
