using Otomatik.Library.Web.Core.Models;

namespace Otomatik.Library.Web.Core.Repositories
{
    public interface IShelfRepository
    {
        void Save(Shelf shelf);
        Shelf GetShelf(int shelfId);
    }
}