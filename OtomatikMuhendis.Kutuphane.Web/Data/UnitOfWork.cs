using OtomatikMuhendis.Kutuphane.Web.Core;
using OtomatikMuhendis.Kutuphane.Web.Core.Repositories;

namespace OtomatikMuhendis.Kutuphane.Web.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;

        public IFollowingRepository Followings { get; }

        public IShelfRepository Shelves { get; }

        public UnitOfWork(ApplicationDbContext context, 
            IShelfRepository shelfRepository, 
            IFollowingRepository followingRepository)
        {
            _context = context;
            Shelves = shelfRepository;
            Followings = followingRepository;
        }

        public void Complete()
        {
            _context.SaveChanges();
        }
    }
}
