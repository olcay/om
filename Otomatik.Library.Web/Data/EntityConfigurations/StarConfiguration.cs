using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Otomatik.Library.Web.Core.Models;

namespace Otomatik.Library.Web.Data.EntityConfigurations
{
    public class StarConfiguration
    {
        public StarConfiguration(EntityTypeBuilder<Star> entityBuilder)
        {
            entityBuilder.HasKey(s => new { s.ShelfId, s.UserId });
        }
    }
}
