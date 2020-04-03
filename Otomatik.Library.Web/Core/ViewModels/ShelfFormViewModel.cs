using System.ComponentModel.DataAnnotations;

namespace Otomatik.Library.Web.Core.ViewModels
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
