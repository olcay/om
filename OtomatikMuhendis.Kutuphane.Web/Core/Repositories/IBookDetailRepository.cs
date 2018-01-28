using OtomatikMuhendis.Kutuphane.Web.Core.Models;

namespace OtomatikMuhendis.Kutuphane.Web.Core.Repositories
{
    public interface IBookDetailRepository
    {
        void Save(BookDetail bookDetail);
        BookDetail GetBookDetail(int bookDetailId);
        BookDetail GetBookDetail(string gBooKId);
    }
}
