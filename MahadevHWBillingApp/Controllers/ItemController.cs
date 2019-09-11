using Dapper;
using MahadevHWBillingApp.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Web;
using System.Web.Mvc;

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
            using (var context = new SQLiteConnection(@"Data Source=E:\SqlLiteDB\DB\GSTBilling.db"))
            {
                var data = @"select  * from Items LIMIT 1000";
                var x = context.Query<Item>(data);
                var xx = Json(x, JsonRequestBehavior.AllowGet);
                xx.MaxJsonLength = int.MaxValue;
                return xx;
            }
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
            using (var context = new SQLiteConnection(@"Data Source=E:\SqlLiteDB\DB\GSTBilling.db"))
            {
                var query = $@"Delete from Items Where Id = {id}";
                context.Execute(query);
            }
            return Json("Item deleted", JsonRequestBehavior.AllowGet);
        }
    }
}