using MahadevHWBillingApp.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MahadevHWBillingApp.Controllers
{
    public class EstimateController : BaseController
    {
        public ActionResult New(int id = 0)
        {
            if (_profile.IsEligible == 0)
                return RedirectToAction("Admin", "Error");
            else if (_profile.IsEligible == 1 && _profile.IsFreeTrial == 2)
            {
                return RedirectToAction("FreeTrial", "Error");
            }
            else if (Helper.Dapper.GetCount(Query.GetItemCount) == 0)
            {
                return RedirectToAction("EmptyProduct", "Billing");
            }
            return View(_profile);
        }
    }
}