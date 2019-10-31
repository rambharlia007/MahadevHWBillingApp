using MahadevHWBillingApp.Models;
using System.Linq;
using System.Web.Mvc;

namespace MahadevHWBillingApp.Controllers
{
    public class AccountController : Controller
    {
        private CoreContext _coreContext;
        public AccountController()
        {
            _coreContext = new CoreContext();
        }
        public ActionResult New()
        {
            var adminAccount = _coreContext.Users.Where(e => e.AccountType.Equals(AccountType.Admin)).FirstOrDefault();
            if (adminAccount == null)
                return RedirectToAction("RegisterAdmin", "Account");

            ViewBag.BusinessName = adminAccount.BusinessName;
            return View();
        }

        public ActionResult Register()
        {
            if (Session != null && Session["AccountType"].ToString().Equals(AccountType.Admin))
                return View();
            return RedirectToAction("New", "Account");
        }

        public ActionResult RegisterAdmin()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(User user)
        {
            try
            {
                var currentUser = _coreContext.Users.Where(e => e.Name == user.Name).SingleOrDefault();
                if (currentUser == null)
                    return Json(new { Message = "User does not exists, Please register" });
                else if (EncryptDecryptData.Decrypt(currentUser.Password) == user.Password)
                {
                    Session["AccountType"] = currentUser.AccountType;
                    Session["AccountId"] = currentUser.AccountId;

                    if (currentUser.AccountType.Equals(AccountType.Admin))
                        return Json(new { Status = "Success", Link = "/Account/Register" });
                    return Json(new { Status = "Success", Link = "/Billing/New" });
                }
                else
                    return Json(new { Status = "Failure", Message = "Incorrect Password." });
            }
            catch (System.Exception ex)
            {
                return Json(new { Status = "Failure", Message = "Incorrect Password." });
            }
        }

        [HttpPost]
        public ActionResult Register(User user)
        {
            var lastUser = _coreContext.Users.LastOrDefault();
            user.AccountId = 1;
            user.Password = EncryptDecryptData.Encrypt(user.Password);
            // accountId is used for session, also act as schema id
            if (lastUser != null)
                user.AccountId = lastUser.AccountId + 1;

            _coreContext.Users.Add(user);
            _coreContext.SaveChanges();
            return Json(new { Message = "Account registered successfully." });
        }
        [HttpPost]
        public ActionResult RegisterAdmin(AdminUser user)
        {
            if (!EncryptDecryptData.Encrypt(user.MasterPassword).Equals(Keys.MasterPassword))
                return Json(new { Status = "Failure", Message = "Master key is not correct" });

            var isAdminExist = _coreContext.Users.Count(e => e.AccountType.Equals(AccountType.Admin)) > 0;
            if (isAdminExist)
                return Json(new { Status = "Failure", Message = "Admin already exists" });

            user.Password = EncryptDecryptData.Encrypt(user.Password);
            user.AccountType = AccountType.Admin;
            _coreContext.Users.Add(user);
            _coreContext.SaveChanges();
            return Json(new { Status = "Success", Link = "/Account/New" });
        }

        public JsonResult Users()
        {
            var result = _coreContext.Users.Where(e => !e.AccountType.Equals(AccountType.Admin));
            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}