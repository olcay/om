using Otomatik.Library.Web.Core;
using Otomatik.Library.Web.Core.Repositories;

namespace Otomatik.Library.Web.Data
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
        public IUserRepository Users { get; }
        public IStarRepository Stars { get; }

        public UnitOfWork(ApplicationDbContext context,
            IShelfRepository shelfRepository,
            IFollowingRepository followingRepository,
            IItemRepository bookRepository,
            IBookDetailRepository bookDetails,
            IBookAuthorRepository bookAuthors,
            IAuthorRepository authors,
            IItemBookDetailRepository itemBookDetails, 
            IStarRepository stars, 
            IUserRepository users)
        {
            _context = context;
            Shelves = shelfRepository;
            Followings = followingRepository;
            Items = bookRepository;
            BookDetails = bookDetails;
            BookAuthors = bookAuthors;
            Authors = authors;
            ItemBookDetails = itemBookDetails;
            Stars = stars;
            Users = users;
        }

        public void Complete()
        {
            _context.SaveChanges();
        }
    }
}
