using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MahadevHWBillingApp.Models;
using Microsoft.EntityFrameworkCore;

namespace MahadevHWBillingApp.Controllers
{
    [HandleError]
    public class ProfileController : BaseController
    {
        public ActionResult New()
        {
            if (_adminUser.IsEligible == 0)
                return RedirectToAction("Admin", "Error");
            else if (_adminUser.IsEligible == 1 && _adminUser.IsFreeTrial == 2)
            {
                return RedirectToAction("FreeTrial", "Error");
            }
            return View(_profile);
        }

        [HttpPost]
        public JsonResult Save(Profile profile)
        {
            _mahadevHwContext.Profiles.Add(profile);
            _mahadevHwContext.SaveChanges();
            _profile = profile;
            return Json(new {Id = profile.Id, Message = "Profile added successfully"});
        }

        [HttpPost]
        public JsonResult Edit(Profile profile)
        {
            _profile.BusinessName = profile.BusinessName;
            _profile.MobileNumber = profile.MobileNumber;
            _profile.Address = profile.Address;
            _profile.Email = profile.Email;
            _profile.GSTIN = profile.GSTIN;
            _profile.Owner = profile.Owner;
            _mahadevHwContext.SaveChanges();

            return Json(new {Id = profile.Id, Message = "Profile edited successfully"});
        }

        public JsonResult Get()
        {
            var result = _mahadevHwContext.Profiles.FirstOrDefault();
            return Json(result ?? new Profile(), JsonRequestBehavior.AllowGet);
        }
    }
}