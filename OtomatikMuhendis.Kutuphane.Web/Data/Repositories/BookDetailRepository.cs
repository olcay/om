using OtomatikMuhendis.Kutuphane.Web.Core.Models;
using OtomatikMuhendis.Kutuphane.Web.Core.Repositories;
using System.Linq;

namespace OtomatikMuhendis.Kutuphane.Web.Data.Repositories
{
    public class BookDetailRepository : IBookDetailRepository
    {
        private readonly ApplicationDbContext _context;

        public BookDetailRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public void Save(BookDetail bookDetail)
        {
            _context.BookDetails.Add(bookDetail);
        }

        public BookDetail GetBookDetail(int bookDetailId)
        {
            return _context.BookDetails
                .Single(b => b.Id == bookDetailId);
        }

        public BookDetail GetBookDetail(string gBooKId)
        {
            return _context.BookDetails
                .SingleOrDefault(b => b.GoogleBookId == gBooKId);
        }
    }
}
