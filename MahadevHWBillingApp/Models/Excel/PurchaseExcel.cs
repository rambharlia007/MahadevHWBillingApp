
using System;
using EPPlusEnumerable;

namespace MahadevHWBillingApp.Models.Excel
{
    public class PurchaseExcel
    {
        [ExcelAttribute(ColumnIndex = "A", ColumnName = "Date")]
        public string FormatDate => Date.ToString("dd-MM-yyyy");

        [ExcelAttribute(ColumnIndex = "B")]
        public string Invoice { get; set; }

        [ExcelAttribute(ColumnIndex = "C", ColumnName = "Distributor")]
        public string Name { get; set; }

        [ExcelAttribute(ColumnIndex = "D", ColumnName = "Distributor GSTIN")]
        public string DistributorGSTIN { get; set; }

        [ExcelAttribute(ColumnIndex = "E", Format = "#,##0.00", IsTotalRequired = true)]
        public decimal CGST { get; set; }

        [ExcelAttribute(ColumnIndex = "F", Format = "#,##0.00", IsTotalRequired = true)]
        public decimal SGST { get; set; }

        [ExcelAttribute(ColumnIndex = "G", ColumnName = "Sub Amount" ,Format = "#,##0.00", IsTotalRequired = true)]
        public decimal SubAmount { get; set; }

        [ExcelAttribute(ColumnIndex = "H", Format = "#,##0.00", IsTotalRequired = true)]
        public decimal Amount { get; set; }

        [SpreadsheetExclude]
        [SpreadsheetTabName(FormatString = "{0:MMMM}")]
        public DateTime Date { get; set; }
        
    }
}