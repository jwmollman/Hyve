using Hyve.ViewModels.Account;
using Microsoft.AspNet.Identity;
using System.Web.Mvc;

namespace Hyve.Controllers {
    [Authorize]
    public class AccountController : BaseController {
        [HttpGet]
        public ActionResult Index() {
            DashboardViewModel model = new DashboardViewModel();
            model.Username = User.Identity.Name;
            return View(model);
        }

        [HttpGet]
        public ActionResult Logout() {
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            return RedirectToAction("Index", "Home");
        }
    }
}