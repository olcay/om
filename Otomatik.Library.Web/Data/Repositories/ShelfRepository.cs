using System.Collections.Generic;
using Otomatik.Library.Web.Core.Models;
using Otomatik.Library.Web.Core.Repositories;
using System.Linq;
using Microsoft.EntityFrameworkCore;

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

        public IQueryable<Shelf> GetPublicShelves(string query)
        {
            if (!string.IsNullOrWhiteSpace(query))
            {
                query = query.ToLowerInvariant();

                return _context.Shelves
                    .Include(s => s.Items)
                    .Where(s => !s.IsDeleted
                                && s.IsPublic
                                && s.Items.Any()
                                && (s.Title.ToLower().Contains(query)
                                    || s.Items.Any(b => b.Title.ToLower().Contains(query)))
                                ).OrderByDescending(s => s.UpdateDate);
            }

            return _context.Shelves
                .Include(s => s.Items)
                .Where(s => !s.IsDeleted && s.IsPublic && s.Items.Any())
                .OrderByDescending(b => b.UpdateDate);
        }

        public Shelf GetShelf(int shelfId)
        {
            return _context.Shelves
                .Include(s => s.Items)
                .Include(s => s.CreatedBy)
                .FirstOrDefault(s => s.Id == shelfId && !s.IsDeleted);
        }

        public IEnumerable<Shelf> GetUserShelves(string userId, string query = null, int limit = 0)
        {
            var shelves = _context.Shelves
                .Include(s => s.Items)
                .Where(s => !s.IsDeleted && s.CreatedById == userId);

            if (string.IsNullOrWhiteSpace(query))
            {
                return limit > 0
                    ? shelves.OrderByDescending(s => s.UpdateDate).Take(limit)
                    : shelves.OrderByDescending(s => s.UpdateDate);
            }

            query = query.ToLowerInvariant();

            var filtered = shelves.AsEnumerable().Where(s =>
                s.Title.ToLowerInvariant().Contains(query));

            return limit > 0
                ? filtered.OrderByDescending(s => s.UpdateDate).Take(limit)
                : filtered.OrderByDescending(s => s.UpdateDate);
        }

        public IQueryable<Shelf> GetShelvesByUser(string userId)
        {
            return _context.Shelves
                .Where(s => !s.IsDeleted && s.CreatedById == userId)
                .OrderByDescending(s => s.CreationDate);
        }

        public IQueryable<Shelf> GetStarredShelves(string userId)
        {
            return _context.Stars
                .Where(s => s.UserId == userId)
                .Select(s => s.Shelf)
                .Where(s => s.IsPublic && !s.IsDeleted)
                .OrderByDescending(b => b.CreationDate);
        }
    }
}