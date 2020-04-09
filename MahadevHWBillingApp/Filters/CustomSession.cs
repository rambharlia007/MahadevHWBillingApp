using MahadevHWBillingApp.Controllers;
using MahadevHWBillingApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace MahadevHWBillingApp.Filters
{
    public class CustomSession : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (filterContext.HttpContext.Session["AccountType"] == null)
            {
                RouteValueDictionary redirectTargetDictionary = new RouteValueDictionary();
                redirectTargetDictionary.Add("action", "Login");
                redirectTargetDictionary.Add("controller", "Account");
                filterContext.Result = new RedirectToRouteResult(redirectTargetDictionary);
            }
            else if (filterContext.HttpContext.Session["AccountType"] != null && filterContext.HttpContext.Session["AccountType"].Equals(AccountType.Admin))
            {
                RouteValueDictionary redirectTargetDictionary = new RouteValueDictionary();
                redirectTargetDictionary.Add("action", "Register");
                redirectTargetDictionary.Add("controller", "Account");
                filterContext.Result = new RedirectToRouteResult(redirectTargetDictionary);
            }
            else if (filterContext.Controller is BaseController)
            {
                BaseController baseController = (BaseController)filterContext.Controller;
                baseController._adminUser = filterContext.HttpContext.Session["AdminUser"] as User;
                baseController._profile = filterContext.HttpContext.Session["Profile"] as Profile;

                if (baseController._adminUser.IsEligible == 0)
                {
                    RouteValueDictionary redirectTargetDictionary = new RouteValueDictionary();
                    redirectTargetDictionary.Add("action", "Admin");
                    redirectTargetDictionary.Add("controller", "Error");
                    filterContext.Result = new RedirectToRouteResult(redirectTargetDictionary);
                }
                else if (baseController._adminUser.IsEligible == 1 && baseController._adminUser.IsFreeTrial == 2)
                {
                    RouteValueDictionary redirectTargetDictionary = new RouteValueDictionary();
                    redirectTargetDictionary.Add("action", "FreeTrial");
                    redirectTargetDictionary.Add("controller", "Error");
                    filterContext.Result = new RedirectToRouteResult(redirectTargetDictionary);
                }
                base.OnActionExecuting(filterContext);
            }
        }
    }
    public class CustomErrorSession : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (filterContext.Controller is BaseController)
            {
                BaseController baseController = (BaseController)filterContext.Controller;
                baseController._adminUser = filterContext.HttpContext.Session["AdminUser"] as User;
                baseController._profile = filterContext.HttpContext.Session["Profile"] as Profile;
                base.OnActionExecuting(filterContext);
            }
        }
    }
}