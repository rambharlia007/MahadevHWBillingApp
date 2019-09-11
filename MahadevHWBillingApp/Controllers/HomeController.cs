using Dapper;
using MahadevHWBillingApp.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data.SQLite;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MahadevHWBillingApp.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            //using (var context = new SQLiteConnection(@"Data Source=E:\SqlLiteDB\DB\GSTBilling.db"))
            //{
            //    var data = @"select * from Items";
            //    var x = context.Query<Item>(data);
            //    var xx = Json(x, JsonRequestBehavior.AllowGet);
            //    xx.MaxJsonLength = int.MaxValue;
            //    return xx;
            //}

            using (var x = new MahadevHWContext())
            {
                for (int i = 0; i < 100000; i++)
                {
                    x.Items.Add(new Item()
                    {
                        Amount = 1000,
                        Category = "fkjslk",
                        Name = "sjlkfs",
                        Unit = "500 lts"
                    });
                }

                //x.SaveChanges();
            }

            return View();

        }

        public JsonResult CreateDb()
        {
            using (var x = new MahadevHWContext())
            {
                // create DB
                return Json("Db created", JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}