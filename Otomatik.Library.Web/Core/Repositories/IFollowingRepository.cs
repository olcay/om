using Otomatik.Library.Web.Core.Models;
using System.Collections.Generic;
using Otomatik.Library.Web.Areas.Identity.Data;

namespace Otomatik.Library.Web.Core.Repositories
{
    public interface IFollowingRepository
    {
        IList<ApplicationUser> GetFollowers(string followeeId);
    }
}