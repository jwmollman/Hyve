using System;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Owin;

[assembly: OwinStartup(typeof(HackerNewsClone.App_Start.IdentityConfig))]

namespace HackerNewsClone.App_Start {
    public class IdentityConfig {
        public void Configuration(IAppBuilder app) {
            CookieAuthenticationOptions cookieOptions = new CookieAuthenticationOptions();
            cookieOptions.AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie;
            cookieOptions.LoginPath = new PathString("~/login");

            app.UseCookieAuthentication(cookieOptions);
        }
    }
}
