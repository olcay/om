using System;
using OtomatikMuhendis.Kutuphane.Web.Core.Models;

namespace OtomatikMuhendis.Kutuphane.Web.Core.Dtos
{
    public class NotificationDto
    {
        public DateTime DateTime { get; set; }

        public NotificationType Type { get; set; }

        public BookDto Book { get; set; }
        
        public ShelfDto Shelf { get; set; }
    }
}
