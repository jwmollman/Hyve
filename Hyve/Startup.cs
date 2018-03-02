using Hyve.App_Start;
using Hyve.Models.Contexts;
using Microsoft.AspNet.Identity;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Owin;

namespace Hyve {
    public class Startup {
        public void Configuration(IAppBuilder app) {
            app.CreatePerOwinContext(HyveDbContext.Create);
            app.CreatePerOwinContext<UserManager>(UserManager.Create);
            app.CreatePerOwinContext<SignInManager>(SignInManager.Create);

            app.UseCookieAuthentication(new CookieAuthenticationOptions() {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/login"),
                ReturnUrlParameter = "redirect",
            });
        }
    }
}