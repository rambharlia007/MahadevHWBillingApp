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

        }
    }
}