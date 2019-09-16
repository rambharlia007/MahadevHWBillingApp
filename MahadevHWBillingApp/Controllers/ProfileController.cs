using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MahadevHWBillingApp.Controllers
{
    public class ProfileController : Controller
    {
        // GET: Profile
        public ActionResult New()
        {
            return View();
        }
    }
}