using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MahadevHWBillingApp.Helper;
using MahadevHWBillingApp.Models;
using Microsoft.Ajax.Utilities;

namespace MahadevHWBillingApp.Controllers
{
    //[HandleError]
    public class DashboardController : BaseController
    {
        public ActionResult New()
        {
            try
            {
                if (_adminUser.IsEligible == 0)
                    return RedirectToAction("Admin", "Error");
                else if (_adminUser.IsEligible == 1 && _adminUser.IsFreeTrial == 2)
                {
                    return RedirectToAction("FreeTrial", "Error");
                }
                return View(_profile);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
           
        }

        public JsonResult Get()
        {
            try
            {
                var currentDate = DateTime.Now;
                var subDate = currentDate.AddMonths(-15);
                var startDate = new DateTime(subDate.Year, subDate.Month, 1);
                var endDate = currentDate.Date;
                var sales = _mahadevHwContext.Sales.Where(e => e.Date >= startDate & e.Date <= endDate)
                    .OrderByDescending(e => e.Date).ToList();
                var purchases = _mahadevHwContext.Purchase.Where(e => e.Date >= startDate & e.Date <= endDate)
                    .OrderByDescending(e => e.Date).ToList();

                var saleData = new List<object>();
                foreach (var grouping in sales.GroupBy(e => new {e.Date.Month, e.Date.Year}))
                {
                    var date = grouping.FirstOrDefault().Date;
                    var name = $"{date:MMMM} {date.Year}";
                    var total = grouping.Select(e => e).FooterSum();
                    saleData.Add(new {name, data = total});
                }

                var purchaseData = new List<object>();
                foreach (var grouping in purchases.GroupBy(e => new {e.Date.Month, e.Date.Year}))
                {
                    var date = grouping.FirstOrDefault().Date;
                    var name = $"{date:MMMM} {date.Year}";
                    var total = grouping.Select(e => e).FooterSum();
                    purchaseData.Add(new {name, data = total});
                }

                saleData.Add(new {name = "Total", data = sales.FooterSum()});
                purchaseData.Add(new {name = "Total", data = purchases.FooterSum()});

                return Json(new {saleData, purchaseData}, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json("Internal server error");
            }
        }
    }
}