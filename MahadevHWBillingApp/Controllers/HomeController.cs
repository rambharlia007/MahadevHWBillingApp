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
            //    var data = @"delete from Items";
            //    context.Execute(data);
            //}

            using (var x = new MahadevHWContext())
            {
                
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