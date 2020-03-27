using System;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace YTS.Shop
{
    /// <summary>
    /// 产品
    /// </summary>
    public class Shop_Product
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
        /// 产品名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 产品价格
        /// </summary>
        public string Price { get; set; }

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
