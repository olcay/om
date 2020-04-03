using Otomatik.Library.Web.Core.Models;

namespace Otomatik.Library.Web.Core.Repositories
{
    public interface IItemBookDetailRepository
    {
        void Save(ItemBookDetail itemBookDetail);

        BookDetail GetBookDetailByItemId(int itemId);

        Item GetItemByBookDetailId(int bookDetailId, string userId);
    }
}
