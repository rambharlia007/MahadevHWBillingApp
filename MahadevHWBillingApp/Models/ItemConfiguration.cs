using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace MahadevHWBillingApp.Models
{
    public class ItemConfiguration: EntityTypeConfiguration<Item>
    {
        public ItemConfiguration()
        {
            this.Property(s => s.Amount).IsRequired();
            this.Property(s => s.CGST).IsRequired();
            this.Property(s => s.SGST).IsRequired();
            this.Property(s => s.Unit).IsRequired();
            this.Property(s => s.Name).IsRequired();




        }
    }
}