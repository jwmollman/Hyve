using HackerNewsClone.Models;
using HackerNewsClone.Models.Contexts;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace HackerNewsClone.App_Start {
    public class UserStore : UserStore<User> {
        public UserStore(HackerNewsCloneDbContext dbContext) : base(dbContext) {
        }
    }

    public class UserManager : UserManager<User> {
        public UserManager(IUserStore<User> userStore) : base(userStore) {
        }
    }
}
