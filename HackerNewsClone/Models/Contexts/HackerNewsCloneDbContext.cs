using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;

namespace HackerNewsClone.Models.Contexts {
    public class HackerNewsCloneDbContext : IdentityDbContext<User> {
        public HackerNewsCloneDbContext() : base("HackerNewsCloneDbContextConnection", throwIfV1Schema: false) {
        }

        public DbSet<Profile> Profiles { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Comment> Comments { get; set; }
    }
}