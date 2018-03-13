using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Hyve.Models {
    public class Post {
        [Key]
        public int Id { get; set; }

        public DateTime DateCreatedUtc { get; set; }

        public DateTime DateUpdatedUtc { get; set; }

        public string Title { get; set; }

        public string LinkUrl { get; set; }

        public User CreatedBy { get; set; }
        
        public IList<Comment> Comments { get; set; }
    }
}