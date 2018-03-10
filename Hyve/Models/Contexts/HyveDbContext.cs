using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;

namespace Hyve.Models.Contexts {
    public class HyveDbContext : IdentityDbContext<User> {
        public HyveDbContext() : base("HyveDbContextConnection", throwIfV1Schema: false) {
        }

        public DbSet<Profile> Profiles { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<PostType> PostTypes { get; set; }
        public DbSet<Comment> Comments { get; set; }

        public static HyveDbContext Create() {
            return new HyveDbContext();
        }
    }
}