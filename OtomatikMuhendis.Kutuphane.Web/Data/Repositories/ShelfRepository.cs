using OtomatikMuhendis.Kutuphane.Web.Core.Models;
using System.Linq;
using OtomatikMuhendis.Kutuphane.Web.Core.Repositories;

namespace OtomatikMuhendis.Kutuphane.Web.Data.Repositories
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
                Books = s.Books.Where(b => !b.IsDeleted).ToList(),
                Title = s.Title,
                CreatedById = s.CreatedById,
                CreatedBy = s.CreatedBy,
                IsPublic = s.IsPublic,
                CreationDate = s.CreationDate,
                UpdateDate = s.UpdateDate,
                StarsCount = s.Stars.Count()
            }).FirstOrDefault(s => s.Id == shelfId
                                   && !s.IsDeleted);
        }
    }
}