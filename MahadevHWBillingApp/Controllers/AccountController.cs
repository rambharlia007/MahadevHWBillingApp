using MahadevHWBillingApp.Models;
using System;
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
        public ActionResult Login()
        {
            var adminAccount = _coreContext.Users.FirstOrDefault(e => e.AccountType.Equals(AccountType.Admin));
            if (adminAccount == null)
                return RedirectToAction("RegisterAdmin", "Account");
            else if (Session != null && Session["Profile"] != null && Session["AdminUser"] != null)
                return RedirectToAction("New", "Billing");
            else if (Session != null && Session["AccountType"] != null && Session["AccountType"].Equals(AccountType.Admin))
                return RedirectToAction("Register", "Account");
            ViewBag.BusinessName = adminAccount.BusinessName;
            return View();
        }

        public ActionResult Register()
        {
            if (Session != null && Session["AccountType"] != null && Session["AccountType"].ToString().Equals(AccountType.Admin))
            {
                var adminUser = _coreContext.Users.FirstOrDefault(e => e.AccountType.Equals(AccountType.Admin));
                ViewBag.BusinessName = adminUser.BusinessName;
                ViewBag.UserName = adminUser.Name;
                return View();
            }
            return RedirectToAction("Login", "Account");
        }

        public ActionResult RegisterAdmin()
        {
            ViewBag.BusinessName = "Product by Ram Bharlia";
            ViewBag.UserName = "GST billing software";
            return View();
        }

        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Login", "Account");
        }

        [HttpPost]
        public ActionResult Login(User user)
        {
            try
            {
                var users = _coreContext.Users.ToList();
                var currentUser = users.FirstOrDefault(e => e.Name == user.Name);

                if (currentUser == null)
                    return Json(new { Message = "User does not exists, Please register" });
                else if (EncryptDecryptData.Decrypt(currentUser.Password) == user.Password)
                {
                    Session["AccountType"] = currentUser.AccountType;
                    Session["AccountId"] = currentUser.AccountId;

                    if (currentUser.AccountType.Equals(AccountType.Admin))
                    {
                        return Json(new { Status = "Success", Link = "/Account/Register" });
                    }

                    var adminUser = users.FirstOrDefault(e => e.AccountType.Equals(AccountType.Admin));
                    adminUser.CheckForAccountValidity();
                    Session["AdminUser"] = adminUser;
                    using (var accountContext = new MahadevHWContext())
                    {
                        var profile = accountContext.Profiles.FirstOrDefault();
                        if (profile != null)
                            Session["Profile"] = profile;
                        else
                        {
                            // new account is created from admin, inject the profile data from corecontext to GSTBillingContext
                            var profileFromAdminUser = new Profile
                            {
                                Address = currentUser.Address,
                                BusinessName = currentUser.BusinessName,
                                Email = currentUser.Email,
                                GSTIN = currentUser.GSTIN,
                                MobileNumber = currentUser.MobileNumber,
                                Owner = currentUser.Owner,
                                EnableStockCount = currentUser.EnableStockCount
                            };
                            Session["Profile"] = profileFromAdminUser;
                            accountContext.Profiles.Add(profileFromAdminUser);
                            accountContext.SaveChanges();
                        }
                    }
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
            if (Session != null && Session["AccountType"].ToString().Equals(AccountType.Admin))
            {
                if (_coreContext.Users.Count() == 6)
                    return Json(new { Message = "Maximun 5 Account are only allowed. Please contact admin" });
                var tempUser = _coreContext.Users.ToList();
                var lastUser = tempUser.LastOrDefault();
                user.AccountId = 1;
                user.Password = EncryptDecryptData.Encrypt(user.Password);
                user.AccountType = AccountType.TaxAccount;
                // accountId is used for session, also act as schema id
                if (lastUser != null)
                    user.AccountId = lastUser.AccountId + 1;

                _coreContext.Users.Add(user);
                _coreContext.SaveChanges();
                return Json(new { Message = "Account registered successfully." });
            }
            return Json(new { Message = "User is not Admin." });
        }
        [HttpPost]
        public ActionResult RegisterAdmin(AdminUser user)
        {
            if (!EncryptDecryptData.Encrypt(user.MasterPassword).Equals(Keys.MasterPassword))
                return Json(new { Status = "Failure", Message = "Master key is not correct" });

            var isAdminExist = _coreContext.Users.Count(e => e.AccountType.Equals(AccountType.Admin)) > 0;
            if (isAdminExist)
                return Json(new { Status = "Failure", Message = "Admin already exists" });

            var computerName = System.Net.Dns.GetHostName();
            user.Password = EncryptDecryptData.Encrypt(user.Password);
            user.AccountType = AccountType.Admin;
            user.IsEligible = 1;
            user.Key = EncryptDecryptData.Encrypt(DateTime.Now.Date.AddDays(90).ToString("dd-MM-yyyy"));
            user.K1 = EncryptDecryptData.Encrypt(computerName);
            _coreContext.Users.Add(user);
            _coreContext.SaveChanges();
            return Json(new { Status = "Success", Link = "/Account/Login" });
        }

        public JsonResult Users()
        {
            if (Session != null && Session["AccountType"].ToString().Equals(AccountType.Admin))
            {
                var result = _coreContext.Users.ToList().Where(e => !e.AccountType.Equals(AccountType.Admin));
                result.ToList().ForEach((user) =>
                {
                    user.Password = EncryptDecryptData.Decrypt(user.Password);
                });
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            return Json("User is not admin", JsonRequestBehavior.AllowGet);
        }
    }
}