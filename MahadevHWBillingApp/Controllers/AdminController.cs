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
        public ActionResult New()
        {
            return View(_profile);
        }
        public JsonResult Grant(Permission permission)
        {
            var computerName = System.Net.Dns.GetHostName();
            var validKey = EncryptDecryptData.Encrypt(permission.Key);

            if (!validKey.Equals(apiKey))
                return Json("Invalid key", JsonRequestBehavior.AllowGet);

            var profile = new Profile()
            {
                BusinessName = "Demo Business",
                Owner = "Demo User",
                IsEligible = 1,
                Key = EncryptDecryptData.Encrypt(DateTime.Now.Date.AddDays(90).ToString("dd-MM-yyyy")),
                K1 = EncryptDecryptData.Encrypt(computerName)
            };

            var profiles = _mahadevHwContext.Profiles;
            if (profiles.Any())
            {
                var dbProfile = profiles.First();
                dbProfile.IsEligible = 1;
                dbProfile.K1 = profile.K1;
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

        public JsonResult Revoke(Permission permission)
        {
            var validKey = EncryptDecryptData.Encrypt(permission.Key);
            if (!validKey.Equals(apiKey)) return Json("Invalid key", JsonRequestBehavior.AllowGet);
            var profile = _mahadevHwContext.Profiles.First();
            profile.IsEligible = 0;
            _profile = profile;
            _mahadevHwContext.SaveChanges();
            return Json("Revoked", JsonRequestBehavior.AllowGet);
        }
    }
}