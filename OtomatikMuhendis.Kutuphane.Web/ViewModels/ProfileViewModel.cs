using OtomatikMuhendis.Kutuphane.Web.Models;
using System.Collections.Generic;

namespace OtomatikMuhendis.Kutuphane.Web.ViewModels
{
    public class ProfileViewModel
    {
        public IEnumerable<Shelf> Shelves { get; set; }

        public ApplicationUser User { get; set; }

        public bool IsFollowing { get; set; }

        public bool IsProfileOwner { get; set; }

        public bool ShowActions { get; set; }

        public string Tab { get; set; }
    }
}
