
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

        [ExcelAttribute(ColumnIndex = "C")]
        public string Name { get; set; }

        [ExcelAttribute(ColumnIndex = "D", Format = "#,##0.00", IsTotalRequired = true)]
        public decimal CGST { get; set; }

        [ExcelAttribute(ColumnIndex = "E", Format = "#,##0.00", IsTotalRequired = true)]
        public decimal SGST { get; set; }

        [ExcelAttribute(ColumnIndex = "F", Format = "#,##0.00", IsTotalRequired = true)]
        public decimal Amount { get; set; }

        [SpreadsheetExclude]
        [SpreadsheetTabName(FormatString = "{0:MMMM}")]
        public DateTime Date { get; set; }
        
    }
}