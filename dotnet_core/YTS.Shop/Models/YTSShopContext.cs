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
            modelBuilder.Entity<Shop_Info>().ToTable("Shop_Info");
            modelBuilder.Entity<Shop_ManagerGroup>().ToTable("Shop_ManagerGroup");
            modelBuilder.Entity<Shop_Manager>().ToTable("Shop_Manager");
            modelBuilder.Entity<Shop_Administrator>().ToTable("Shop_Administrator");
            modelBuilder.Entity<Shop_UserGroup>().ToTable("Shop_UserGroup");
            modelBuilder.Entity<Shop_User>().ToTable("Shop_User");
            modelBuilder.Entity<Shop_Product>().ToTable("Shop_Product");
        }

        public DbSet<Shop_Info> Shop_Info { get; set; }
        public DbSet<Shop_ManagerGroup> Shop_ManagerGroup { get; set; }
        public DbSet<Shop_Manager> Shop_Manager { get; set; }
        public DbSet<Shop_Administrator> Shop_Administrator { get; set; }
        public DbSet<Shop_UserGroup> Shop_UserGroup { get; set; }
        public DbSet<Shop_User> Shop_User { get; set; }
        public DbSet<Shop_Product> Shop_Product { get; set; }
    }
}
