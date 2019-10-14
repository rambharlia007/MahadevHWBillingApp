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
            else if (Helper.Dapper.GetCount(Query.GetItemCount) == 0)
            {
                return RedirectToAction("EmptyProduct", "Billing");
            }
            return View(_profile);
        }

        public ActionResult EmptyProduct()
        {
            return View(_profile);
        }

        public JsonResult Save(Bill bill)
        {
            using (var transaction = _mahadevHwContext.Database.BeginTransaction())
            {
                try
                {
                    if (bill.Customer.Id == 0 && bill.Customer.IsSaveNewCustomer)
                    {
                        _mahadevHwContext.Contacts.Add(bill.Customer);
                        _mahadevHwContext.SaveChanges();
                    }

                    bill.SaleDetail.CustomerId = bill.Customer.Id;
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
                    var saleItemIdsOfBillAfterEdit = bill.SaleItems.Select(e => e.Id);
                    var saleItemIdsOfBillBeforeEdit = productQuantityTracks.Select(e => e.SaleItemId);
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
                        // new item added to the bill
                        else
                        {
                            saleItem.SaleId = bill.SaleDetail.Id;
                            productData.Quantity -= saleItem.Quantity;
                            productData.SoldQuantity += saleItem.Quantity;
                            _mahadevHwContext.SaleItems.Add(saleItem);
                        }
                    }

                    var deletedSaleItemIds = saleItemIdsOfBillBeforeEdit.Except(saleItemIdsOfBillAfterEdit);
                    foreach(var id in deletedSaleItemIds)
                    {
                        var deleteSaleItem = _mahadevHwContext.SaleItems.Where(e => e.Id == id).First();
                        var productUpdate = _mahadevHwContext.Items.Where(e => e.Id == deleteSaleItem.ItemId).First();
                        productUpdate.Quantity += deleteSaleItem.Quantity;
                        productUpdate.SoldQuantity -= deleteSaleItem.Quantity;
                        _mahadevHwContext.SaleItems.Remove(deleteSaleItem);
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