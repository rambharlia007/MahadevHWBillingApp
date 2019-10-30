using MahadevHWBillingApp.Helper;
using MahadevHWBillingApp.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
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

        public JsonResult Save(EstimateBill bill)
        {
            using (var transaction = _mahadevHwContext.Database.BeginTransaction())
            {
                try
                {
                    bill.Estimate.Date = DateTime.ParseExact(bill.Estimate.TempDate, "dd-MM-yyyy",
                        CultureInfo.InvariantCulture);
                    _mahadevHwContext.Estimates.Add(bill.Estimate);
                    _mahadevHwContext.SaveChanges();

                    foreach (var estimateItem in bill.EstimateItems)
                    {
                        estimateItem.EstimateId = bill.Estimate.Id;
                        _mahadevHwContext.EstimateItems.Add(estimateItem);
                    }
                    _mahadevHwContext.SaveChanges();
                    transaction.Commit();
                    return Json(new { Message = "Save successfull" });
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    return Json(new { Message = "Internal Server error" });
                }
            }
        }
    }
}