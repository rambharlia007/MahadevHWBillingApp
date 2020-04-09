using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MahadevHWBillingApp.Models.Excel
{
    public class ProductExcel
    {
        [ExcelAttribute(ColumnIndex = "A")]
        public string Name { get; set; }

        [ExcelAttribute(ColumnIndex = "B", ColumnName = "HSN/SAC")]
        public string HSN { get; set; }

        [ExcelAttribute(ColumnIndex = "C", ColumnName = "PER")]
        public string MeasuringUnit { get; set; }

        [ExcelAttribute(ColumnIndex = "D", ColumnName = "Quantity available")]
        public string Quantity { get; set; }

        [ExcelAttribute(ColumnIndex = "E", ColumnName = "Quantity sold")]
        public string SoldQuantity { get; set; }

        [ExcelAttribute(ColumnIndex = "F", ColumnName = "CGST")]
        public int CGST { get; set; }

        [ExcelAttribute(ColumnIndex = "G", ColumnName = "SGST")]
        public int SGST { get; set; }

        [ExcelAttribute(ColumnIndex = "H", ColumnName = "Discount (%)")]
        public int Discount { get; set; }

        [ExcelAttribute(ColumnIndex = "I", ColumnName = "List Price")]
        public decimal Price { get; set; }

        [ExcelAttribute(ColumnIndex = "J", ColumnName = "Discount Price")]
        public decimal DiscountPrice { get; set; }
    }
}