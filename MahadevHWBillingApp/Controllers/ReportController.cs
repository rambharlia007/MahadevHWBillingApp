﻿
using System;
using System.Collections.Generic;
using System.Globalization;
using EPPlusEnumerable;
using MahadevHWBillingApp.Models;
using OfficeOpenXml;
using System.Linq;
using System.Web.Mvc;
using MahadevHWBillingApp.Models.Excel;
using OfficeOpenXml.Table;
using WebGrease.Css.Extensions;

namespace MahadevHWBillingApp.Controllers
{

    public class ReportController : BaseController
    {
        // GET: Report
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Sale(string fromDate, string toDate)
        {
            var months = new List<string>();
            var data = Helper.Dapper.Get<SaleExcel>(
                Helper.Query.GetSaleExcelDownloadQuery(fromDate, toDate));

            IList<IList<SaleExcel>> finalData = new List<IList<SaleExcel>>();
            data.GroupBy(e => e.Date.Date.ToString("MMM", CultureInfo.InvariantCulture)).ForEach((x) =>
            {
                months.Add(x.Key);
                finalData.Add(x.Select(g => g).ToList());
            });

            var excelPackage = Spreadsheet.CreatePackage(finalData);

            var properties = typeof(SaleExcel).GetProperties();

            var index = 0;

            foreach (var sheet in excelPackage.Workbook.Worksheets)
            {
                var currentRow = sheet.Dimension.End.Row + 1;
                sheet.Tables[0].TableStyle = TableStyles.None;
                sheet.Name = months[index++];
                sheet.View.FreezePanes(1, sheet.Dimension.End.Column);

                foreach (var property in properties)
                {
                    var attr = (ExcelAttribute[]) property.GetCustomAttributes(typeof(ExcelAttribute), true);

                    if (attr.Any())
                    {
                        var columnIndex = attr[0].ColumnIndex;
                        if (attr[0].IsTotalRequired)
                        {
                            sheet.Cells[$"{columnIndex}2:{columnIndex}{currentRow}"].Style.Numberformat.Format =
                                attr[0].Format;
                            sheet.Cells[$"{columnIndex}{currentRow}"].Formula =
                                $"=SUM({columnIndex}2:{columnIndex}{currentRow - 1})";
                        }

                        if (!string.IsNullOrEmpty(attr[0].ColumnName))
                        {
                            sheet.Cells[$"{columnIndex}1"].Value = attr[0].ColumnName;
                        }
                    }
                }

                sheet.Cells[sheet.Dimension.Address].AutoFitColumns();

                sheet.Cells[sheet.Dimension.Address].Style.HorizontalAlignment =
                    OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;

                sheet.PrinterSettings.RepeatRows = new ExcelAddress("1:1");
            }

            return File(excelPackage.GetAsByteArray(), "application/vnd.ms-excel",
                $"Sale_{DateTime.Now.Date.Year}_{months.First()}_{months.Last()}.xlsx");
        }

        public ActionResult Purchase(string fromDate, string toDate)
        {
            var months = new List<string>();
            var data = Helper.Dapper.Get<PurchaseExcel>(
                Helper.Query.GetPurchaseExcelDownload(fromDate, toDate));

            IList<IList<PurchaseExcel>> finalData = new List<IList<PurchaseExcel>>();
            data.GroupBy(e => e.Date.Date.ToString("MMM", CultureInfo.InvariantCulture)).ForEach((x) =>
            {
                months.Add(x.Key);
                finalData.Add(x.Select(g => g).ToList());
            });

            var excelPackage = Spreadsheet.CreatePackage(finalData);

            var properties = typeof(PurchaseExcel).GetProperties();

            var index = 0;

            foreach (var sheet in excelPackage.Workbook.Worksheets)
            {
                var currentRow = sheet.Dimension.End.Row + 1;
                sheet.Tables[0].TableStyle = TableStyles.None;
                sheet.Name = months[index++];
                sheet.View.FreezePanes(1, sheet.Dimension.End.Column);

                foreach (var property in properties)
                {
                    var attr = (ExcelAttribute[]) property.GetCustomAttributes(typeof(ExcelAttribute), true);

                    if (attr.Any())
                    {
                        var columnIndex = attr[0].ColumnIndex;
                        if (attr[0].IsTotalRequired)
                        {
                            sheet.Cells[$"{columnIndex}2:{columnIndex}{currentRow}"].Style.Numberformat.Format =
                                attr[0].Format;
                            sheet.Cells[$"{columnIndex}{currentRow}"].Formula =
                                $"=SUM({columnIndex}2:{columnIndex}{currentRow - 1})";
                        }

                        if (!string.IsNullOrEmpty(attr[0].ColumnName))
                        {
                            sheet.Cells[$"{columnIndex}1"].Value = attr[0].ColumnName;
                        }
                    }
                }

                sheet.Cells[sheet.Dimension.Address].AutoFitColumns();

                sheet.Cells[sheet.Dimension.Address].Style.HorizontalAlignment =
                    OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;

                sheet.PrinterSettings.RepeatRows = new ExcelAddress("1:1");
            }

            return File(excelPackage.GetAsByteArray(), "application/vnd.ms-excel", $"Purchase_{DateTime.Now.Date.Year}_{months.First()}_{months.Last()}.xlsx");
        }

    }
}