using System;

namespace MahadevHWBillingApp.Models
{
    public class Estimate
    {
        public int Id { get; set; }
        public int Name { get; set; }
        public decimal TotalAmount { get; set; }
        public string Invoice { get; set; }
        public string TempDate { get; set; }
        public DateTime Date { get; set; }
        public string FormatDate => Date.ToString("dd-MM-yyyy");
    }
}