using Hyve.App_Start;
using Hyve.Models;
using Hyve.ViewModels.Home;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Hyve.Controllers {
    [AllowAnonymous]
    public class HomeController : BaseController {
        [HttpGet]
        public ActionResult Index() {
            PostListViewModel model = new PostListViewModel();
            model.Posts = db.Posts
                .Include(p => p.CreatedBy)
                .Include(p => p.Comments)
                .ToList();
            return View(model);
        }

        [HttpGet]
        public ActionResult Post(int id) {
            PostViewModel model = new PostViewModel();
            Post post = db.Posts
                .Where(p => p.Id == id)
                .Include(p => p.CreatedBy)
                .Include(p => p.Comments)
                .FirstOrDefault();
            model.Post = post;
            ViewBag.Title = post.Title;
            return View(model);
        }

        [HttpGet]
        public new ActionResult User(string id) {
            UserViewModel model = new UserViewModel();
            model.User = db.Users
                .Where(u => u.UserName == id)
                .Include(u => u.Profile)
                .Include(u => u.Posts)
                .FirstOrDefault();
            return View(model);
        }

        [HttpGet]
        public ActionResult Register() {
            ViewBag.Title = "Register";
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
            if (base.User.Identity.IsAuthenticated) {
                return RedirectToAction("Index", "Account");
            }

            ViewBag.Title = "Log in";
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
                        if (!string.IsNullOrEmpty(redirect)) {
                            return Redirect(redirect);
                        } else {
                            return RedirectToAction("Index", "Account");
                        }
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