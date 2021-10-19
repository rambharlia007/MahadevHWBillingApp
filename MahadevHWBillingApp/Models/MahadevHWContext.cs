using Microsoft.EntityFrameworkCore;
using MahadevHWBillingApp.Helper;

namespace MahadevHWBillingApp.Models
{
    public class MahadevHWContext : DbContext
    {
        public string ConnectionString { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<Profile> Profiles { get; set; }
        public DbSet<Purchase> Purchase { get; set; }
        public DbSet<Sale> Sales { get; set; }
        public DbSet<SaleItem> SaleItems { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<RecordPayment> RecordPayments { get; set; }
        public DbSet<Estimate> Estimates { get; set; }
        public DbSet<EstimateItem> EstimateItems { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<PurchaseGSTDetail> PurchaseGSTDetails { get; set; }
        public DbSet<ShortcutKey> ShortcutKeys { get; set; }    
        public DbSet<BillingSetting> BillingSettings { get; set; }
        public DbSet<PurchaseItem> PurchaseItems { get; set; }  

        public MahadevHWContext()
        {
            System.IO.Directory.CreateDirectory("C:\\SqlServerDataBase\\DataBase");
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            ConnectionString = Generic.GetConnectionString();
            if (ConnectionString != null)
                builder.UseSqlite(ConnectionString);
            //builder.UseSqlite(ConnectionString);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<User>();
            builder.Entity<Item>();
            builder.Entity<Profile>();
            builder.Entity<Purchase>().Ignore(e => e.FormatDate);
            builder.Entity<Sale>();
            builder.Entity<Contact>().Ignore(e => e.IsSaveNewCustomer);
            builder.Entity<RecordPayment>();
            builder.Entity<Estimate>();
            builder.Entity<EstimateItem>();
            builder.Entity<PurchaseGSTDetail>();
            builder.Entity<ShortcutKey>();
            builder.Entity<PurchaseItem>();
            base.OnModelCreating(builder);
        }
    }

    public class CoreContext : DbContext
    {
        public static readonly string ConnectionString = Helper.Generic.GetCoreConnectionString();
        public DbSet<User> Users { get; set; }

        public CoreContext()
        {
            System.IO.Directory.CreateDirectory("C:\\SqlServerDataBase\\DataBase");
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            builder.UseSqlite(Generic.GetCoreConnectionString());
            //builder.UseSqlServer(Generic.GetCoreConnectionString());
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<User>();
            base.OnModelCreating(builder);
        }
    }
}