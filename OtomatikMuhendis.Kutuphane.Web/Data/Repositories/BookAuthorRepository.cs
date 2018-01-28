using OtomatikMuhendis.Kutuphane.Web.Core.Models;
using OtomatikMuhendis.Kutuphane.Web.Core.Repositories;

namespace OtomatikMuhendis.Kutuphane.Web.Data.Repositories
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
