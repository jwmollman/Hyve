using System;
using System.Collections.Generic;

namespace Hyve.Models {
    public class Comment {
        public int Id { get; set; }

        public DateTime DateCreatedUtc { get; set; }

        public DateTime DateUpdatedUtc { get; set; }

        public string Content { get; set; }

        public Post Post { get; set; }

        public IList<Comment> Comments { get; set; }
    }
}