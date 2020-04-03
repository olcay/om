using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Otomatik.Library.Web.Core.Models;

namespace Otomatik.Library.Web.Data.EntityConfigurations
{
    public class BookAuthorConfiguration
    {
        public BookAuthorConfiguration(EntityTypeBuilder<BookAuthor> entityBuilder)
        {
            entityBuilder.HasKey(s => new { s.BookDetailId, s.AuthorId });
        }
    }
}
