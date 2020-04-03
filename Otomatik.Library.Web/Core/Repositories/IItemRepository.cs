using Otomatik.Library.Web.Core.Models;

namespace Otomatik.Library.Web.Core.Repositories
{
    public interface IItemRepository
    {
        void Save(Item item);
        Item GetItem(int itemId);

        Item GetItemByRawgId(int rawgId, int shelfId);
    }
}
