using System;
using System.Web.Mvc;

namespace MahadevHWBillingApp.Controllers
{
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