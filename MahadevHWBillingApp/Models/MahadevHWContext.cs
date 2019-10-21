﻿using System;
using Microsoft.EntityFrameworkCore;
using System.Configuration;

namespace MahadevHWBillingApp.Models
{
    public class MahadevHWContext : DbContext
    {
        public static readonly string ConnectionString = Helper.Generic.GetConnectionString();


        public DbSet<Item> Items { get; set; }
        public DbSet<Profile> Profiles { get; set; }
        public DbSet<Purchase> Purchase { get; set; }
        public DbSet<Sale> Sales { get; set; }
        public DbSet<SaleItem> SaleItems { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<RecordPayment> RecordPayments { get; set; }
            
        public MahadevHWContext()
        {
            System.IO.Directory.CreateDirectory("C:\\SqlServerDataBase\\DataBase");
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
            builder.Entity<Purchase>().Ignore(e => e.FormatDate);
            builder.Entity<Sale>();
            builder.Entity<Contact>().Ignore(e => e.IsSaveNewCustomer);
            builder.Entity<RecordPayment>();
            base.OnModelCreating(builder);
        } 
    }
}