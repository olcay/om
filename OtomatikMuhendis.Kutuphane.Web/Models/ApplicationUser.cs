using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

namespace OtomatikMuhendis.Kutuphane.Web.Models
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser
    {
        [Required]
        [StringLength(100)]
        [RegularExpression("^[a-zA-Z0-9]*$", ErrorMessage = "Username may only contain alphanumeric characters.")]
        [Display(Name= "Username")]
        public string Name { get; set; }

        public ICollection<Following> Followers { get; set; }
        public ICollection<Following> Followees { get; set; }

        public ApplicationUser()
        {
            Followers = new Collection<Following>();
            Followees = new Collection<Following>();
        }
    }
}
