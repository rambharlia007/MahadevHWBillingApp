using MahadevHWBillingApp.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using MahadevHWBillingApp.Helper;
using WebGrease.Css.Extensions;
using MahadevHWBillingApp.Filters;

namespace MahadevHWBillingApp.Controllers
{
    [CustomSession]
    public class ItemController : BaseController
    {
        public ActionResult Index()
        {
            if (_adminUser.IsEligible == 0)
                return RedirectToAction("Admin", "Error");
            else if (_adminUser.IsEligible == 1 && _adminUser.IsFreeTrial == 2)
            {
                return RedirectToAction("FreeTrial", "Error");
            }
            return View(_profile);
        }

        public ActionResult New()
        {
            if (_mahadevHwContext is null)
                return RedirectToAction("InternalServerError", "Error");
            return View(_profile);
        }

        public JsonResult GetData()
        {
            var items = Helper.Dapper.Get<Item>(Query.GetItem);
            var response = Json(items, JsonRequestBehavior.AllowGet);
            response.MaxJsonLength = int.MaxValue;
            return response;
        }

        public JsonResult GetDataBySearch(string q)
        {
            var items = Helper.Dapper.Get<Item>(Query.GetItemBySearch(q));
            var response = Json(items, JsonRequestBehavior.AllowGet);
            response.MaxJsonLength = int.MaxValue;
            return response;
        }

        public JsonResult GetCount(string q)
        {
            var count = Helper.Dapper.GetCount(Query.GetItemCount);
            var response = Json(count, JsonRequestBehavior.AllowGet);
            return response;
        }

        public JsonResult GetDataById(int id)
        {
            var keys = Helper.Dapper.GetById<Item>(Query.GetItemById(id));
            var response = Json(keys, JsonRequestBehavior.AllowGet);
            return response;
        }

        [HttpPost]
        public JsonResult AddItems(List<Item> items)
        {
            foreach (var item in items)
            {
                _mahadevHwContext.Items.Add(item);
            }
            _mahadevHwContext.SaveChanges();
            return Json("Item created");
        }

        [HttpPost]
        public JsonResult EditItems(List<Item> items)
        {
            foreach (var item in items)
            {
                _mahadevHwContext.Entry(item).State = EntityState.Modified;
            }
            _mahadevHwContext.SaveChanges();
            return Json("Item Edited");
        }

        [HttpPost]
        public JsonResult EditItem(Item item)
        {
            _mahadevHwContext.Entry(item).State = EntityState.Modified;
            _mahadevHwContext.SaveChanges();
            return Json("Item Edited");
        }

        public JsonResult RemoveItem(int id)
        {
            var item =_mahadevHwContext.Items.ToList().Where(e => e.Id == id).First();
            item.IsDelete = 1;
            _mahadevHwContext.SaveChanges();
            return Json("Item deleted", JsonRequestBehavior.AllowGet);
        }
    }
}