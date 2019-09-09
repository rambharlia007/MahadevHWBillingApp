using Dapper;
using MahadevHWBillingApp.Models;
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
        public ActionResult Index()
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
    }
}