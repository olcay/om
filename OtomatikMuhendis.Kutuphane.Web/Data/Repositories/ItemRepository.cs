using OtomatikMuhendis.Kutuphane.Web.Core.Models;
using OtomatikMuhendis.Kutuphane.Web.Core.Repositories;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace OtomatikMuhendis.Kutuphane.Web.Data.Repositories
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
            if (item.Id == 0)
            {
                _context.Items.Add(item);
            }
            else
            {
                _context.Items.Update(item);
            }
        }

        public Item GetItem(int bookId)
        {
            return _context.Items
                .Include(b => b.Shelf)
                .Single(b => b.Id == bookId);
        }
    }
}
