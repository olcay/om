using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OtomatikMuhendis.Kutuphane.Web.Core.Models;

namespace OtomatikMuhendis.Kutuphane.Web.Data.EntityConfigurations
{
    public class BookConfiguration
    {
        public BookConfiguration(EntityTypeBuilder<Book> entityBuilder)
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
