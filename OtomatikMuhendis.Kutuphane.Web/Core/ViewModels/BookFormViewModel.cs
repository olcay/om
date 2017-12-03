using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace OtomatikMuhendis.Kutuphane.Web.Core.ViewModels
{
    public class BookFormViewModel
    {
        [Required]
        [StringLength(255)]
        public string Title { get; set; }
        
        public int Shelf { get; set; }

        public IEnumerable<SelectListItem> Shelves { get; set; }
    }
}
