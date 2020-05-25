using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace Otomatik.Library.Web.Core.Dtos
{
    public class ShelfDto
    {
        //We need those ugly names because the js library which we use for editable areas is sending parameters like this
        [FromForm(Name = "pk")]
        [Required]
        public int Id { get; set; }

        [FromForm(Name = "value")]
        [Required]
        [StringLength(255)]
        public string Title { get; set; }

        public List<ItemDto> Items { get; set; }

        public DateTime UpdateDate { get; set; }

        public bool IsStarred { get; set; }

        public string Slug { get; set; }
    }
}
