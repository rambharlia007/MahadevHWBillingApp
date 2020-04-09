using MahadevHWBillingApp.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MahadevHWBillingApp.Models;
using Microsoft.EntityFrameworkCore;
using MahadevHWBillingApp.Filters;

namespace MahadevHWBillingApp.Controllers
{
    [CustomSession]
    public class KeyboardController : BaseController
    {
        // GET: Keyboard
        public ActionResult List()
        {
            if (_mahadevHwContext.ShortcutKeys.Count() <= 0)
            {
                var result = new List<ShortcutKey>
                {
                    new ShortcutKey { Key = "F1", Name = "Generate Invoice", Url = "/Billing/New" },
                    new ShortcutKey { Key = "F2", Name = "Add Product", Url = "/item/index" },
                    new ShortcutKey { Key = "F3", Name = "Sales", Url = "/sale/list" },
                    new ShortcutKey { Key = "F4", Name = "Purchase", Url = "/purchase/list" },
                    new ShortcutKey { Key = "F5", Name = "Record payment", Url = "/sale/recordpayment" },
                    new ShortcutKey { Key = "F6", Name = "Estimate", Url = "/estimate/New" },
                    new ShortcutKey { Key = "F7", Name = "Report", Url = "/dashboard/New" },
                    new ShortcutKey { Key = "F8", Name = "Keyboard shortcut", Url = "/keyboard/list" },
                    new ShortcutKey { Key = "F9", Name = "Contacts", Url = "/contact/list" },
                    new ShortcutKey { Key = "F10", Name = "Profile", Url = "/profile/New" },
                };

                _mahadevHwContext.ShortcutKeys.AddRange(result);
                _mahadevHwContext.SaveChanges();
            }
            return View(_profile);
        }

        public JsonResult GetShorcutKeys()
        {
            var items = Helper.Dapper.Get<ShortcutKey>(Query.GetShorcutKeys());
            var response = Json(items, JsonRequestBehavior.AllowGet);
            return response;
        }

        [HttpPost]
        public JsonResult EditShortcut(ShortcutKey key)
        {
            _mahadevHwContext.Entry(key).State = EntityState.Modified;
            _mahadevHwContext.SaveChanges();
            return Json("key Edited");
        }
    }
}