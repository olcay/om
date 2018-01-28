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
        public IBookDetailRepository BookDetails { get; }
        public IBookAuthorRepository BookAuthors { get; }
        public IAuthorRepository Authors { get; }

        public UnitOfWork(ApplicationDbContext context, 
            IShelfRepository shelfRepository, 
            IFollowingRepository followingRepository,
            IBookRepository bookRepository, 
            IBookDetailRepository bookDetails, 
            IBookAuthorRepository bookAuthors, 
            IAuthorRepository authors)
        {
            _context = context;
            Shelves = shelfRepository;
            Followings = followingRepository;
            Books = bookRepository;
            BookDetails = bookDetails;
            BookAuthors = bookAuthors;
            Authors = authors;
        }

        public void Complete()
        {
            _context.SaveChanges();
        }
    }
}
