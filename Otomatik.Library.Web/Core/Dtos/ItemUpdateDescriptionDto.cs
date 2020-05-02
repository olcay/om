using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace Otomatik.Library.Web.Core.Dtos
{
    public class ItemUpdateDescriptionDto
    {
        [FromForm(Name = "pk")]
        [Required]
        public int Id { get; set; }

        [FromForm(Name = "value")]
        [Required]
        [StringLength(1000)]
        public string Description { get; set; }
    }
}
