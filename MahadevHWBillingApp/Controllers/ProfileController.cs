using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MahadevHWBillingApp.Filters;
using MahadevHWBillingApp.Models;
using Microsoft.EntityFrameworkCore;

namespace MahadevHWBillingApp.Controllers
{
    [CustomSession]
    public class ProfileController : BaseController
    {
        public ActionResult New()
        {
            return View(_profile);
        }

        [HttpPost]
        public JsonResult Save(Profile profile)
        {
            _mahadevHwContext.Profiles.Add(profile);
            _mahadevHwContext.SaveChanges();
            _profile = profile;
            Session["Profile"] = _profile;
            return Json(new {Id = profile.Id, Message = "Profile added successfully"});
        }

        [HttpPost]
        public JsonResult Edit(Profile profile)
        {
            _profile = _mahadevHwContext.Profiles.FirstOrDefault();
            _profile.BusinessName = profile.BusinessName;
            _profile.MobileNumber = profile.MobileNumber;
            _profile.Address = profile.Address;
            _profile.Email = profile.Email;
            _profile.GSTIN = profile.GSTIN;
            _profile.Owner = profile.Owner;
            _mahadevHwContext.SaveChanges();
            Session["Profile"] = _profile;
            return Json(new {Id = profile.Id, Message = "Profile edited successfully"});
        }

        public JsonResult Get()
        {
            var result = _mahadevHwContext.Profiles.FirstOrDefault();
            return Json(result ?? new Profile(), JsonRequestBehavior.AllowGet);
        }
    }
}