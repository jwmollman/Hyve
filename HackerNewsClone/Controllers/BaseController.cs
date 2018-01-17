using HackerNewsClone.Models;
using HackerNewsClone.Models.Contexts;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HackerNewsClone.Controllers {
    public class BaseController : Controller {
        protected UserManager<User> UserManager { get; set; }
        protected RoleManager<Role> RoleManager { get; set; }

        public BaseController() {
            HackerNewsCloneDbContext db = new HackerNewsCloneDbContext();

            UserStore<User> userStore = new UserStore<User>(db);
            UserManager = new UserManager<User>(userStore);

            RoleStore<Role> roleStore = new RoleStore<Role>(db);
            RoleManager = new RoleManager<Role>(roleStore);
        }
    }
}