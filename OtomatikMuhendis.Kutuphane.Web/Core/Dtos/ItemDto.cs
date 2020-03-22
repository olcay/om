using System;

namespace OtomatikMuhendis.Kutuphane.Web.Core.Dtos
{
    public class ItemDto
    {
        public int Id { get; set; }

        public UserDto CreatedBy { get; set; }

        public DateTime CreationDate { get; set; }
        
        public string Title { get; set; }

        public ShelfDto Shelf { get; set; }

        public bool IsDeleted { get; set; }
    }
}
