using System.Collections.Generic;
using System.Linq;
using OtomatikMuhendis.Kutuphane.Web.Core.Models;

namespace OtomatikMuhendis.Kutuphane.Web.Core.ViewModels
{
    public class HomeViewModel
    {
        public IEnumerable<Shelf> Shelves { get; set; }

        public bool ShowActions { get; set; }

        public string Query { get; set; }

        public IEnumerable<Shelf> UserShelves { get; set; }

        public ILookup<int, Star> Stars { get; set; }

        public HomeViewModel()
        {
            Shelves = Enumerable.Empty<Shelf>();
            UserShelves = Enumerable.Empty<Shelf>();
        }
    }
}
