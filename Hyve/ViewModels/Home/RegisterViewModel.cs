using System.ComponentModel.DataAnnotations;

namespace Hyve.ViewModels.Home {
    public class RegisterViewModel {
        [Required(ErrorMessage = "Enter your desired username")]
        [Display(Name = "Username")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Enter your email address")]
        [Display(Name = "Email")]
        [EmailAddress(ErrorMessage = "Enter a valid email address")]
        public string EmailAddress { get; set; }

        [Required(ErrorMessage = "Enter your password")]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Confirm your password")]
        [Display(Name = "Confirm Password")]
        [Compare("Password", ErrorMessage = "Passwords must match.")]
        public string PasswordConfirm { get; set; }
    }
}