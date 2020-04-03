using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Otomatik.Library.Web.Core.Models;

namespace Otomatik.Library.Web.Data.EntityConfigurations
{
    public class ItemBookDetailConfiguration
    {
        public ItemBookDetailConfiguration(EntityTypeBuilder<ItemBookDetail> entityBuilder)
        {
            entityBuilder.HasKey(ib => new { ib.ItemId, ib.BookDetailId });
        }
    }
}
