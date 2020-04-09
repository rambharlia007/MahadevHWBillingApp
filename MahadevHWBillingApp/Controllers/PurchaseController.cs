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

        public JsonResult GetDataById(int id)
        {
            var item = Helper.Dapper.GetById<Purchase>(Query.GetPurchaseById(id));
            var gstModelForPurchase = Helper.Dapper.Get<PurchaseGSTDetail>(Query.GetPurchaseGSTDetailsById(id)).ToList();
            var response = Json(new PurchaseModel { Purchase = item, GSTModelData = gstModelForPurchase }, JsonRequestBehavior.AllowGet);
            return response;
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