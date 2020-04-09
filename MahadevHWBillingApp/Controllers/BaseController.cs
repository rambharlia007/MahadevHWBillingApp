using System.Web.Mvc;
using MahadevHWBillingApp.Models;

namespace MahadevHWBillingApp.Controllers
{
    public class BaseController : Controller
    {
        protected MahadevHWContext _mahadevHwContext;
        public Profile _profile;
        public User _adminUser;

        public BaseController()
        {
            try
            {
                _mahadevHwContext = new MahadevHWContext();
            }
            catch (System.Exception ex)
            {
                //
                
            }
        }
    }
}