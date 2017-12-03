using System.ComponentModel.DataAnnotations;

namespace OtomatikMuhendis.Kutuphane.Web.Core.ViewModels
{
    public class ShelfFormViewModel
    {
        [Required]
        [StringLength(255)]
        public string Title { get; set; }
        
        public int Shelf { get; set; }

        public bool IsPublic { get; set; }
    }
}
