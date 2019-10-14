using MahadevHWBillingApp.Helper;
using MahadevHWBillingApp.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace MahadevHWBillingApp.Controllers
{
    public class ContactController : BaseController
    {
        public ActionResult List()
        {
            return View(_profile);
        }
        public JsonResult GetData()
        {
            var Contacts = Helper.Dapper.Get<Contact>(Query.GetContacts);
            var response = Json(Contacts, JsonRequestBehavior.AllowGet);
            response.MaxJsonLength = int.MaxValue;
            return response;
        }
        [HttpPost]
        public JsonResult Add(List<Contact> Contacts)
        {
            foreach (var contact in Contacts)
            {
                _mahadevHwContext.Contacts.Add(contact);
            }
            _mahadevHwContext.SaveChanges();
            return Json("contact Added");
        }
        [HttpPost]
        public JsonResult Update(Contact contact)
        {
            _mahadevHwContext.Entry(contact).State = EntityState.Modified;
            _mahadevHwContext.SaveChanges();
            return Json("Item Edited");
        }

        public JsonResult Remove(int id)
        {
            var contact = _mahadevHwContext.Contacts.Where(e => e.Id == id).First();
            contact.IsDelete = 1;
            _mahadevHwContext.SaveChanges();
            return Json("contact deleted", JsonRequestBehavior.AllowGet);
        }
        public JsonResult Search(string q)
        {
            var Contacts = Helper.Dapper.Get<Contact>(Query.GetContactsBySearch(q));
            var response = Json(Contacts, JsonRequestBehavior.AllowGet);
            response.MaxJsonLength = int.MaxValue;
            return response;
        }

        public JsonResult Get(int id)
        {
            var contact = _mahadevHwContext.Contacts.Where(e => e.Id == id).First();
            var response = Json(contact, JsonRequestBehavior.AllowGet);
            return response;
        }
    }
}   