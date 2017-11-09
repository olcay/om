using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OtomatikMuhendis.Kutuphane.Web.ViewModels
{
    public class BookFormViewModel
    {
        [Required]
        public string Title { get; set; }
        
        public int Shelf { get; set; }

        public IEnumerable<SelectListItem> Shelves { get; set; }
    }
}
