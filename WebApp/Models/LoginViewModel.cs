using System.ComponentModel.DataAnnotations;

namespace WebApp.Models
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Please enter a valid email address.")]
        [Display(Name = "Email")]
        public string Email { get; set; } = null!;

        [Required(ErrorMessage = "Password is required.")]
        [Display(Name = "Password")]
        public string Password { get; set; } = null!;

        [Display(Name = "Remember Me")]
        public bool RememberMe { get; set; }
    }
}
