using Otomatik.Library.Web.Core.Models;
using System.Linq;
using Otomatik.Library.Web.Core.Repositories;

namespace Otomatik.Library.Web.Data.Repositories
{
    public class StarRepository: IStarRepository
    {
        private readonly ApplicationDbContext _context;

        public StarRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public void Save(Star star)
        {
            _context.Stars.Add(star);
        }

        public ILookup<int, Star> GetStarLookup(string userId)
        {
            return _context.Stars.Where(s => s.UserId == userId).ToLookup(s => s.ShelfId);
        }
    }
}
