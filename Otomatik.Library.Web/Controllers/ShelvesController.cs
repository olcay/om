using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using CloudinaryDotNet;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Otomatik.Library.Web.Core;
using Otomatik.Library.Web.Core.Enums;
using Otomatik.Library.Web.Core.Models;
using Otomatik.Library.Web.Core.ViewModels;
using Otomatik.Library.Web.Extensions;
using Otomatik.Library.Web.Services.ApiClients;

namespace Otomatik.Library.Web.Controllers
{
    [AutoValidateAntiforgeryToken]
    [Route("shelves")]
    public class ShelvesController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRawgGamesClient _rawgGamesClient;
        private readonly IMapper _mapper;

        public ShelvesController(IUnitOfWork unitOfWork, IRawgGamesClient rawgGamesClient, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _rawgGamesClient = rawgGamesClient;
            _mapper = mapper;
        }

        [HttpGet("{shelfId}/{slug?}")]
        public async Task<IActionResult> Detail(int shelfId, string slug = "")
        {
            if (shelfId == 0)
                return RedirectToAction("Error", "Home");

            var userId = User.GetUserId();

            var shelf = _unitOfWork.Shelves.GetShelf(shelfId);

            if (shelf == null
                || shelf.IsDeleted
                || !shelf.IsPublic && shelf.CreatedById != userId
                || (!string.IsNullOrEmpty(slug) && shelf.Slug != slug))
                return RedirectToAction("Error", "Home");

            var viewModel = _mapper.Map<ShelfViewModel>(shelf);

            viewModel.ShowActions = User.Identity.IsAuthenticated;
            viewModel.IsShelfOwner = userId == shelf.CreatedById;
            viewModel.Items = new List<ItemViewModel>();

            foreach (var item in shelf.Items)
            {
                var itemViewModel = _mapper.Map<ItemViewModel>(item);

                if (!string.IsNullOrEmpty(item.CoverId))
                {
                    itemViewModel.CoverImageUrl = new Cloudinary().GetResource(item.CoverId)?.SecureUrl;
                }
                else
                {
                    switch (item.Type)
                    {
                        case ItemType.Book:
                            itemViewModel.CoverImageUrl = _unitOfWork.ItemBookDetails.GetBookDetailByItemId(item.Id)?.ImageLink;
                            break;
                        case ItemType.Game:
                            itemViewModel.CoverImageUrl = (await _rawgGamesClient.ReadAsync(item.RawgId.ToString()))?.Background_image.ToString();
                            break;
                    }
                }

                viewModel.Items.Add(itemViewModel);
            }

            return View(viewModel);
        }

        [Authorize]
        public IActionResult Create()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        public IActionResult Create(ShelfFormViewModel viewModel)
        {
            var userId = User.GetUserId();

            if (!ModelState.IsValid)
                return View();

            var shelf = new Shelf(userId, viewModel.Title);

            if (viewModel.IsPublic)
                shelf.Publish(_unitOfWork.Followings.GetFollowers(userId));

            _unitOfWork.Shelves.Save(shelf);
            _unitOfWork.Complete();

            return RedirectToAction("Detail", "Shelves", new { shelfId = shelf.Id });
        }
    }
}