using MahadevHWBillingApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace MahadevHWBillingApp.Controllers
{
    [HandleError]
    public class DataController : BaseController
    {

        // GET: Data
        public JsonResult Purchase()
        {
            try
            {
                var data = new List<Purchase>();
                var gen = new Random();
                for (int i = 0; i < 18; i++)
                {
                    var monthStartDate = DateTime.Now.AddMonths(-i).Date;

                    for (int j = 0; j < 30; j++)
                    {
                        var date = monthStartDate.AddDays(-j).Date;
                        for (int k = 0; k < 2; k++)
                        {
                            var am = gen.Next(5000);
                            decimal tax = (decimal)(am * 0.09);
                            data.Add(new Purchase()
                            {
                                Date = date,
                                DistributorName = $"Busn-{i}{j}{k}",
                                DistributorGSTIN = "DIS" + date.ToString("yyyyMMddHHmmss") + k.ToString(),
                                Invoice = date.ToString("yyyyMMddHHmmss") + k.ToString(),
                                TotalAmount = am + 2 * tax,
                                TotalCGSTAmount = tax,
                                TotalSGSTAmount = tax,
                                SubAmount = am
                            });
                        }
                    }
                }
                _mahadevHwContext.Purchase.AddRange(data);
                _mahadevHwContext.SaveChanges();
                return Json("Done", JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(ex.ToString(), JsonRequestBehavior.AllowGet);
            }
        
        }
        public JsonResult Sales()
        {
            using (var transaction = _mahadevHwContext.Database.BeginTransaction())
            {
                try
                {
                    int index = 1;
                    var data = new List<Sale>();
                    for (int i = 0; i < 18; i++)
                    {
                        var monthStartDate = DateTime.Now.AddMonths(-i).Date;

                        for (int j = 0; j < 30; j++)
                        {
                            var date = monthStartDate.AddDays(-j).Date;
                            for (int k = 0; k < 1; k++)
                            {
                                var gst = new[] {5, 9, 15, 18};
                                var gen = new Random();
                                var products = new List<SaleItem>();

                                for (int m = 0; m < 5; m++)
                                {
                                    var price = gen.Next(5000, 15000);
                                    var taxper = gst[gen.Next(0, 3)];
                                    products.Add(new SaleItem()
                                    {
                                        ItemId = m + 1,
                                        Name = $"item-{m + 1}",
                                        Quantity = 10,
                                        Price = price,
                                        TotalAmount = price * 10,
                                        CGST = taxper,
                                        TotalCGSTAmount = (taxper * price * 10) / 100,
                                        SGST = taxper,
                                        TotalSGSTAmount = (taxper * price * 10) / 100
                                    });
                                }

                                var ta = products.Sum(e => e.TotalAmount);
                                var tta = products.Sum(e => e.TotalCGSTAmount);

                                var saleDetails = new Sale()
                                {
                                    Date = date,
                                    BusinessName = $"B-{i}-{j}-{k}",
                                    Invoice = $"A-{index++}",
                                    TotalAmount = ta + tta + tta,
                                    TotalCGSTAmount = tta,
                                    TotalSGSTAmount = tta,
                                    //CustomerGSTIN = "AK123" + date.ToString("yyyyMMddHHmmss") + k.ToString(),
                                    //CustomerName = "RGDJSJ" + k.ToString(),
                                    SubTotal = ta
                                };
                                _mahadevHwContext.Sales.Add(saleDetails);
                                _mahadevHwContext.SaveChanges();

                                foreach (var saleItem in products)
                                {
                                    saleItem.SaleId = saleDetails.Id;
                                    _mahadevHwContext.SaleItems.Add(saleItem);
                                }

                                _mahadevHwContext.SaveChanges();

                            }
                        }
                    }

                    transaction.Commit();
                    return Json("Done", JsonRequestBehavior.AllowGet);
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    Response.StatusCode = (int) HttpStatusCode.InternalServerError;
                    return Json("Error", JsonRequestBehavior.AllowGet);
                }
            }
        }

        public JsonResult GetNext(string input) {
            return Json(Helper.Generic.Invoice(input), JsonRequestBehavior.AllowGet);
        }

        public JsonResult Product()
        {
            try
            {
                var taxPer = new int[] { 5, 9, 15, 18 };
                var data = new List<Item>();
                var gen = new Random();
                for (int i = 1; i <= 1000; i++)
                {
                    var t = taxPer[gen.Next(4)];
                    var p = (decimal) gen.Next(5000);
                    var d =  gen.Next(40);
                    var dp = p - (decimal)(d * p) / 100;
                    data.Add(new Item
                    {
                        CGST = t,
                        SGST = t,
                        Price = p,
                        Discount = d,
                        DiscountPrice = dp,
                        Quantity = gen.Next(100, 1000),
                        Name = $"Pro-{i}",
                        MeasuringUnit = "Unit",
                        HSN = $"HSN-{i}"
                    });
                }
                _mahadevHwContext.Items.AddRange(data);
                _mahadevHwContext.SaveChanges();
                return Json("Done", JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(ex.ToString(), JsonRequestBehavior.AllowGet);
            }

        }
    }
}