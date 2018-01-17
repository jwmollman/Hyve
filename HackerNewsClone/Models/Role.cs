using Microsoft.AspNet.Identity.EntityFramework;

namespace HackerNewsClone.Models {
    public class Role : IdentityRole {
        public Role() : base() {
        }

        public Role(string name) : base(name) {
        }

        public string Description { get; set; }
    }
}