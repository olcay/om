using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OtomatikMuhendis.Kutuphane.Web.Data;
using OtomatikMuhendis.Kutuphane.Web.ViewModels;
using System.Diagnostics;
using System.Linq;
using OtomatikMuhendis.Kutuphane.Web.Enums;
using OtomatikMuhendis.Kutuphane.Web.Extensions;

namespace OtomatikMuhendis.Kutuphane.Web.Controllers
{
    public class ProfilesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProfilesController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet("/p/{userName}/{tab?}")]
        public IActionResult Index(string userName, string tab = ProfileTabs.Shelves)
        {
            if (string.IsNullOrWhiteSpace(userName))
            {
                return Error();
            }

            var profileUser = _context.Users
                .SingleOrDefault(p => p.Name == userName);

            if (profileUser == null)
            {
                return Error();
            }
            
            var loggedInUserId = User.GetUserId();

            var userId = profileUser.Id;

            var viewModel = new ProfileViewModel
            {
                ShowActions = User.Identity.IsAuthenticated,
                User = profileUser,
                IsProfileOwner = loggedInUserId == userId,
                Tab = tab
            };

            if (!viewModel.IsProfileOwner)
            {
                viewModel.IsFollowing =
                    _context.Followings
                        .Any(f => f.FollowerId == loggedInUserId
                            && f.FolloweeId == userId);
            }
            
            if (tab == ProfileTabs.Stars)
            {
                viewModel.Shelves = _context.Stars
                    .Where(s => s.UserId == userId)
                    .Select(s => s.Shelf)
                    .Include(s => s.CreatedBy)
                    .Include(s => s.Books)
                    .ToList();
            }
            else if (tab == ProfileTabs.Followers)
            {
                viewModel.Users = _context.Followings
                    .Where(f => f.FolloweeId == userId)
                    .Select(f => f.Follower)
                    .ToList();
            }
            else if (tab == ProfileTabs.Following)
            {
                viewModel.Users = _context.Followings
                    .Where(f => f.FollowerId == userId)
                    .Select(f => f.Followee)
                    .ToList();
            }
            else
            {
                viewModel.Shelves = _context.Shelves
                    .Include(b => b.Books)
                    .Include(b => b.CreatedBy)
                    .Where(s => s.CreatedById == userId)
                    .OrderByDescending(b => b.CreationDate)
                    .ToList();
            }

            return View(viewModel);
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
