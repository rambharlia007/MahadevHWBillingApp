using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Extensions.Logging.Internal;

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
        public DateTime Date { get; set; }

        public string FormatDate => Date.ToString("dd-MM-yyyy");
    }
}