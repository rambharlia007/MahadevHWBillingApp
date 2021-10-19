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
using MahadevHWBillingApp.Filters;

namespace MahadevHWBillingApp.Controllers
{
    [HandleError]
    [CustomSession]
    public class BillingController : BaseController
    {
        // GET: Billing
        public ActionResult New(int id = 0)
        {
            if (Helper.Dapper.GetCount(Query.GetItemCount) == 0)
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
                    //saved from ajax call directly and id is passed
                    if (bill.Customer.Id == 0 && bill.Customer.GSTIN != null)
                    {
                        var currentCustomer = _mahadevHwContext.Contacts.Where(e => e.GSTIN.ToLower() == bill.Customer.GSTIN.ToLower()).FirstOrDefault();
                        if (currentCustomer != null)
                        {
                            bill.Customer.Id = currentCustomer.Id;
                            currentCustomer.Name = bill.Customer.Name;
                            currentCustomer.State = bill.Customer.State;
                            currentCustomer.StateCode = bill.Customer.StateCode;
                            currentCustomer.MobileNumber = bill.Customer.MobileNumber;
                            _mahadevHwContext.Entry(currentCustomer).State = EntityState.Modified;
                            _mahadevHwContext.SaveChanges();
                        }
                        else
                        {
                            bill.Customer.Type = "temp";
                            _mahadevHwContext.Contacts.Add(bill.Customer);
                            _mahadevHwContext.SaveChanges();
                        }
                    }

                    bill.SaleDetail.CustomerId = bill.Customer.Id;
                    bill.SaleDetail.Date = DateTime.ParseExact(bill.SaleDetail.TempDate, "dd-MM-yyyy",
                        CultureInfo.InvariantCulture);
                    _mahadevHwContext.Sales.Add(bill.SaleDetail);
                    _mahadevHwContext.SaveChanges();

                    //var isStockCount = _profile.EnableStockCount;
                    foreach (var saleItem in bill.SaleItems)
                    {
                        saleItem.SaleId = bill.SaleDetail.Id;
                        _mahadevHwContext.SaleItems.Add(saleItem);

                        var editItem = _mahadevHwContext.Items.SingleOrDefault(e => e.Id == saleItem.ItemId);
                        if(editItem != null)
                        {
                            editItem.Quantity -= saleItem.Quantity;
                            editItem.SoldQuantity += saleItem.Quantity;
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

        public JsonResult GetBillSettings()
        {
            var result = _mahadevHwContext.BillingSettings.FirstOrDefault();
            var response = Json(result, JsonRequestBehavior.AllowGet);
            return response;
        }

        [HttpPost]
        public JsonResult UpdateBillSettings(BillingSetting billingSetting)
        {
            _mahadevHwContext.Entry(billingSetting).State = EntityState.Modified;
            _mahadevHwContext.SaveChanges();
            return Json("Item Edited");
        }

        public JsonResult Edit(Bill bill)
        {
            using (var transaction = _mahadevHwContext.Database.BeginTransaction())
            {
                try
                {
                    bill.SaleDetail.CustomerId = bill.Customer.Id;
                    bill.SaleDetail.Date = DateTime.ParseExact(bill.SaleDetail.TempDate, "dd-MM-yyyy",
                        CultureInfo.InvariantCulture);
                    var productQuantityTracks = Helper.Dapper.Get<ProductQuantityTrack>(Query.GetProductsByBill(bill.SaleDetail.Id));
                    var trackProductIds = productQuantityTracks.Select(e => e.ItemId);
                    var saleItemIdsOfBillAfterEdit = bill.SaleItems.Select(e => e.Id);
                    var saleItemIdsOfBillBeforeEdit = productQuantityTracks.Select(e => e.SaleItemId);
                    _mahadevHwContext.Entry(bill.SaleDetail).State = EntityState.Modified;
                    foreach (var saleItem in bill.SaleItems)
                    {
                        var productData = _mahadevHwContext.Items.FirstOrDefault(e => e.Id == saleItem.ItemId);
                        if (trackProductIds.Contains(saleItem.ItemId))
                        {
                            var previousAddedQuantity = productQuantityTracks.First(e => e.ItemId == saleItem.ItemId).Quantity;
                            var updateQuantityCount = saleItem.Quantity - previousAddedQuantity;

                            // item not der in DB, but only sale item is edited
                            if(productData != null)
                            {
                                productData.Quantity = productData.Quantity - updateQuantityCount;
                                productData.SoldQuantity += updateQuantityCount;
                            }
                           
                            _mahadevHwContext.Entry(saleItem).State = EntityState.Modified;
                        }
                        // new item added to the bill
                        else
                        {
                            // Sale item exists but not der in products
                            if(saleItem.Id < 1)
                            {
                                saleItem.SaleId = bill.SaleDetail.Id;
                                if (productData != null)
                                {
                                    productData.Quantity -= saleItem.Quantity;
                                    productData.SoldQuantity += saleItem.Quantity;
                                }
                                _mahadevHwContext.SaleItems.Add(saleItem);
                            }
                        }
                    }

                    var deletedSaleItemIds = saleItemIdsOfBillBeforeEdit.Except(saleItemIdsOfBillAfterEdit);
                    foreach(var id in deletedSaleItemIds)
                    {
                        var deleteSaleItem = _mahadevHwContext.SaleItems.ToList().Where(e => e.Id == id).First();
                        var productUpdate = _mahadevHwContext.Items.FirstOrDefault(e => e.Id == deleteSaleItem.ItemId);
                        if(productUpdate != null)
                        {
                            productUpdate.Quantity += deleteSaleItem.Quantity;
                            productUpdate.SoldQuantity -= deleteSaleItem.Quantity;
                        }
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