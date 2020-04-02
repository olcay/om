using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace OtomatikMuhendis.Kutuphane.Web.Core.Dtos
{
    public class ItemDto
    {
        [FromForm(Name = "pk")]
        [Required]
        public int Id { get; set; }

        [FromForm(Name = "value")]
        [Required]
        [StringLength(255)]
        public string Title { get; set; }
    }
}
