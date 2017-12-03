using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OtomatikMuhendis.Kutuphane.Web.Core.Models;

namespace OtomatikMuhendis.Kutuphane.Web.Data.EntityConfigurations
{
    public class ShelfConfiguration
    {
        public ShelfConfiguration(EntityTypeBuilder<Shelf> entityBuilder)
        {
            entityBuilder.Property(s => s.Title)
                .IsRequired()
                .HasMaxLength(255);

            entityBuilder.Property(s => s.CreatedById)
                .IsRequired();

            entityBuilder.Ignore(s => s.StarsCount);
        }
    }
}
