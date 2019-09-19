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
        public decimal BasePrice { get; set; }
        public decimal Amount { get; set; }
        public int Quantity { get; set; }
        public decimal SGST { get; set; }
        public decimal CGST { get; set; }
    }
}