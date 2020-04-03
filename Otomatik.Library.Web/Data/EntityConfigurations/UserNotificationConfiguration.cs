using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Otomatik.Library.Web.Core.Models;

namespace Otomatik.Library.Web.Data.EntityConfigurations
{
    public class UserNotificationConfiguration
    {
        public UserNotificationConfiguration(EntityTypeBuilder<UserNotification> entityBuilder)
        {
            entityBuilder.HasKey(un => new { un.UserId, un.NotificationId });
        }
    }
}
