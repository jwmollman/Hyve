using Hyve.App_Start;
using Hyve.Models.Contexts;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using System.Web;
using System.Web.Mvc;

namespace Hyve.Controllers {
    public class BaseController : Controller {
        protected HyveDbContext db = new HyveDbContext();

        private UserManager _userManager;
        public UserManager UserManager {
            get {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<UserManager>();
            }
            private set {
                _userManager = value;
            }
        }

        private SignInManager _signInManager;
        public SignInManager SignInManager {
            get {
                return _signInManager ?? HttpContext.GetOwinContext().Get<SignInManager>();
            }
            private set {
                _signInManager = value;
            }
        }

        private IAuthenticationManager _authenticationManager;
        public IAuthenticationManager AuthenticationManager {
            get {
                return HttpContext.GetOwinContext().Authentication;
            }
            private set {
                _authenticationManager = value;
            }
        }

        protected override void Dispose(bool disposing) {
            if (disposing) {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}