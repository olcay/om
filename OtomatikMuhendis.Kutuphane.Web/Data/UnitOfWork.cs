using OtomatikMuhendis.Kutuphane.Web.Core;
using OtomatikMuhendis.Kutuphane.Web.Core.Repositories;

namespace OtomatikMuhendis.Kutuphane.Web.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;

        public IFollowingRepository Followings { get; }

        public IShelfRepository Shelves { get; }

        public IBookRepository Books { get; }

        public UnitOfWork(ApplicationDbContext context, 
            IShelfRepository shelfRepository, 
            IFollowingRepository followingRepository,
            IBookRepository bookRepository)
        {
            _context = context;
            Shelves = shelfRepository;
            Followings = followingRepository;
            Books = bookRepository;
        }

        public void Complete()
        {
            _context.SaveChanges();
        }
    }
}
