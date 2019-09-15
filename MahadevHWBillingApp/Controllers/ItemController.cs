using MahadevHWBillingApp.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Web.Mvc;
using MahadevHWBillingApp.Helper;

namespace MahadevHWBillingApp.Controllers
{
    public class ItemController : Controller
    {
        private MahadevHWContext _mahadevHwContext;

        public ItemController()
        {
            _mahadevHwContext = new MahadevHWContext();
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult New()
        {
            return View();
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

        public JsonResult GetDataById(int id)
        {
            var items = Helper.Dapper.GetById<Item>(Query.GetItemById(id));
            var response = Json(items, JsonRequestBehavior.AllowGet);
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
        public JsonResult EditItem(Item item)
        {
            _mahadevHwContext.Entry(item).State = EntityState.Modified;
            _mahadevHwContext.SaveChanges();
            return Json("Item Edited");
        }

        public JsonResult RemoveItem(int id)
        {
            Helper.Dapper.Execute(Query.DeleteItem(new List<int> {id}));
            return Json("Item deleted", JsonRequestBehavior.AllowGet);
        }
    }
}