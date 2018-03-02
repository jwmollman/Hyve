using Hyve.App_Start;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using System.Web;
using System.Web.Mvc;

namespace Hyve.Controllers {
    public class BaseController : Controller {
        private UserManager _userManager;
        private SignInManager _signInManager;

        public UserManager UserManager {
            get {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<UserManager>();
            }
            private set {
                _userManager = value;
            }
        }

        public SignInManager SignInManager {
            get {
                return _signInManager ?? HttpContext.GetOwinContext().Get<SignInManager>();
            }
            private set {
                _signInManager = value;
            }
        }

        public IAuthenticationManager AuthenticationManager {
            get {
                return HttpContext.GetOwinContext().Authentication;
            }
        }
    }
}