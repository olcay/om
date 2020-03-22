using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OtomatikMuhendis.Kutuphane.Web.Core.Models;

namespace OtomatikMuhendis.Kutuphane.Web.Data.EntityConfigurations
{
    public class ItemBookDetailConfiguration
    {
        public ItemBookDetailConfiguration(EntityTypeBuilder<ItemBookDetail> entityBuilder)
        {
            entityBuilder.HasKey(ib => new { ib.ItemId, ib.BookDetailId });
        }
    }
}
