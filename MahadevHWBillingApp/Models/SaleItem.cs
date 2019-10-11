using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MahadevHWBillingApp.Models
{
    public class SaleItem
    {
        public int Id { get; set; }
        public int ItemId { get; set; }
        public int SaleId { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public decimal TotalAmount { get; set; }
        public int Quantity { get; set; }
        public decimal SGST { get; set; }
        public decimal CGST { get; set; }
        public decimal TotalCGSTAmount { get; set; }
        public decimal TotalSGSTAmount { get; set; }
        public decimal Discount { get; set; }
        public decimal TotalDiscount { get; set; }
        public string HSN { get; set; }
        public string MeasuringUnit { get; set; }
    }
}