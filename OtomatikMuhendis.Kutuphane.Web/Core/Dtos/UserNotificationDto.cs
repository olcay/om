namespace OtomatikMuhendis.Kutuphane.Web.Core.Dtos
{
    public class UserNotificationDto
    {
        public NotificationDto Notification { get; set; }
        
        public bool IsRead { get; set; }
    }
}
