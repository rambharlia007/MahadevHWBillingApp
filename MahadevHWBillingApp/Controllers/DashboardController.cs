using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MahadevHWBillingApp.Controllers
{
    public class DashboardController : BaseController
    {
        // GET: Dashboard
        public ActionResult New()
        {
            return View(_profile);
        }
    }
}