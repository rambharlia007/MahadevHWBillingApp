using System;

namespace MahadevHWBillingApp.Models
{
    public class Sale
    {
        public int Id { get; set; }
        public string BusinessName { get; set; }
        public string CustomerName { get; set; }
        public string CustomerGSTIN { get; set; }
        public decimal TotalCGSTAmount { get; set; }
        public decimal TotalSGSTAmount { get; set; }
        public decimal SubTotal { get; set; }
        public decimal TotalAmount { get; set; }
        public string Invoice { get; set; }
        public DateTime Date { get; set; }
        public string FormatDate => Date.ToString("dd-MM-yyyy");
    }
}