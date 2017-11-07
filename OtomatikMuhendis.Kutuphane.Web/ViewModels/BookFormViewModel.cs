using OtomatikMuhendis.Kutuphane.Web.Models;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace OtomatikMuhendis.Kutuphane.Web.ViewModels
{
    public class BookFormViewModel
    {
        public string Title { get; set; }

        public int ShelfId { get; set; }

        public IEnumerable<SelectListItem> Shelves { get; set; }
    }
}
