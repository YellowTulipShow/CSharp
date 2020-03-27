using System;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace YTS.Shop
{
    /// <summary>
    /// 店铺管理员组
    /// </summary>
    public class Shop_ManagerGroup
    {
        /// <summary>
        /// ID
        /// </summary>
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        /// <summary>
        /// 店铺信息ID
        /// </summary>
        public int? ShopInfoID { get; set; }

        /// <summary>
        /// 组名
        /// </summary>
        public string GroupName { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }

        /// <summary>
        /// 添加时间
        /// </summary>
        public DateTime? AddTime { get; set; }

        /// <summary>
        /// 添加人Id
        /// </summary>
        public int? AddManagerID { get; set; }
    }
}
