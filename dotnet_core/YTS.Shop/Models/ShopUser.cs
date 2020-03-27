using System;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace YTS.Shop
{
    /// <summary>
    /// 店铺下属用户
    /// </summary>
    public class ShopUser
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
        /// OpenID
        /// </summary>
        public string OpenID { get; set; }

        /// <summary>
        /// UnionID
        /// </summary>
        public string UnionID { get; set; }

        /// <summary>
        /// 店铺信息实体
        /// </summary>
        public ShopInfo ShopInfo { get; set; }
    }
}
