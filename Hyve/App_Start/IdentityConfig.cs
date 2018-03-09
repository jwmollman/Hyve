using Hyve.Models;
using Hyve.Models.Contexts;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security;

namespace Hyve.App_Start {
    public class UserStore : UserStore<User> {
        public UserStore(HyveDbContext dbContext) : base(dbContext) {
        }
    }

    public class UserManager : UserManager<User> {
        public UserManager(IUserStore<User> userStore) : base(userStore) {
        }

        public static UserManager Create(IdentityFactoryOptions<UserManager> options, IOwinContext context) {
            UserStore<User> userStore = new UserStore<User>(context.Get<HyveDbContext>());
            return new UserManager(userStore);
        }
    }

    public class RoleManager : RoleManager<IdentityRole> {
        public RoleManager(RoleStore<IdentityRole> roleStore) : base(roleStore) {
        }

        public static RoleManager Create(IdentityFactoryOptions<RoleManager> options, IOwinContext context) {
            RoleStore<IdentityRole> roleStore = new RoleStore<IdentityRole>(context.Get<HyveDbContext>());
            return new RoleManager(roleStore);
        }
    }

    public class SignInManager : SignInManager<User, string> {
        public SignInManager(UserManager userManager, IAuthenticationManager authManager) : base(userManager, authManager) {
        }

        public static SignInManager Create(IdentityFactoryOptions<SignInManager> options, IOwinContext context) {
            return new SignInManager(context.Get<UserManager>(), context.Authentication);
        }
    }
}
