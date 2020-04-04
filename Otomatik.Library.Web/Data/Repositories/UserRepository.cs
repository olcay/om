using System.Linq;
using Otomatik.Library.Web.Core.Repositories;
using Otomatik.Library.Web.Areas.Identity.Data;

namespace Otomatik.Library.Web.Data.Repositories
{
    public class UserRepository: IUserRepository
    {
        private readonly ApplicationDbContext _context;

        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public ApplicationUser GetByUsername(string username)
        {
            return _context.Users
                .SingleOrDefault(p => p.Name == username);
        }
    }
}
