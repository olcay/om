using OtomatikMuhendis.Kutuphane.Web.Core.Models;

namespace OtomatikMuhendis.Kutuphane.Web.Core.Repositories
{
    public interface IItemBookDetailRepository
    {
        void Save(ItemBookDetail itemBookDetail);

        BookDetail GetBookDetailByItemId(int itemId);

        Item GetItemByBookDetailId(int bookDetailId, string userId);
    }
}
