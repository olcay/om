using OtomatikMuhendis.Kutuphane.Web.Core.Models;
using OtomatikMuhendis.Kutuphane.Web.Core.Repositories;
using System.Collections.Generic;
using System.Linq;

namespace OtomatikMuhendis.Kutuphane.Web.Data.Repositories
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
