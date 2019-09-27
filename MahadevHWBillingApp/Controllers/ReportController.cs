
using EPPlusEnumerable;
using MahadevHWBillingApp.Models;
using OfficeOpenXml;
using System.Linq;
using System.Web.Mvc;

namespace MahadevHWBillingApp.Controllers
{

    public class ReportController : BaseController
    {
        // GET: Report
        public ActionResult Index()
        {
            return View();
        }



        public ActionResult Product()
        {
            var alp = new[] { "A", "B", "C", "D", "E", "F", "G", "H" };
            var p = _mahadevHwContext.Purchase.ToList();
            var excelPackage = Spreadsheet.CreatePackage(p);
            var cs = excelPackage.Workbook.Worksheets.FirstOrDefault();

            var prop = typeof(Purchase).GetProperties();
            var count = p.Count + 2;

            var index = 0;
            foreach (var d in prop)
            {
                var let = alp[index++];
                if (d.PropertyType == typeof(decimal) && !d.Name.Equals("Id"))
                {
                    cs.Cells[$"{let}2:{let}{count}"].Style.Numberformat.Format = "#,##0.00";
                    cs.Cells[$"{let}{count}"].Formula = $"=SUM({let}2:{let}{count-1})";
                }
            }

            cs.Name = "Rename";

            cs.Cells[cs.Dimension.Address].AutoFitColumns();

            cs.Cells[cs.Dimension.Address].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;

            cs.PrinterSettings.RepeatRows = new ExcelAddress("1:1");

            return File(excelPackage.GetAsByteArray(), "application/vnd.ms-excel", "MySpreadsheet.xlsx");
        }
    }
}