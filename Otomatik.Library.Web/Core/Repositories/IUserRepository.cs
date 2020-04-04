using Otomatik.Library.Web.Areas.Identity.Data;

namespace Otomatik.Library.Web.Core.Repositories
{
    public interface IUserRepository
    {
        ApplicationUser GetByUsername(string username);
    }
}
