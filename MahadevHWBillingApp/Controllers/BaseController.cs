using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MahadevHWBillingApp.Models;

namespace MahadevHWBillingApp.Controllers
{
    public class BaseController : Controller
    {
        protected MahadevHWContext _mahadevHwContext;
        protected static Profile _profile;

        public BaseController()
        {
                _mahadevHwContext = new MahadevHWContext();
                if (_profile != null) return;
                _profile = _mahadevHwContext.Profiles.FirstOrDefault() ?? new Profile()
                {
                    BusinessName = "Demo Business",
                    Owner = "Demo User",
                    MobileNumber = "4242553252",
                    GSTIN = "2552552325",
                    Email = "test@gmail.com",
                    Address = "101A Kr puram"
                };
        }
    }
}