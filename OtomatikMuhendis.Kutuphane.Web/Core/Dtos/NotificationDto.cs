using OtomatikMuhendis.Kutuphane.Web.Core.Enums;
using System;

namespace OtomatikMuhendis.Kutuphane.Web.Core.Dtos
{
    public class NotificationDto
    {
        public DateTime DateTime { get; set; }

        public NotificationType Type { get; set; }

        public ItemDto Item { get; set; }
        
        public ShelfDto Shelf { get; set; }
    }
}
