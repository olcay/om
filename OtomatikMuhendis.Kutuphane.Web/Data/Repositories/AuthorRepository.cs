using OtomatikMuhendis.Kutuphane.Web.Core.Models;
using OtomatikMuhendis.Kutuphane.Web.Core.Repositories;
using System.Linq;

namespace OtomatikMuhendis.Kutuphane.Web.Data.Repositories
{
    public class AuthorRepository : IAuthorRepository
    {
        private readonly ApplicationDbContext _context;

        public AuthorRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public void Save(Author author)
        {
            _context.Authors.Add(author);
        }

        public Author GetAuthor(int authorId)
        {
            return _context.Authors
                .Single(b => b.Id == authorId);
        }

        public Author GetAuthor(string name)
        {
            return _context.Authors
                .SingleOrDefault(b => b.Name == name);
        }
    }
}
