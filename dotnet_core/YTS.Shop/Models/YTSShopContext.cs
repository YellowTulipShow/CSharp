using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace YTS.Shop
{
    public class YTSShopContext : DbContext
    {
        private IConfiguration _configuration;
        public YTSShopContext(IConfiguration configuration)
        {
            this._configuration = configuration;
        }

        public DbSet<ShopInfo> ShopInfo { get; set; }
        public DbSet<ShopUser> ShopUser { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlite("Data Source=YTSShop.db");
        }
    }
}
