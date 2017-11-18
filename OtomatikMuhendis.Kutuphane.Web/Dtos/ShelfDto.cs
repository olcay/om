using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace OtomatikMuhendis.Kutuphane.Web.Dtos
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

        public UserDto CreatedBy { get; set; }
    }
}
