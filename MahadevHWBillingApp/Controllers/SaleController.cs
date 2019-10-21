using MahadevHWBillingApp.Helper;
using MahadevHWBillingApp.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace MahadevHWBillingApp.Controllers
{
    [HandleError]
    public class SaleController : BaseController
    {
        public ActionResult List()
        {
            if (_profile.IsEligible == 0)
                return RedirectToAction("Admin", "Error");
            else if (_profile.IsEligible == 1 && _profile.IsFreeTrial == 2)
            {
                return RedirectToAction("FreeTrial", "Error");
            }
            return View(_profile);
        }

        public ActionResult RecordPayment()
        {   
            if (_profile.IsEligible == 0)
                return RedirectToAction("Admin", "Error");
            else if (_profile.IsEligible == 1 && _profile.IsFreeTrial == 2)
            {
                return RedirectToAction("FreeTrial", "Error");
            }
            return View(_profile);
        }

        public JsonResult GetData(string fromDate, string toDate, int customerId = 0)
        {
            var items = Helper.Dapper.Get<SaleDto>(Query.GetSale(fromDate, toDate, customerId));
            var footerSum = items.FooterSum();
            return Json(new { data = items, footer = footerSum }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetDataById(int id)
        {
            var result = Helper.Dapper.GetBillDetails(Query.GetSaleAndProducts(id));
            var response = Json(result, JsonRequestBehavior.AllowGet);
            response.MaxJsonLength = int.MaxValue;
            return response;
        }

        public JsonResult GetInvoice()
        {
            var currentInvoice = Helper.Dapper.Get<string>("Select Invoice From Sales order by Id Desc Limit 1");
            if (!currentInvoice.Any())
                return Json("A-1", JsonRequestBehavior.AllowGet);
            var invoice = Helper.Generic.Invoice(currentInvoice.FirstOrDefault());
            return Json(invoice, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetRecordPayment(string fromDate, string toDate, int customerId)
        {
            try
            {
                fromDate = fromDate.ToCustomFormat();
                toDate = toDate.ToCustomFormat();
                var recordPayments = Helper.Dapper.Get<RecordPayment>($@"Select * From RecordPayments Where CustomerId = {customerId}
            And Date >= '{fromDate}' AND Date <= '{toDate}' Order By Date");
                var bills = Helper.Dapper.Get<RecordPaymentSaleDto>($@"Select TotalAmount, Invoice, Date, CustomerId From sales Where CustomerId = {customerId} And Date >= '{fromDate}' AND Date <= '{toDate}' Order By Date");

                if (!bills.Any())
                    return Json(new { data = new List<RecordPayment>(), amount = 0, balance = 0 }, JsonRequestBehavior.AllowGet);

                var results = recordPayments.ToList().CalculateRunningBalance(bills.ToList());
                var billTotalAmount = bills.Sum(e => e.TotalAmount);
                var balanceAmount = billTotalAmount - recordPayments.Sum(e => e.Credit);
                return Json(new { data = results, amount = billTotalAmount, balance = balanceAmount }, JsonRequestBehavior.AllowGet);
            }
            catch (System.Exception ex)
            {
                throw;
            }
        }

        public JsonResult DeleteBillCredit(int id)
        {
            var data = Helper.Dapper.Get<RecordPayment>($"Delete From RecordPayments Where Id = {id}");
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult SaveRecordPayment(RecordPayment data)
        {
            var RecordPayment = new RecordPayment
            {
                Credit = data.Credit,
                Date = data.UIDateFormat.ToCustomDateTimeFormat(),
                CustomerId = data.CustomerId,
                Particulars = data.Particulars
            };
            _mahadevHwContext.RecordPayments.Add(RecordPayment);
            _mahadevHwContext.SaveChanges();
            return Json("Bill credit added succesfully");
        }
    }
}