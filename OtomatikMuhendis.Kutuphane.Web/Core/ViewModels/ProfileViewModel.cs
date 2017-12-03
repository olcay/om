using System.Collections.Generic;
using System.Linq;
using OtomatikMuhendis.Kutuphane.Web.Core.Models;

namespace OtomatikMuhendis.Kutuphane.Web.Core.ViewModels
{
    public class ProfileViewModel
    {
        public ApplicationUser User { get; set; }

        public bool IsFollowing { get; set; }

        public bool IsProfileOwner { get; set; }

        public string Tab { get; set; }

        public string ImageUrl { get; set; }

        public IEnumerable<ApplicationUser> Users { get; set; }

        public IEnumerable<Shelf> Shelves { get; set; }

        public bool ShowActions { get; set; }

        public ILookup<int, Star> Stars { get; set; }

        public ProfileViewModel()
        {
            Users = new List<ApplicationUser>();
            Shelves = new List<Shelf>();
        }
    }
}
