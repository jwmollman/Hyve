using HackerNewsClone.Models;
using HackerNewsClone.ViewModels.Home;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace HackerNewsClone.Controllers {
    public class HomeController : BaseController {
        public ActionResult Index() {
            return View();
        }

        public ActionResult Register() {
            return View(new RegisterViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(RegisterViewModel model) {
            try {
                if (!ModelState.IsValid) {
                    return View(model);
                }

                User newUser = new User() {
                    UserName = model.Username,
                    Email = model.EmailAddress,
                    DateCreated = DateTime.Now,
                    DateUpdated = DateTime.Now,
                    LockoutEndDateUtc = DateTime.MaxValue,
                    Profile = new Profile(),
                    Posts = new List<Post>(),
                    Comments = new List<Comment>(),
                };

                IdentityResult result = UserManager.Create(newUser, model.Password);
                if (result.Succeeded) {
                    UserManager.AddToRole(newUser.Id, "User");
                    return RedirectToAction("Login");
                }

                return View(model);
            } catch (Exception e) {
                throw new Exception(e.Message);
            }
        }

        public ActionResult Login() {
            return View();
        }
    }
}