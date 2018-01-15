using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;

namespace HackerNewsClone.Models {
    public class User : IdentityUser {
        public DateTime DateCreated { get; set; }

        public DateTime DateUpdated { get; set; }

        public Profile Profile { get; set; }

        public IList<Post> Posts { get; set; }

        public IList<Comment> Comments { get; set; }
    }
}