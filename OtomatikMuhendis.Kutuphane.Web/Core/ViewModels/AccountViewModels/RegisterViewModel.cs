using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace OtomatikMuhendis.Kutuphane.Web.Core.ViewModels.AccountViewModels
{
    public class RegisterViewModel
    {
        [Required]
        [StringLength(100)]
        [RegularExpression("^[a-zA-Z0-9]*$", ErrorMessage = "Username may only contain alphanumeric characters.")]
        [Display(Name = "Username")]
        [Remote("IsUserExists", "Home", ErrorMessage = "Username is already taken.")]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }
}
