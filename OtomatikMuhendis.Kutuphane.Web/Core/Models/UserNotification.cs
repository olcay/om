using System;

namespace OtomatikMuhendis.Kutuphane.Web.Core.Models
{
    public class UserNotification
    {
        public ApplicationUser User { get; private set; }

        public Notification Notification { get; private set; }

        public string UserId { get; private set; }

        public int NotificationId { get; private set; }

        public bool IsRead { get; private set; }

        protected UserNotification()
        {
        }

        public UserNotification(ApplicationUser user, Notification notification)
        {
            User = user ?? throw new ArgumentNullException(nameof(user));
            Notification = notification ?? throw new ArgumentNullException(nameof(notification));
        }

        public void Read()
        {
            IsRead = true;
        }
    }
}
