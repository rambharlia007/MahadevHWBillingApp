using MahadevHWBillingApp.Models;
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace MahadevHWBillingApp.Controllers
{
    public class BillingController : BaseController
    {
        // GET: Billing
        public ActionResult New(int id = 0)
        {
            return View(_profile);
        }

        public JsonResult Save(Bill bill)
        {
            using (var transaction = _mahadevHwContext.Database.BeginTransaction())
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

                    transaction.Commit();
                    return Json(new {Message = "Save successfull"});

                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    return Json(new {Message = "Internal Server error"});
                }
            }
        }
    }
}