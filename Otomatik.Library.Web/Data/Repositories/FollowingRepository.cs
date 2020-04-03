using Otomatik.Library.Web.Core.Models;
using Otomatik.Library.Web.Core.Repositories;
using System.Collections.Generic;
using System.Linq;
using Otomatik.Library.Web.Areas.Identity.Data;

namespace Otomatik.Library.Web.Data.Repositories
{
    public class FollowingRepository : IFollowingRepository
    {
        private readonly ApplicationDbContext _context;

        public FollowingRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<ApplicationUser> GetFollowers(string followeeId)
        {
            return _context.Followings
                .Where(f => f.FolloweeId == followeeId)
                .Select(f => f.Follower)
                .ToList();
        }
    }
}
