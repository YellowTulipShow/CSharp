using System;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace YTS.Shop
{
    /// <summary>
    /// 店铺最高管理员
    /// </summary>
    public class Shop_Administrator
    {
        /// <summary>
        /// ID
        /// </summary>
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        /// <summary>
        /// 管理员ID
        /// </summary>
        public int ManagerID { get; set; }
    }
}
