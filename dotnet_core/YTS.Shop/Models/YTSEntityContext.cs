using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace YTS.Shop
{
    public class YTSEntityContext : DbContext
    {
        public YTSEntityContext(DbContextOptions<YTSEntityContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Administrator>().ToTable("Administrator");
            modelBuilder.Entity<Manager>().ToTable("Manager");
            modelBuilder.Entity<ManagerGroup>().ToTable("ManagerGroup");

            modelBuilder.Entity<Shop_Info>().ToTable("Shop_Info");
            modelBuilder.Entity<Shop_UserGroup>().ToTable("Shop_UserGroup");
            modelBuilder.Entity<Shop_User>().ToTable("Shop_User");
            modelBuilder.Entity<Shop_Product>().ToTable("Shop_Product");
        }

        public DbSet<Administrator> Administrator { get; set; }
        public DbSet<Manager> Manager { get; set; }
        public DbSet<ManagerGroup> ManagerGroup { get; set; }
        public DbSet<Shop_Info> Shop_Info { get; set; }
        public DbSet<Shop_UserGroup> Shop_UserGroup { get; set; }
        public DbSet<Shop_User> Shop_User { get; set; }
        public DbSet<Shop_Product> Shop_Product { get; set; }
    }
}
