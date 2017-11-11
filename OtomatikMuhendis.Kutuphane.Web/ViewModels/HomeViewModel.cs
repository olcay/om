using OtomatikMuhendis.Kutuphane.Web.Models;
using System.Collections.Generic;

namespace OtomatikMuhendis.Kutuphane.Web.ViewModels
{
    public class HomeViewModel
    {
        public IEnumerable<Shelf> Shelves { get; set; }

        public bool ShowActions { get; set; }
    }
}
