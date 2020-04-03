using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Otomatik.Library.Web.Core.Models;

namespace Otomatik.Library.Web.Data.EntityConfigurations
{
    public class ItemConfiguration
    {
        public ItemConfiguration(EntityTypeBuilder<Item> entityBuilder)
        {
            entityBuilder.Property(s => s.Title)
                .IsRequired()
                .HasMaxLength(255);

            entityBuilder.Property(s => s.CreatedById)
                .IsRequired();

            entityBuilder.Property(s => s.ShelfId)
                .IsRequired();
        }
    }
}
