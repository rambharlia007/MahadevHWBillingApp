using MahadevHWBillingApp.Models;
using System;
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
        public JsonResult Save(Bill bill)
        {
            try
            {
                _mahadevHwContext.Sales.Add(bill.SaleDetail);
                _mahadevHwContext.SaveChanges();
                foreach (var saleItem in bill.SaleItems)
                {
                    saleItem.SaleId = bill.SaleDetail.Id;
                    _mahadevHwContext.SaleItems.Add(saleItem);
                }
                _mahadevHwContext.SaveChanges();
                return Json(new { Message = "Save successfull" });
            }
            catch (Exception ex)
            {
                return Json(new { Message = "Internal Server error" });
            }
        }
    }
}