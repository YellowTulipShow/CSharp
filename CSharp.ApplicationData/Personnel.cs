using System;
using CSharp.LibrayFunction;
using CSharp.LibrayDataBase;

namespace CSharp.ApplicationData
{

    /// <summary>
    /// 数据模型类(抽象): 人员
    /// </summary>
    [Explain("人员")]
    public abstract class Personnel : AbsModel_ID {
        /// <summary>
        /// 昵称
        /// </summary>
        [Explain("昵称")]
        [Column(ColumnAttribute.FieldTypeCharCount.Nvarchar, 100)]
        public string NickName { get { return _nickName; } set { _nickName = value; } }
        private string _nickName = string.Empty;

        /// <summary>
        /// 真实姓名
        /// </summary>
        [Explain("真实姓名")]
        [Column(ColumnAttribute.FieldTypeCharCount.Nvarchar, 50)]
        public string RealName { get { return _realName; } set { _realName = value; } }
        private string _realName = string.Empty;

        /// <summary>
        /// 密码
        /// </summary>
        [Explain("密码")]
        [Column(ColumnAttribute.FieldTypeCharCount.Nvarchar, 200)]
        public string Password { get { return _password; } set { _password = value; } }
        private string _password = string.Empty;
    }


    /// <summary>
    /// 数据模型类: 用户
    /// </summary>
    [Explain("用户")]
    [Table]
    public class ModelUser : Personnel
    {
        public override string GetTableName() {
            return @"dt_User";
        }
    }

    /// <summary>
    /// 数据模型类: 用户
    /// </summary>
    [Explain("管理")]
    [Table]
    public class ModelAdmin : Personnel
    {
        public override string GetTableName() {
            return @"dt_User";
        }
    }
}
