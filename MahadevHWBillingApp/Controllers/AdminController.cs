using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MahadevHWBillingApp.Filters;
using MahadevHWBillingApp.Models;

namespace MahadevHWBillingApp.Controllers
{
    [CustomSession]
    public class AdminController : BaseController
    {
        public ActionResult New()
        {
            return View(_profile);
        }

        public JsonResult Revoke(Permission permission)
        {
            var validKey = EncryptDecryptData.Encrypt(permission.Key);
            if (!validKey.Equals(Keys.MasterPassword)) return Json("Invalid key", JsonRequestBehavior.AllowGet);
            var profile = _mahadevHwContext.Profiles.First();
          //  profile.IsEligible = 0;
            _profile = profile;
            _mahadevHwContext.SaveChanges();
            return Json("Revoked", JsonRequestBehavior.AllowGet);
        }

        public JsonResult ChangeDate(string date)
        {
            var value = EncryptDecryptData.Encrypt(date);
            var profile = _mahadevHwContext.Profiles.First();
         //   profile.Key = value;
            _profile = profile;
            _mahadevHwContext.SaveChanges();
            return Json("Date Changed", JsonRequestBehavior.AllowGet);
        }
        public JsonResult ChangeSystem(string system)
        {
            var value = EncryptDecryptData.Encrypt(system);
            var profile = _mahadevHwContext.Profiles.First();
            //profile.K1 = value;
            _profile = profile;
            _mahadevHwContext.SaveChanges();
            return Json("Date Changed", JsonRequestBehavior.AllowGet);
        }
    }
}