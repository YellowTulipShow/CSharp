using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace YTS.Shop
{
    public class YTSShopContext : DbContext
    {
        public YTSShopContext(DbContextOptions<YTSShopContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ShopInfo>().ToTable("ShopInfo");
            modelBuilder.Entity<ShopUser>().ToTable("ShopUser");
        }

        public DbSet<ShopInfo> ShopInfo { get; set; }
        public DbSet<ShopUser> ShopUser { get; set; }
    }
}
