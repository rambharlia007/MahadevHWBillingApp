using MahadevHWBillingApp.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MahadevHWBillingApp.Helper;

namespace MahadevHWBillingApp.Controllers
{
    [HandleError]
    public class BillingController : BaseController
    {
        // GET: Billing
        public ActionResult New(int id = 0)
        {
            if (_profile.IsEligible == 0)
                return RedirectToAction("Admin", "Error");
            else if (_profile.IsEligible == 1 && _profile.IsFreeTrial == 2)
            {
                return RedirectToAction("FreeTrial", "Error");
            }
            return View(_profile);
        }

        public JsonResult Save(Bill bill)
        {
            using (var transaction = _mahadevHwContext.Database.BeginTransaction())
            {
                try
                {
                    bill.SaleDetail.Date = DateTime.ParseExact(bill.SaleDetail.TempDate, "dd-MM-yyyy",
                        CultureInfo.InvariantCulture);
                    _mahadevHwContext.Sales.Add(bill.SaleDetail);
                    _mahadevHwContext.SaveChanges();

                    foreach (var saleItem in bill.SaleItems)
                    {
                        saleItem.SaleId = bill.SaleDetail.Id;
                        _mahadevHwContext.SaleItems.Add(saleItem);

                        var editItem = _mahadevHwContext.Items.SingleOrDefault(e => e.Id == saleItem.ItemId);
                        editItem.Quantity -= saleItem.Quantity;
                        editItem.SoldQuantity += saleItem.Quantity;
                    }

                    _mahadevHwContext.SaveChanges();

                    transaction.Commit();
                    return Json(new { Message = "Save successfull" });

                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    return Json(new { Message = "Internal Server error" });
                }
            }
        }
        public JsonResult Edit(Bill bill)
        {
            using (var transaction = _mahadevHwContext.Database.BeginTransaction())
            {
                try
                {
                    bill.SaleDetail.Date = DateTime.ParseExact(bill.SaleDetail.TempDate, "dd-MM-yyyy",
                        CultureInfo.InvariantCulture);
                    var productQuantityTracks = Helper.Dapper.Get<ProductQuantityTrack>(Query.GetProductsByBill(bill.SaleDetail.Id));
                    var trackProductIds = productQuantityTracks.Select(e => e.ItemId);
                    _mahadevHwContext.Entry(bill.SaleDetail).State = EntityState.Modified;
                    foreach (var saleItem in bill.SaleItems)
                    {
                        var productData = _mahadevHwContext.Items.First(e => e.Id == saleItem.ItemId);
                        if (trackProductIds.Contains(saleItem.ItemId))
                        {
                            var previousAddedQuantity = productQuantityTracks.First(e => e.ItemId == saleItem.ItemId).Quantity;
                            var updateQuantityCount = saleItem.Quantity - previousAddedQuantity;
                            productData.Quantity = productData.Quantity - updateQuantityCount;
                            productData.SoldQuantity += updateQuantityCount;
                            _mahadevHwContext.Entry(saleItem).State = EntityState.Modified;
                        }
                        else
                        {
                            saleItem.SaleId = bill.SaleDetail.Id;
                            productData.Quantity -= saleItem.Quantity;
                            productData.SoldQuantity += saleItem.Quantity;
                            _mahadevHwContext.SaleItems.Add(saleItem);
                        }
                    }

                    _mahadevHwContext.SaveChanges();
                    transaction.Commit();
                    return Json(new { Message = "Save successfull" });
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    return Json(new { Message = "Internal Server error" });
                }
            }
        }
    }
}