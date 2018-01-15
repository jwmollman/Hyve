using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace HackerNewsClone.Models.Contexts {
    public class HackerNewsCloneDbContext : IdentityDbContext<User> {
        public HackerNewsCloneDbContext() : base("HackerNewsCloneDbContextConnection") {
        }
        
        public DbSet<Profile> Profiles { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Comment> Comments { get; set; }
    }
}