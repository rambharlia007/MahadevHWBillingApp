﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MahadevHWBillingApp.Controllers
{
    public class BillingController : BaseController
    {
        // GET: Billing
        public ActionResult New()
        {
            return View(_profile);
        }
    }
}