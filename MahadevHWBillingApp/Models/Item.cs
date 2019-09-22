using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MahadevHWBillingApp.Models
{
    public class Item
    {
        public int Id { get; set; }
        public int Price { get; set; }
        public string Name { get; set; }
        public string MeasuringUnit { get; set; }
        public string Category { get; set; }
        public int SGST { get; set; }
        public int CGST { get; set; }
        public int Quantity { get; set; }
        public int Discount { get; set; }
        public decimal DiscountPrice { get; set; }
    }
}