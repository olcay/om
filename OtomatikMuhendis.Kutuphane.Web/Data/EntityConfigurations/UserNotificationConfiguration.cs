using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OtomatikMuhendis.Kutuphane.Web.Core.Models;

namespace OtomatikMuhendis.Kutuphane.Web.Data.EntityConfigurations
{
    public class UserNotificationConfiguration
    {
        public UserNotificationConfiguration(EntityTypeBuilder<UserNotification> entityBuilder)
        {
            entityBuilder.HasKey(un => new { un.UserId, un.NotificationId });
        }
    }
}
