using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MahadevHWBillingApp.Models
{
    public class Purchase
    {
        public int Id { get; set; }
        public string BusinessName { get; set; }
        public decimal TotalCGSTAmount { get; set; }
        public decimal TotalSGSTAmount { get; set; }
        public decimal TotalAmount { get; set; }
        public string Invoice { get; set; }
        public string Date { get; set; }
    }
}