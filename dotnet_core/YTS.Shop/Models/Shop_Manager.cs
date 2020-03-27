using System;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using YTS.Tools;

namespace YTS.Shop
{
    /// <summary>
    /// 店铺管理员
    /// </summary>
    public class Shop_Manager
    {
        /// <summary>
        /// ID
        /// </summary>
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        /// <summary>
        /// 店铺信息ID
        /// </summary>
        public int ShopInfoID { get; set; }

        /// <summary>
        /// 店铺信息实体
        /// </summary>
        public Shop_Info ShopInfo { get; set; }

        /// <summary>
        /// 用户组
        /// </summary>
        public int? UserGroupID { get; set; }

        /// <summary>
        /// 账号
        /// </summary>
        public string Account { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// 昵称
        /// </summary>
        public string NickName { get; set; }

        /// <summary>
        /// 真实姓名
        /// </summary>
        public string TrueName { get; set; }

        /// <summary>
        /// 电话
        /// </summary>
        public string Phone { get; set; }

        /// <summary>
        /// 添加时间
        /// </summary>
        public DateTime? AddTime { get; set; }

        /// <summary>
        /// 添加人Id
        /// </summary>
        public int? AddManagerID { get; set; }

        /// <summary>
        /// 加密密码
        /// </summary>
        /// <param name="Password">密码原文</param>
        public static string EncryptionPassword(string Password)
        {
            if (string.IsNullOrWhiteSpace(Password))
            {
                throw new NullReferenceException("请输入有效的密码原文!");
            }
            string md5 = EncryptionAlgorithm.MD5_16(Password);
            var symmetric = new EncryptionAlgorithm.Symmetric();
            var md5s = symmetric.Encrypto(md5);
            return EncryptionAlgorithm.MD5_32(md5s);
        }
    }
}
