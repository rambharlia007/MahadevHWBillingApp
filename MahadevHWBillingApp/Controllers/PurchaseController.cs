using MahadevHWBillingApp.Filters;
using MahadevHWBillingApp.Helper;
using MahadevHWBillingApp.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace MahadevHWBillingApp.Controllers
{
    [CustomSession]
    public class PurchaseController : BaseController
    {
        public ActionResult List()
        {
            if (_adminUser.IsEligible == 0)
                return RedirectToAction("Admin", "Error");
            else if (_adminUser.IsEligible == 1 && _adminUser.IsFreeTrial == 2)
            {
                return RedirectToAction("FreeTrial", "Error");
            }
            return View(_profile);
        }

        public JsonResult GetData(string fromDate, string toDate)
        {
            var items = Helper.Dapper.Get<Purchase>(Query.GetPurchase(fromDate, toDate));
            var footerSum = items.FooterSum();
            return Json(new {data = items, footer = footerSum}, JsonRequestBehavior.AllowGet);
        }

        //public JsonResult GetDataById(int id)
        //{
        //    var item = Helper.Dapper.GetById<Purchase>(Query.GetPurchaseById(id));
        //    var gstModelForPurchase = Helper.Dapper.Get<PurchaseGSTDetail>(Query.GetPurchaseGSTDetailsById(id)).ToList();
        //    var response = Json(new PurchaseModel { Purchase = item, GSTModelData = gstModelForPurchase }, JsonRequestBehavior.AllowGet);
        //    return response;
        //}

        public JsonResult GetDataById(int id)
        {
            var item = Helper.Dapper.GetById<Purchase>(Query.GetPurchaseById(id));
            var purchaseItems = Helper.Dapper.Get<PurchaseItem>(Query.GetPurchaseItemsByPurchaseId(id)).ToList();
            var response = Json(new PurchaseProductModel { Purchase = item, PurchaseItems = purchaseItems }, JsonRequestBehavior.AllowGet);
            return response;
        }


        [HttpPost]
        public JsonResult AddWithProduct(PurchaseProductModel purchaseModel)
        {
            using (var transaction = _mahadevHwContext.Database.BeginTransaction())
            {
                try
                {
                    purchaseModel.Purchase.Date = DateTime.ParseExact(purchaseModel.Purchase.TempDate, "dd-MM-yyyy",
                            CultureInfo.InvariantCulture);
                    _mahadevHwContext.Purchase.Add(purchaseModel.Purchase);
                    _mahadevHwContext.SaveChanges();

                    foreach (var item in purchaseModel.PurchaseItems)
                    {
                        item.PurchaseId = purchaseModel.Purchase.Id;

                        if(item.IsExistingProduct)
                        {
                            var existingItem = _mahadevHwContext.Items.Where(e => e.Id == item.ItemId).FirstOrDefault();
                            existingItem.Quantity += item.Quantity;
                        }
                        else
                        {
                            var newItem = new Item()
                            {
                                Category = item.Category,
                                Name = item.Name,
                                HSN = item.HSN,
                                SellPrice = item.SellPrice,
                                CGST = item.CGST,
                                SGST = item.SGST,
                                Price = item.Price,
                                Discount = item.Discount,
                                DiscountPrice = item.DiscountPrice,
                                MeasuringUnit = item.MeasuringUnit,
                                Quantity = item.Quantity,
                                SoldQuantity = item.SoldQuantity
                            };
                            _mahadevHwContext.Items.Add(newItem);
                            _mahadevHwContext.SaveChanges();
                            item.ItemId = newItem.Id;
                        }
                        _mahadevHwContext.PurchaseItems.Add(item);
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

        [HttpPost]
        public JsonResult Add(List<PurchaseModel> purchaseModel)
        {
            using (var transaction = _mahadevHwContext.Database.BeginTransaction())
            {
                try
                {
                    foreach (var data in purchaseModel)
                    {
                        data.Purchase.Date = DateTime.ParseExact(data.Purchase.TempDate, "dd-MM-yyyy",
                            CultureInfo.InvariantCulture);
                        _mahadevHwContext.Purchase.Add(data.Purchase);
                        _mahadevHwContext.SaveChanges();

                        foreach (var gstModel in data.GSTModelData)
                        {
                            gstModel.PurchaseId = data.Purchase.Id;
                            _mahadevHwContext.PurchaseGSTDetails.Add(gstModel);
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

        [HttpPost]
        public JsonResult Edit(PurchaseModel model)
        {
            model.Purchase.Date = DateTime.ParseExact(model.Purchase.TempDate, "dd-MM-yyyy",
                   CultureInfo.InvariantCulture);
            model.GSTModelData.ForEach((data) =>
            {
                _mahadevHwContext.Entry(data).State = EntityState.Modified;
            });
            _mahadevHwContext.Entry(model.Purchase).State = EntityState.Modified;
            _mahadevHwContext.SaveChanges();
            return Json("Item Edited");
        }

        public JsonResult Remove(int id)
        {
            Helper.Dapper.Execute(Query.DeletePurchase(new List<int> { id }));
            return Json("Item deleted", JsonRequestBehavior.AllowGet);
        }
    }
}