using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MahadevHWBillingApp.Models;

namespace MahadevHWBillingApp.Controllers
{
    public class BaseController : Controller
    {
        protected static MahadevHWContext _mahadevHwContext;
        protected static Profile _profile;

        public BaseController()
        {
            if (_mahadevHwContext == null)
            {
                _mahadevHwContext = new MahadevHWContext();
                _profile = _mahadevHwContext.Profiles.FirstOrDefault();
            }
        }
    }
}