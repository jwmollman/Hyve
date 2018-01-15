using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HackerNewsClone.Models {
    public class Role : IdentityRole {
        public Role() : base() {
        }

        public Role(string name) : base(name) {
        }

        public string Description { get; set; }
    }
}