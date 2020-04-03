using Otomatik.Library.Web.Core.Models;
using Otomatik.Library.Web.Core.Repositories;
using System.Linq;

namespace Otomatik.Library.Web.Data.Repositories
{
    public class ShelfRepository : IShelfRepository
    {
        private readonly ApplicationDbContext _context;

        public ShelfRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public void Save(Shelf shelf)
        {
            _context.Shelves.Add(shelf);
        }

        public Shelf GetShelf(int shelfId)
        {
            return _context.Shelves.Select(s => new Shelf
            {
                Id = s.Id,
                Items = s.Items.Where(b => !b.IsDeleted).ToList(),
                Title = s.Title,
                CreatedById = s.CreatedById,
                CreatedBy = s.CreatedBy,
                IsPublic = s.IsPublic,
                CreationDate = s.CreationDate,
                UpdateDate = s.UpdateDate,
                Slug = s.Slug,
                StarsCount = s.Stars.Count()
            }).FirstOrDefault(s => s.Id == shelfId
                                   && !s.IsDeleted);
        }
    }
}