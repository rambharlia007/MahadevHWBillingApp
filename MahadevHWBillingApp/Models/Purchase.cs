using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using EPPlusEnumerable;
//using Microsoft.Extensions.Logging.Internal;

namespace MahadevHWBillingApp.Models
{
    public class Purchase
    {
        [SpreadsheetExclude]
        public int Id { get; set; }
        public string DistributorName { get; set; }
        public string DistributorGSTIN { get; set; }
        public decimal TotalCGSTAmount { get; set; }
        public decimal TotalSGSTAmount { get; set; }
        public decimal SubAmount { get; set; }
        public decimal TotalAmount { get; set; }
        public string Invoice { get; set; }
        public string TempDate { get; set; }
        [SpreadsheetExclude]
        public DateTime Date { get; set; }

        public string FormatDate => Date.ToString("dd-MM-yyyy");
    }

    public class PurchaseModel 
    {
        public Purchase Purchase { get; set; }
        public List<PurchaseGSTDetail> GSTModelData { get; set; }
    }
}