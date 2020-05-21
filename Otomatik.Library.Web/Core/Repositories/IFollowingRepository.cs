using System.Collections.Generic;
using Otomatik.Library.Web.Areas.Identity.Data;
using Otomatik.Library.Web.Core.Models;

namespace Otomatik.Library.Web.Core.Repositories
{
    public interface IFollowingRepository
    {
        IList<ApplicationUser> GetFollowers(string userId);

        int CountFollowers(string userId);

        int CountFollowings(string userId);

        bool IsFollowing(string userId, string followerId);

        IList<ApplicationUser> GetFollowings(string userId);

        Following GetFollowing(string userId, string followerId);

        void Save(Following following);

        void Delete(Following following);
    }
}