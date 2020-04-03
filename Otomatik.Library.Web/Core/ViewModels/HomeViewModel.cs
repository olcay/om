using System.Collections.Generic;
using System.Linq;
using Otomatik.Library.Web.Core.Models;

namespace Otomatik.Library.Web.Core.ViewModels
{
    public class HomeViewModel
    {
        public IEnumerable<ShelfViewModel> Shelves { get; set; }

        public bool ShowActions { get; set; }

        public string Query { get; set; }

        public IEnumerable<Shelf> UserShelves { get; set; }

        public ILookup<int, Star> Stars { get; set; }

        public HomeViewModel()
        {
            Shelves = Enumerable.Empty<ShelfViewModel>();
            UserShelves = Enumerable.Empty<Shelf>();
        }
    }
}
