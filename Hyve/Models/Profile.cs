using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hyve.Models {
    public class Profile {
        [Key]
        [ForeignKey("User")]
        public string UserId { get; set; }

        public User User { get; set; }

        public DateTime DateCreatedUtc { get; set; }

        public DateTime DateUpdatedUtc { get; set; }

        public string Bio { get; set; }
    }
}