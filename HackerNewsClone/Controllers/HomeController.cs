using HackerNewsClone.ViewModels.Home;
using System.Web.Mvc;

namespace HackerNewsClone.Controllers {
    public class HomeController : Controller {
        public ActionResult Index() {
            return View();
        }

        public ActionResult Register() {
            return View(new RegisterViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(RegisterViewModel model) {
            if (!ModelState.IsValid) {
                return View(model);
            }

            // register the user

            return View(model);
        }
    }
}