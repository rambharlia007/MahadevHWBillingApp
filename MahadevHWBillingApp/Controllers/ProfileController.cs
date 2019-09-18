using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MahadevHWBillingApp.Models;
using Microsoft.EntityFrameworkCore;

namespace MahadevHWBillingApp.Controllers
{
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
            return Json(new {Id = profile.Id, Message = "Profile added successfully"});
        }

        [HttpPost]
        public JsonResult Edit(Profile profile)
        {
            _mahadevHwContext.Entry(profile).State = EntityState.Modified;
            _mahadevHwContext.SaveChanges();
            _profile = profile;
            return Json(new {Id = profile.Id, Message = "Profile edited successfully"});
        }

        public JsonResult Get()
        {
            var result = _mahadevHwContext.Profiles.FirstOrDefault();
            return Json(result ?? new Profile(), JsonRequestBehavior.AllowGet);
        }
    }
}