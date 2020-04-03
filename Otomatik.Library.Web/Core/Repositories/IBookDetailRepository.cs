using Otomatik.Library.Web.Core.Models;

namespace Otomatik.Library.Web.Core.Repositories
{
    public interface IBookDetailRepository
    {
        void Save(BookDetail bookDetail);
        BookDetail GetBookDetail(int bookDetailId);
        BookDetail GetBookDetail(string gBooKId);
    }
}
