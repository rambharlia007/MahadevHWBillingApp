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
    public class HomeController : BaseController
    {
        public ActionResult Index()
        {
            return View(_profile);
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