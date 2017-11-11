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
        private ApplicationDbContext _context;

        public ProfilesController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet("/profile/{userId}/{tab?}")]
        public IActionResult Index(string userId, string tab = ProfileTabs.Shelves)
        {
            var isUserAuthenticated = User.Identity.IsAuthenticated;
            var loggedInUserId = User.GetUserId();

            if (string.IsNullOrWhiteSpace(userId))
            {
                if (!isUserAuthenticated)
                {
                    return Error();
                }

                userId = loggedInUserId;
            }

            var profileUser = _context.Users
                .SingleOrDefault(p => p.Id == userId);

            if (profileUser == null)
            {
                return Error();
            }

            var viewModel = new ProfileViewModel
            {
                ShowActions = isUserAuthenticated,
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
