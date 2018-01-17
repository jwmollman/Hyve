using System.Web.Mvc;

namespace HackerNewsClone.Controllers {
    public class HomeController : Controller {
        public ActionResult Index() {
            return View();
        }
    }
}