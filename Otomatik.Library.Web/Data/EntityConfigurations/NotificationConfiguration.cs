using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Otomatik.Library.Web.Core.Models;

namespace Otomatik.Library.Web.Data.EntityConfigurations
{
    public class NotificationConfiguration
    {
        public NotificationConfiguration(EntityTypeBuilder<Notification> entityBuilder)
        {
            entityBuilder.Property(n => n.ShelfId)
                .IsRequired();
        }
    }
}
