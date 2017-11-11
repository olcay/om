using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace OtomatikMuhendis.Kutuphane.Web.ViewModels.ManageViewModels
{
    public class IndexViewModel
    {
        [Required]
        [StringLength(100)]
        [RegularExpression("^[a-zA-Z0-9]*$", ErrorMessage = "Username may only contain alphanumeric characters.")]
        [Display(Name = "Username")]
        [Remote("IsUserExists", "Home", ErrorMessage = "Username is already taken.")]
        public string Name { get; set; }

        public string Username { get; set; }

        public bool IsEmailConfirmed { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Phone]
        [Display(Name = "Phone number")]
        public string PhoneNumber { get; set; }

        public string StatusMessage { get; set; }
    }
}
