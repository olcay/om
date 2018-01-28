using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OtomatikMuhendis.Kutuphane.Web.Core.Models;

namespace OtomatikMuhendis.Kutuphane.Web.Data.EntityConfigurations
{
    public class AuthorConfiguration
    {
        public AuthorConfiguration(EntityTypeBuilder<Author> entityBuilder)
        {
            entityBuilder.Property(p => p.Name)
                .IsRequired()
                .HasMaxLength(255);
        }
    }
}
