using OtomatikMuhendis.Kutuphane.Web.Data;
using OtomatikMuhendis.Kutuphane.Web.Repositories;

namespace OtomatikMuhendis.Kutuphane.Web.Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;

        public FollowingRepository Followings { get; }

        public ShelfRepository Shelves { get; }

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
            Shelves = new ShelfRepository(_context);
            Followings = new FollowingRepository(_context);
        }

        public void Complete()
        {
            _context.SaveChanges();
        }
    }
}
