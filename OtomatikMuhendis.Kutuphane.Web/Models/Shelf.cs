using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OtomatikMuhendis.Kutuphane.Web.Models
{
    public class Shelf
    {
        public int Id { get; set; }

        [Required]
        [StringLength(255)]
        public string Title { get; set; }

        public ApplicationUser CreatedBy { get; set; }

        [Required]
        public string CreatedById { get; set; }

        public DateTime CreationDate { get; set; }

        public IEnumerable<Book> Books { get; set; }

        public IEnumerable<Star> Stars { get; set; }

        public bool IsDeleted { get; set; }

        public bool IsPublic { get; set; }

        public DateTime UpdateDate { get; set; }
    }
}
