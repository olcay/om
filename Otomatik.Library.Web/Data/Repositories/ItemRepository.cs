using Otomatik.Library.Web.Core.Models;
using Otomatik.Library.Web.Core.Repositories;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Otomatik.Library.Web.Data.Repositories
{
    public class ItemRepository : IItemRepository
    {
        private readonly ApplicationDbContext _context;

        public ItemRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public void Save(Item item)
        {
            if (item.Id <= 0)
            {
                _context.Items.Add(item);
            }
            else
            {
                _context.Items.Update(item);
            }
        }

        public Item GetItemByRawgId(int rawgId, int shelfId)
        {
            return _context.Items
                .SingleOrDefault(b => b.RawgId == rawgId && b.ShelfId == shelfId);
        }

        public Item GetItem(int bookId)
        {
            return _context.Items
                .Include(b => b.Shelf)
                .Include(b => b.CreatedBy)
                .Single(b => b.Id == bookId);
        }
    }
}
