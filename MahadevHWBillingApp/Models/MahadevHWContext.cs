using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace MahadevHWBillingApp.Models
{
    public class MahadevHWContext : DbContext
    {
        public static readonly string ConnectionString =
            ConfigurationManager.ConnectionStrings["GstContext"].ConnectionString;

        public DbSet<Item> Items { get; set; }
        public DbSet<Profile> Profiles { get; set; }
        public DbSet<Purchase> Purchase { get; set; }
        public DbSet<Sale> Sales { get; set; }

        public MahadevHWContext()
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            builder.UseSqlite(ConnectionString);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Item>();
            builder.Entity<Profile>();
            builder.Entity<Purchase>();
            builder.Entity<Sale>();
            base.OnModelCreating(builder);
        }
    }
}