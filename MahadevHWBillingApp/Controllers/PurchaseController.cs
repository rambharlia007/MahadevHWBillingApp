using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MahadevHWBillingApp.Controllers
{
    public class PurchaseController : BaseController
    {
        public ActionResult List()
        {
            return View(_profile);
        }
    }
}