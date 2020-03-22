using OtomatikMuhendis.Kutuphane.Web.Core;
using OtomatikMuhendis.Kutuphane.Web.Core.Repositories;

namespace OtomatikMuhendis.Kutuphane.Web.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;

        public IFollowingRepository Followings { get; }
        public IShelfRepository Shelves { get; }
        public IItemRepository Items { get; }
        public IBookDetailRepository BookDetails { get; }
        public IBookAuthorRepository BookAuthors { get; }
        public IAuthorRepository Authors { get; }
        public IItemBookDetailRepository ItemBookDetails { get; }

        public UnitOfWork(ApplicationDbContext context,
            IShelfRepository shelfRepository,
            IFollowingRepository followingRepository,
            IItemRepository bookRepository,
            IBookDetailRepository bookDetails,
            IBookAuthorRepository bookAuthors,
            IAuthorRepository authors,
            IItemBookDetailRepository itemBookDetails)
        {
            _context = context;
            Shelves = shelfRepository;
            Followings = followingRepository;
            Items = bookRepository;
            BookDetails = bookDetails;
            BookAuthors = bookAuthors;
            Authors = authors;
            ItemBookDetails = itemBookDetails;
        }

        public void Complete()
        {
            _context.SaveChanges();
        }
    }
}
