using OtomatikMuhendis.Kutuphane.Web.Data;
using OtomatikMuhendis.Kutuphane.Web.Models;
using System.Collections.Generic;
using System.Linq;

namespace OtomatikMuhendis.Kutuphane.Web.Repositories
{
    public class FollowingRepository
    {
        private readonly ApplicationDbContext _context;

        public FollowingRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<ApplicationUser> GetFollowers(string followeeId)
        {
            return _context.Followings
                .Where(f => f.FolloweeId == followeeId)
                .Select(f => f.Follower)
                .ToList();
        }
    }
}
