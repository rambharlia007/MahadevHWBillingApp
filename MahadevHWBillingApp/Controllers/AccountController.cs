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
            return View();
        }

        public ActionResult Register()
        {
            return View();
        }   

        [HttpPost]
        public ActionResult Login(User user)
        {
            try
            {
                if (Keys.MasterPassword == user.Password && user.Name.ToLower() == Keys.MasterUserName)
                {
                    return Json(new { Status = "Success", Link = "/Account/Register" });
                }

                var currentUser = _coreContext.Users.Where(e => e.Name == user.Name).SingleOrDefault();
                if (currentUser == null)
                    return Json(new { Message = "User does not exists, Please register" });
                else if (EncryptDecryptData.Decrypt(currentUser.Password) == user.Password)
                {
                    Session["AccountId"] = currentUser.AccountId;
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
    }
}