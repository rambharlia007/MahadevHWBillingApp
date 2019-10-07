using System;
using System.Linq;
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
            try
            {
                _mahadevHwContext = new MahadevHWContext();
                _profile = _mahadevHwContext.Profiles.FirstOrDefault() ?? new Profile()
                {
                    BusinessName = "Demo Business",
                    Owner = "Demo User",
                    MobileNumber = "4242553252",
                    GSTIN = "2552552325",
                    Email = "test@gmail.com",
                    Address = "101A Kr puram",
                    IsEligible = 0
                };

            }
            catch (Exception ex)
            {
                _profile = new Profile()
                {
                    BusinessName = "Demo Business",
                    Owner = "Demo User",
                    MobileNumber = "4242553252",
                    GSTIN = "2552552325",
                    Email = "test@gmail.com",
                    Address = "101A Kr puram",
                    IsEligible = 0,
                };
            }
        }
    }
}