using System.Collections.Generic;
using OtomatikMuhendis.Kutuphane.Web.Core.Models;

namespace OtomatikMuhendis.Kutuphane.Web.Core.Repositories
{
    public interface IFollowingRepository
    {
        IEnumerable<ApplicationUser> GetFollowers(string followeeId);
    }
}