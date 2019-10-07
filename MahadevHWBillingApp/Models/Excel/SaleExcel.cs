using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EPPlusEnumerable;

namespace MahadevHWBillingApp.Models.Excel
{
    public class SaleExcel
    {
        [ExcelAttribute(ColumnIndex = "A", ColumnName = "Date")]
        public string FormatDate => Date.ToString("dd-MM-yyyy");

        [ExcelAttribute(ColumnIndex = "B")] public string Invoice { get; set; }

        [ExcelAttribute(ColumnIndex = "C", ColumnName = "Customer")]
        public string CustomerName { get; set; }

        [ExcelAttribute(ColumnIndex = "D", ColumnName = "Customer GSTIN")]
        public string CustomerGSTIN { get; set; }

        [ExcelAttribute(ColumnIndex = "E", Format = "#,##0.00", ColumnName = "5%", IsTotalRequired = true)]
        public decimal Tax1 { get; set; }

        [ExcelAttribute(ColumnIndex = "F", Format = "#,##0.00", ColumnName = "9%", IsTotalRequired = true)]
        public decimal Tax2 { get; set; }

        [ExcelAttribute(ColumnIndex = "G", Format = "#,##0.00", ColumnName = "15%", IsTotalRequired = true)]
        public decimal Tax3 { get; set; }

        [ExcelAttribute(ColumnIndex = "H", Format = "#,##0.00", ColumnName = "18%", IsTotalRequired = true)]
        public decimal Tax4 { get; set; }
        [ExcelAttribute(ColumnIndex = "I", Format = "#,##0.00", ColumnName = "Sub Total", IsTotalRequired = true)]
        public decimal SubTotal { get; set; }

        [SpreadsheetExclude]
        [SpreadsheetTabName(FormatString = "{0:MMMM}")]
        public DateTime Date { get; set; }
    }
}