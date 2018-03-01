using HackerNewsClone.App_Start;
using HackerNewsClone.Models;
using HackerNewsClone.Models.Contexts;
using HackerNewsClone.ViewModels.Home;
using System;
using System.Collections;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace HackerNewsClone.Controllers {
    [AllowAnonymous]
    public class HomeController : Controller {
        [HttpGet]
        public ActionResult Index() {
            return View();
        }

        [HttpGet]
        public ActionResult Register() {
            return View(new RegisterViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(RegisterViewModel model) {
            try {
                if (!ModelState.IsValid) {
                    return View(model);
                }

                User newUser = new User() {
                    UserName = model.Username,
                    Email = model.EmailAddress,
                    DateCreatedUtc = DateTime.Now,
                    DateUpdatedUtc = DateTime.Now,
                };

                UserStore userStore = new UserStore(new HackerNewsCloneDbContext());
                UserManager userManager = new UserManager(userStore);
                var result = await userManager.CreateAsync(newUser, model.Password);
                if (result.Succeeded) {
                    return RedirectToAction("Index");
                } else {
                    AddErrorsToModelState(result.Errors);
                }
            } catch (Exception e) {
                ModelState.AddModelError(string.Empty, e.Message);
            }

            return View(model);
        }

        [HttpGet]
        public ActionResult Login() {
            return View(new LoginViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginViewModel model) {
            try {
                if (!ModelState.IsValid) {
                    return View(model);
                }

                // login
            } catch (Exception e) {
                ModelState.AddModelError(string.Empty, e.Message);
            }

            return View(model);
        }

        #region Helper Methods
        private void AddErrorsToModelState(IEnumerable errors) {
            foreach (string error in errors) {
                ModelState.AddModelError(string.Empty, error);
            }
        }
        #endregion
    }
}