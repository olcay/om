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

        public void Save(Following following)
        {
            _context.Followings.Add(following);
        }

        public void Delete(Following following)
        {
            _context.Followings.Remove(following);
        }

        public IList<ApplicationUser> GetFollowers(string userId)
        {
            return _context.Followings
                .Where(f => f.FolloweeId == userId)
                .Select(f => f.Follower)
                .ToList();
        }

        public IList<ApplicationUser> GetFollowings(string userId)
        {
            return _context.Followings
                .Where(f => f.FollowerId == userId)
                .Select(f => f.Followee)
                .ToList();
        }

        public int CountFollowers(string userId)
        {
            return _context.Followings
                .Count(f => f.FolloweeId == userId);
        }

        public int CountFollowings(string userId)
        {
            return _context.Followings
                .Count(f => f.FollowerId == userId);
        }

        public bool IsFollowing(string userId, string followerId)
        {
            return _context.Followings
                .Any(f => f.FollowerId == followerId && f.FolloweeId == userId);
        }

        public Following GetFollowing(string userId, string followerId)
        {
            return _context.Followings
                .FirstOrDefault(a => a.FollowerId == followerId && a.FolloweeId == userId);
        }
    }
}
