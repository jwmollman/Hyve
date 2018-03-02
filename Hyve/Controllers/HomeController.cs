using Hyve.App_Start;
using Hyve.Models;
using Hyve.ViewModels.Home;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Hyve.Controllers {
    [AllowAnonymous]
    public class HomeController : BaseController {
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
                
                var result = await UserManager.CreateAsync(newUser, model.Password);
                if (result.Succeeded) {
                    await SignInManager.SignInAsync(newUser, isPersistent: false, rememberBrowser: false);
                    return RedirectToAction("Index", "Account");
                }
                AddErrorsToModelState(result.Errors);
            } catch (Exception e) {
                ModelState.AddModelError(string.Empty, e.Message);
            }

            return View(model);
        }

        [HttpGet]
        public ActionResult Login(string redirect) {
            if (User.Identity.IsAuthenticated) {
                return RedirectToAction("Index", "Account");
            }

            ViewBag.RedirectUrl = redirect;
            return View(new LoginViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel model, string redirect) {
            try {
                if (!ModelState.IsValid) {
                    return View(model);
                }

                var result = await SignInManager.PasswordSignInAsync(
                    model.Username,
                    model.Password,
                    model.RememberMe,
                    shouldLockout: false);

                switch (result) {
                    case SignInStatus.Success:
                        return Redirect(redirect);
                    case SignInStatus.LockedOut:
                        return View("LockedOut");
                    case SignInStatus.Failure:
                    default:
                        ModelState.AddModelError(string.Empty, "Invalid login");
                        break;
                }
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