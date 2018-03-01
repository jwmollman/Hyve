using System.ComponentModel.DataAnnotations;

namespace Hyve.ViewModels.Home {
    public class LoginViewModel {
        [Required(ErrorMessage = "Enter your username")]
        [Display(Name = "Username")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Enter your password")]
        [Display(Name = "Password")]
        public string Password { get; set; }
    }
}