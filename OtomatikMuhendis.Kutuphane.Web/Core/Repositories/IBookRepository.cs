using OtomatikMuhendis.Kutuphane.Web.Core.Models;

namespace OtomatikMuhendis.Kutuphane.Web.Core.Repositories
{
    public interface IBookRepository
    {
        void Save(Book book);
        Book GetBook(int bookId);
        Book GetBookByDetailId(int bookDetailId, int shelfId);
    }
}
