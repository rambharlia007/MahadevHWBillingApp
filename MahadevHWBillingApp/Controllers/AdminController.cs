using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MahadevHWBillingApp.Models;

namespace MahadevHWBillingApp.Controllers
{
    public class AdminController : BaseController
    {
        private string apiKey = "hdst58Mb2orw9J+oeJpifxNEu92maDyhfLYhzepxgoc=";
        public JsonResult Grant(string key)
        {
            var validKey = EncryptDecryptData.Encrypt(key);
            if(!validKey.Equals(apiKey))
                return Json("Invalid key", JsonRequestBehavior.AllowGet);

            var profile = new Profile()
            {
                BusinessName = "Demo Business",
                Owner = "Demo User",
                MobileNumber = "4242553252",
                GSTIN = "2552552325",
                Email = "test@gmail.com",
                Address = "101A Kr puram",
                IsEligible = 1,
                Key = EncryptDecryptData.Encrypt(DateTime.Now.Date.AddDays(90).ToString("dd-MM-yyyy"))
            };

            var profiles = _mahadevHwContext.Profiles;
            if (profiles.Any())
            {
                var dbProfile = profiles.First();
                dbProfile.IsEligible = 1;
                dbProfile.Key = EncryptDecryptData.Encrypt(DateTime.Now.Date.AddDays(90).ToString("dd-MM-yyyy"));
                _profile = profile;
                _mahadevHwContext.SaveChanges();
            }
            else
            {
                _mahadevHwContext.Profiles.Add(profile);
                _profile = profile;
            }
            _mahadevHwContext.SaveChanges();
            return Json("Granted", JsonRequestBehavior.AllowGet);
        }

        public JsonResult Revoke(string key)
        {
            var validKey = EncryptDecryptData.Encrypt(key);
            if (!validKey.Equals(apiKey)) return Json("Invalid key", JsonRequestBehavior.AllowGet);
            var profile = _mahadevHwContext.Profiles.First();
            profile.IsEligible = 0;
            _profile = profile;
            _mahadevHwContext.SaveChanges();
            return Json("Revoked", JsonRequestBehavior.AllowGet);
        }
    }
}