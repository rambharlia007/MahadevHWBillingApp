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
        public decimal Roundoff { get; set; }   
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

    public class PurchaseItem 
    {
        public int Id { get; set; }
        public decimal Price { get; set; }
        public string Name { get; set; }
        public string MeasuringUnit { get; set; }
        public string Category { get; set; }
        public int SGST { get; set; }
        public int CGST { get; set; }
        public int Quantity { get; set; }
        public int Discount { get; set; }
        public decimal DiscountPrice { get; set; }
        public decimal SellPrice { get; set; }
        public string HSN { get; set; }
        public int SoldQuantity { get; set; }
        public int IsDelete { get; set; }
        public int ItemId { get; set; }
        public int PurchaseId { get; set; }
        public bool IsExistingProduct { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal TotalTax { get; set; }
    }

    public class PurchaseProductModel
    {
        public Purchase Purchase { get; set; }
        public IEnumerable<PurchaseItem> PurchaseItems { get; set; }
    }
}       