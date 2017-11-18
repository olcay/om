using OtomatikMuhendis.Kutuphane.Web.Models;
using System;

namespace OtomatikMuhendis.Kutuphane.Web.Dtos
{
    public class NotificationDto
    {
        public DateTime DateTime { get; set; }

        public NotificationType Type { get; set; }

        public BookDto Book { get; set; }
        
        public ShelfDto Shelf { get; set; }
    }
}
