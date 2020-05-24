using System.Collections.Generic;
using System.Linq;
using Otomatik.Library.Web.Core.Models;

namespace Otomatik.Library.Web.Core.Repositories
{
    public interface IShelfRepository
    {
        void Save(Shelf shelf);

        Shelf GetShelf(int shelfId);

        IQueryable<Shelf> GetPublicShelves(string query);

        IEnumerable<Shelf> GetUserShelves(string userId, string query = null, int limit = 0);

        IQueryable<Shelf> GetShelvesByUser(string userId);

        IQueryable<Shelf> GetStarredShelves(string userId);
    }
}