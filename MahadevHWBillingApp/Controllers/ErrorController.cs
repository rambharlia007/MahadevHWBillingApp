using MahadevHWBillingApp.Filters;
using System;
using System.Web.Mvc;

namespace MahadevHWBillingApp.Controllers
{
    [CustomErrorSession]
    public class ErrorController : BaseController
    {
        public ActionResult PageNotFound()
        {
            return View(_profile);
        }

        public ActionResult InternalServerError()
        {
            return View(_profile);
        }
        public ActionResult Admin()
        {
            return View(_profile);
        }

        public ActionResult FreeTrial()
        {
            return View(_profile);
        }
    }
}