using System.Web.Mvc;
using MahadevHWBillingApp.Models;

namespace MahadevHWBillingApp.Controllers
{
    public class BaseController : Controller
    {
        protected MahadevHWContext _mahadevHwContext;
        protected Profile _profile;
        protected User _adminUser;

        public BaseController()
        {
            _mahadevHwContext = new MahadevHWContext();
            _profile = Session["Profile"] as Profile;
            _adminUser = Session["AdminUser"] as User;
        }
    }
}