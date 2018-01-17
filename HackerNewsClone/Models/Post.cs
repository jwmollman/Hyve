using System;
using System.Collections.Generic;

namespace HackerNewsClone.Models {
    public class Post {
        public int Id { get; set; }

        public DateTime DateCreatedUtc { get; set; }

        public DateTime DateUpdatedUtc { get; set; }

        public string Content { get; set; }

        public User CreatedBy { get; set; }

        public bool Enabled { get; set; }

        public IList<Comment> Comments { get; set; }
    }
}