using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace OtomatikMuhendis.Kutuphane.Web.Models
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser
    {
        [Required]
        [StringLength(100)]
        public string Name { get; set; }
    }
}
