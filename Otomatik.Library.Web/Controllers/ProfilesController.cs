using Microsoft.AspNetCore.Mvc;
using Otomatik.Library.Web.Core.ViewModels;
using Otomatik.Library.Web.Data;
using Otomatik.Library.Web.Extensions;
using System.Diagnostics;
using System.Linq;
using Otomatik.Library.Web.Core;
using Otomatik.Library.Web.Core.Enums;
using Otomatik.Library.Web.Core.Helpers;
using Otomatik.Library.Web.Core.Models;
using Otomatik.Library.Web.Models;

namespace Otomatik.Library.Web.Controllers
{
    public class ProfilesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IUnitOfWork _unitOfWork;

        public ProfilesController(ApplicationDbContext context, IUnitOfWork unitOfWork)
        {
            _context = context;
            _unitOfWork = unitOfWork;
        }

        [HttpGet("/p/{userName}/{tab?}")]
        public IActionResult Index(string userName, string tab = ProfileTabs.Shelves)
        {
            if (string.IsNullOrWhiteSpace(userName))
            {
                return Error();
            }

            var profileUser = _unitOfWork.Users.GetByUsername(userName);

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
                Tab = tab,
                ImageUrl = GetGravatarUrl(profileUser.Email),
                Stars = _unitOfWork.Stars.GetStarLookup(loggedInUserId)
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
                    .Select(s => new Shelf
                    {
                        Id = s.Id,
                        Items = s.Items.Where(b => !b.IsDeleted).Take(5).ToList(),
                        Title = s.Title,
                        CreatedById = s.CreatedById,
                        CreatedBy = s.CreatedBy,
                        IsPublic = s.IsPublic,
                        CreationDate = s.CreationDate,
                        Slug = s.Slug
                        
                    })
                    .Where(s => (s.IsPublic || s.CreatedById == loggedInUserId) && !s.IsDeleted)
                    .OrderByDescending(b => b.UpdateDate);
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
                viewModel.Shelves = _context.Shelves.Select(s => new Shelf
                    {
                        Id = s.Id,
                        Items = s.Items.Where(b => !b.IsDeleted).Take(5).ToList(),
                        Title = s.Title,
                        CreatedById = s.CreatedById,
                        CreatedBy = s.CreatedBy,
                        IsPublic = s.IsPublic,
                        CreationDate = s.CreationDate,
                        Slug = s.Slug
                    })
                    .Where(s => s.CreatedById == userId && (s.IsPublic || userId == loggedInUserId) && !s.IsDeleted)
                    .OrderByDescending(b => b.UpdateDate);
            }

            return View(viewModel);
        }

        private string GetGravatarUrl(string email)
        {
            return string.Format("https://www.gravatar.com/avatar/{0}?s=300&r=pg&d=mm",
                CryptographyHelper.GetMd5Hash(email.Trim().ToLowerInvariant()));
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
