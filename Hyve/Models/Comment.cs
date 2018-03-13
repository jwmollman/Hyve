using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Hyve.Models {
    public class Comment {
        [Key]
        public int Id { get; set; }

        public DateTime DateCreatedUtc { get; set; }

        public DateTime DateUpdatedUtc { get; set; }

        public string Content { get; set; }

        public int Rating { get; set; }

        public Post Post { get; set; }

        public User CreatedBy { get; set; }

        public IList<Comment> Comments { get; set; }
    }
}