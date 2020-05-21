using Microsoft.AspNetCore.Mvc;
using Otomatik.Library.Web.Core.ViewModels;
using Otomatik.Library.Web.Extensions;
using System.Diagnostics;
using System.Linq;
using Otomatik.Library.Web.Core;
using Otomatik.Library.Web.Core.Enums;
using Otomatik.Library.Web.Core.Helpers;
using Otomatik.Library.Web.Models;

namespace Otomatik.Library.Web.Controllers
{
    public class ProfilesController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public ProfilesController(IUnitOfWork unitOfWork)
        {
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
                FollowersCount = _unitOfWork.Followings.CountFollowers(userId),
                FollowingCount = _unitOfWork.Followings.CountFollowings(userId)
            };

            if (!viewModel.IsProfileOwner)
            {
                viewModel.IsFollowing = _unitOfWork.Followings.IsFollowing(userId, loggedInUserId);
            }
            
            var shelves = (tab == ProfileTabs.Stars) ? _unitOfWork.Shelves.GetStarredShelves(userId) : _unitOfWork.Shelves.GetUserShelves(userId);

            if (userId != loggedInUserId)
            {
                shelves = shelves.Where(s => s.IsPublic);
            }

            viewModel.ShelvesCount = shelves.Count();

            return View(viewModel);
        }

        private string GetGravatarUrl(string email)
        {
            return $"https://www.gravatar.com/avatar/{CryptographyHelper.GetMd5Hash(email.Trim().ToLowerInvariant())}?s=300&r=pg&d=mm";
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
