using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OtomatikMuhendis.Kutuphane.Web.Core.Models;

namespace OtomatikMuhendis.Kutuphane.Web.Data.EntityConfigurations
{
    public class BookAuthorConfiguration
    {
        public BookAuthorConfiguration(EntityTypeBuilder<BookAuthor> entityBuilder)
        {
            entityBuilder.HasKey(s => new { s.BookDetailId, s.AuthorId });
        }
    }
}
