using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Otomatik.Library.Web.Core.Dtos
{
    public class ShelfDto
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [StringLength(255)]
        public string Title { get; set; }

        public List<ItemDto> Items { get; set; }

        public DateTime UpdateDate { get; set; }

        public bool IsStarred { get; set; }

        public string Slug { get; set; }
    }
}
