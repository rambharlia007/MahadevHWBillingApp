using MahadevHWBillingApp.Helper;
using MahadevHWBillingApp.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MahadevHWBillingApp.Controllers
{
    [HandleError]
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
            var items = Helper.Dapper.GetById<Purchase>(Query.GetPurchaseById(id));
            var response = Json(items, JsonRequestBehavior.AllowGet);
            return response;
        }
        [HttpPost]
        public JsonResult Add(List<Purchase> purchases)
        {
            foreach (var purchase in purchases)
            {
                purchase.Date = DateTime.ParseExact(purchase.TempDate, "dd-MM-yyyy",
                    CultureInfo.InvariantCulture);
                _mahadevHwContext.Purchase.Add(purchase);
            }

            _mahadevHwContext.SaveChanges();
            return Json("Item created");
        }

        [HttpPost]
        public JsonResult Edit(Purchase purchase)
        {
            purchase.Date = DateTime.ParseExact(purchase.TempDate, "dd-MM-yyyy",
                   CultureInfo.InvariantCulture);
            _mahadevHwContext.Entry(purchase).State = EntityState.Modified;
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