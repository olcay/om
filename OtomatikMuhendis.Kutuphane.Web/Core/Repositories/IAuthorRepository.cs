using OtomatikMuhendis.Kutuphane.Web.Core.Models;

namespace OtomatikMuhendis.Kutuphane.Web.Core.Repositories
{
    public interface IAuthorRepository
    {
        void Save(Author author);
        Author GetAuthor(int authorId);
        Author GetAuthor(string name);
    }
}
