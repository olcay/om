using System.Collections.Generic;
using Otomatik.Library.Web.Core.Models;

namespace Otomatik.Library.Web.Core.Repositories
{
    public interface IShelfRepository
    {
        void Save(Shelf shelf);

        Shelf GetShelf(int shelfId);

        IEnumerable<Shelf> GetPublicShelves(string query);

        IEnumerable<Shelf> GetUserShelves(string userId, string query = null, int limit = 0);
    }
}