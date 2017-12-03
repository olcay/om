using OtomatikMuhendis.Kutuphane.Web.Core.Models;

namespace OtomatikMuhendis.Kutuphane.Web.Core.Repositories
{
    public interface IShelfRepository
    {
        void Save(Shelf shelf);
        Shelf GetShelf(int shelfId);
    }
}