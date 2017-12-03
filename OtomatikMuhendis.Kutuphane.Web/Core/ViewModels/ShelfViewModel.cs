using OtomatikMuhendis.Kutuphane.Web.Core.Models;

namespace OtomatikMuhendis.Kutuphane.Web.Core.ViewModels
{
    public class ShelfViewModel
    {
        public Shelf Shelf { get; set; }

        public bool ShowActions { get; set; }

        public bool IsShelfOwner { get; set; }

        public bool IsStarred { get; set; }
    }
}
