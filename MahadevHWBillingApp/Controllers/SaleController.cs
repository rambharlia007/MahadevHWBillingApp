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

        public JsonResult GetData(string fromDate, string toDate)
        {
            var items = Helper.Dapper.Get<Sale>(Query.GetSale(fromDate, toDate));
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
    }
}