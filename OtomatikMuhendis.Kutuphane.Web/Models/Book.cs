﻿using System;
using System.ComponentModel.DataAnnotations;

namespace OtomatikMuhendis.Kutuphane.Web.Models
{
    public class Book
    {
        public int Id { get; set; }

        [Required]
        public ApplicationUser CreatedBy { get; set; }

        public DateTime CreationDate { get; set; }

        [Required]
        [StringLength(255)]
        public string Title { get; set; }

        [Required]
        public Shelf Shelf { get; set; }
    }
}
