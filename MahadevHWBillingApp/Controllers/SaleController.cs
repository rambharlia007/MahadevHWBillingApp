using MahadevHWBillingApp.Helper;
using MahadevHWBillingApp.Models;
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

        public JsonResult GetBillCredit(string fromDate, string toDate, int customerId)
        {
            var billCreditDetails = Helper.Dapper.Get<BillCreditDetailDto>($"Select * From BillCreditDetails Where CustomerId = {customerId}");
            var saleDetails = Helper.Dapper.Get<RecordPaymentSaleDto>($"Select Invoice, FormatDate, TotalAmount Amount From Sales Where CustomerId = {customerId}");
            var data = new { sales = saleDetails, recordPayments = billCreditDetails, sum = saleDetails.Sum(e => e.Amount) };
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public JsonResult DeleteBillCredit(int id)
        {
            var data = Helper.Dapper.Get<BillCreditDetailDto>($"Delete From BillCreditDetails Where Id = {id}");
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult SaveBillCredit(BillCreditDetailDto data)
        {
            var billCredit = new BillCreditDetail
            {
                Amount = data.Amount,
                Date = data.UIDateFormat.ToCustomDateTimeFormat(),
                CustomerId = data.CustomerId
            };
            _mahadevHwContext.BillCreditDetails.Add(billCredit);
            _mahadevHwContext.SaveChanges();
            return Json("Bill credit added succesfully");
        }
    }
}