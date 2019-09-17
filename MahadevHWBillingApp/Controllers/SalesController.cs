using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MahadevHWBillingApp.Controllers
{
    public class SalesController : BaseController
    {
        public ActionResult List()
        {
            return View(_profile);
        }
    }
}