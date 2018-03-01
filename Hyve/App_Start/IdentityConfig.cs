using Hyve.Models;
using Hyve.Models.Contexts;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Hyve.App_Start {
    public class UserStore : UserStore<User> {
        public UserStore(HyveDbContext dbContext) : base(dbContext) {
        }
    }

    public class UserManager : UserManager<User> {
        public UserManager(IUserStore<User> userStore) : base(userStore) {
        }
    }
}
