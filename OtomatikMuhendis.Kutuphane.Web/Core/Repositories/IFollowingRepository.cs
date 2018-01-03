using OtomatikMuhendis.Kutuphane.Web.Core.Models;
using System.Collections.Generic;

namespace OtomatikMuhendis.Kutuphane.Web.Core.Repositories
{
    public interface IFollowingRepository
    {
        IList<ApplicationUser> GetFollowers(string followeeId);
    }
}