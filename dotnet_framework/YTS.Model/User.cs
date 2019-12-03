using System;
using YTS.Engine.DataBase;
using YTS.Model.DB;
using YTS.Tools;
using YTS.Tools.Const;

namespace YTS.Model
{
    /// <summary>
    /// 数据模型类: 用户
    /// </summary>
    [Explain(@"用户")]
    [EntityTable]
    public class User : AbsTable_StringID
    {
        public override Enums.UseCodeMark GetUseCode() {
            return Enums.UseCodeMark.User;
        }

        public override string GetTableName() {
            return @"dt_User";
        }

        #region === Model ===
        /// <summary>
        /// 昵称
        /// </summary>
        [Explain(@"昵称")]
        [Column]
        public string NickName { get { return _nickName; } set { _nickName = value; } }
        private string _nickName = string.Empty;

        /// <summary>
        /// 密码
        /// </summary>
        [Explain(@"密码")]
        [Column]
        public string Password { get { return _password; } set { _password = value; } }
        private string _password = string.Empty;

        /// <summary>
        /// 真实姓名
        /// </summary>
        [Explain(@"真实姓名")]
        [Column]
        public string RealName { get { return _realName; } set { _realName = value; } }
        private string _realName = string.Empty;

        /// <summary>
        /// 邮箱
        /// </summary>
        [Explain(@"邮箱")]
        [Column]
        public string Email { get { return _email; } set { _email = value; } }
        private string _email = string.Empty;

        /// <summary>
        /// 电话座机
        /// </summary>
        [Explain(@"电话座机")]
        [Column]
        public string TelePhone { get { return _telePhone; } set { _telePhone = value; } }
        private string _telePhone = string.Empty;

        /// <summary>
        /// 移动电话手机
        /// </summary>
        [Explain(@"移动电话手机")]
        [Column]
        public string MobilePhone { get { return _mobilePhone; } set { _mobilePhone = value; } }
        private string _mobilePhone = string.Empty;

        /// <summary>
        /// 性别
        /// </summary>
        [Explain(@"性别")]
        [Column]
        public Enums.SexEnum Sex { get { return _sex; } set { _sex = value; } }
        private Enums.SexEnum _sex = Enums.SexEnum.Secrecy;

        /// <summary>
        /// 生日
        /// </summary>
        [Explain(@"生日")]
        [Column]
        public DateTime Birthday { get { return _Birthday; } set { _Birthday = value; } }
        private DateTime _Birthday = DateTime.Now;
        #endregion
    }
}
