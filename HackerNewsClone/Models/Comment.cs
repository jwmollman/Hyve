using System;
using System.Collections.Generic;

namespace HackerNewsClone.Models {
    public class Comment {
        public int Id { get; set; }

        public DateTime DateCreated { get; set; }

        public DateTime DateUpdated { get; set; }

        public string Content { get; set; }

        public Post Post { get; set; }

        public IList<Comment> Comments { get; set; }
    }
}