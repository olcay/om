using System.Collections.Generic;
using OtomatikMuhendis.Kutuphane.Web.Models;

namespace OtomatikMuhendis.Kutuphane.Web.ViewModels
{
    public class ProfileViewModel : ShelvesViewModel
    {
        public ApplicationUser User { get; set; }

        public bool IsFollowing { get; set; }

        public bool IsProfileOwner { get; set; }

        public string Tab { get; set; }

        public IEnumerable<ApplicationUser> Users { get; set; }

        public ProfileViewModel()
        {
            Users = new List<ApplicationUser>();
            Shelves = new List<Shelf>();
        }
    }
}
