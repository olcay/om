using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OtomatikMuhendis.Kutuphane.Web.Core.Models;

namespace OtomatikMuhendis.Kutuphane.Web.Data.EntityConfigurations
{
    public class BookDetailConfiguration
    {
        public BookDetailConfiguration(EntityTypeBuilder<BookDetail> entityBuilder)
        {
            entityBuilder.Property(p => p.Subtitle)
                .HasMaxLength(255);

            entityBuilder.Property(p => p.Description)
                .HasMaxLength(1000);
        }
    }
}
