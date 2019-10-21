using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace MahadevHWBillingApp.Models
{
    public class RecordPayment
    {
        public int Id { get; set; }
        public string Invoice { get; set; }
        public string Particulars { get; set; }
        public int CustomerId { get; set; }
        [NotMapped]
        public string FormatDate => Date.ToString("dd-MM-yyyy");
        // Debit, Bill Amount
        public decimal Debit { get; set; }
        // AMount paid for the bill
        public decimal Credit { get; set; }
        [NotMapped]
        public decimal Balance { get; set; }
        [NotMapped]
        public string UIDateFormat { get; set; }
        public DateTime Date { get; set; }
    }
}