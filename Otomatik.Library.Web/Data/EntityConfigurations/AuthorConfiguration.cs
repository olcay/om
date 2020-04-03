using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Otomatik.Library.Web.Core.Models;

namespace Otomatik.Library.Web.Data.EntityConfigurations
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
