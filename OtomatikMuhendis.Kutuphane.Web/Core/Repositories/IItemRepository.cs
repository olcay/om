using OtomatikMuhendis.Kutuphane.Web.Core.Models;

namespace OtomatikMuhendis.Kutuphane.Web.Core.Repositories
{
    public interface IItemRepository
    {
        void Save(Item item);
        Item GetItem(int itemId);

        Item GetItemByRawgId(int rawgId, int shelfId);
    }
}
