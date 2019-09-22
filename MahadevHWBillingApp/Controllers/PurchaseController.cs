using MahadevHWBillingApp.Helper;
using MahadevHWBillingApp.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MahadevHWBillingApp.Controllers
{
    public class PurchaseController : BaseController
    {
        public ActionResult List()
        {
            return View(_profile);
        }

        public JsonResult GetData(string fromDate = null, string toDate = null)
        {
            var items = Helper.Dapper.Get<Purchase>(Query.GetPurchase(fromDate, toDate));
            var footerSum = new {CGST = 9000, SGST = 9000, TotalAmount = 15000};
            return Json(new {data=items, footer = footerSum}, JsonRequestBehavior.AllowGet);
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
                _mahadevHwContext.Purchase.Add(purchase);
            }
            _mahadevHwContext.SaveChanges();
            return Json("Item created");
        }

        [HttpPost]
        public JsonResult Edit(Purchase purchase)
        {
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