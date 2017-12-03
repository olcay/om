using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OtomatikMuhendis.Kutuphane.Web.Core.Models;

namespace OtomatikMuhendis.Kutuphane.Web.Data.EntityConfigurations
{
    public class StarConfiguration
    {
        public StarConfiguration(EntityTypeBuilder<Star> entityBuilder)
        {
            entityBuilder.HasKey(s => new { s.ShelfId, s.UserId });
        }
    }
}
