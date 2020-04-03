using Otomatik.Library.Web.Core.Models;
using Otomatik.Library.Web.Core.Repositories;

namespace Otomatik.Library.Web.Data.Repositories
{
    public class BookAuthorRepository : IBookAuthorRepository
    {
        private readonly ApplicationDbContext _context;

        public BookAuthorRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public void Save(BookAuthor bookAuthor)
        {
            _context.BookAuthors.Add(bookAuthor);
        }
    }
}
