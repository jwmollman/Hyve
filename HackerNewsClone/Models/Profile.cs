using System;
using System.ComponentModel.DataAnnotations;

namespace HackerNewsClone.Models {
    public class Profile {
        public int Id { get; set; }

        public DateTime DateCreatedUtc { get; set; }

        public DateTime DateUpdatedUtc { get; set; }

        public string Bio { get; set; }

        [Required]
        public User User { get; set; }
    }
}