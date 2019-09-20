using MahadevHWBillingApp.Helper;
using MahadevHWBillingApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MahadevHWBillingApp.Controllers
{
    public class SaleController : BaseController
    {
        public ActionResult List()
        {
            return View(_profile);
        }
        public JsonResult GetData()
        {
            var items = Helper.Dapper.Get<Sale>(Query.GetSale);
            var response = Json(items, JsonRequestBehavior.AllowGet);
            response.MaxJsonLength = int.MaxValue;
            return response;
        }
        public JsonResult GetDataById(int id)
        {
            var gridReader = Helper.Dapper.QueryMultiple(Query.GetSaleAndProducts(id));
            var result = new Bill
            {
                SaleDetail = gridReader.ReadSingle<Sale>(),
                SaleItems = gridReader.Read<SaleItem>().ToList()
            };
            var response = Json(result, JsonRequestBehavior.AllowGet);
            response.MaxJsonLength = int.MaxValue;
            return response;
        }
    }
}