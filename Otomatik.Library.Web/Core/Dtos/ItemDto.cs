using System.ComponentModel.DataAnnotations;

namespace Otomatik.Library.Web.Core.Dtos
{
    public class ItemDto
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [StringLength(255)]
        public string Title { get; set; }
    }
}
