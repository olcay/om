using Otomatik.Library.Web.Core.Models;

namespace Otomatik.Library.Web.Core.Repositories
{
    public interface IAuthorRepository
    {
        void Save(Author author);
        Author GetAuthor(int authorId);
        Author GetAuthor(string name);
    }
}
