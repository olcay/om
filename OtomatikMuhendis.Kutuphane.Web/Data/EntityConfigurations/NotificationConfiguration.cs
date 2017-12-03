using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OtomatikMuhendis.Kutuphane.Web.Core.Models;

namespace OtomatikMuhendis.Kutuphane.Web.Data.EntityConfigurations
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
