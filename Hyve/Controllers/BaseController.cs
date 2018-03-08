using Hyve.App_Start;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using System.Web;
using System.Web.Mvc;

namespace Hyve.Controllers {
    public abstract class BaseController : Controller {
        public UserManager UserManager {
            get {
                return HttpContext.GetOwinContext().Get<UserManager>();
            }
        }

        public SignInManager SignInManager {
            get {
                return HttpContext.GetOwinContext().Get<SignInManager>();
            }
        }

        public IAuthenticationManager AuthenticationManager {
            get {
                return HttpContext.GetOwinContext().Authentication;
            }
        }
    }
}