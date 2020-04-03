using Otomatik.Library.Web.Core.Models;
using System.Collections.Generic;

namespace Otomatik.Library.Web.Core.Repositories
{
    public interface IFollowingRepository
    {
        IList<ApplicationUser> GetFollowers(string followeeId);
    }
}