﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Otomatik.Library.Web.Core.ViewModels;
using Otomatik.Library.Web.Data;
using Otomatik.Library.Web.Extensions;
using Otomatik.Library.Web.Services;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CloudinaryDotNet;
using Otomatik.Library.Web.Core;
using Otomatik.Library.Web.Core.Enums;
using Otomatik.Library.Web.Services.ApiClients;

namespace Otomatik.Library.Web.Controllers
{
    [AutoValidateAntiforgeryToken]
    [Route("[controller]")]
    public class ItemsController : Controller
    {
        private readonly IApplicationDbContext _context;
        private readonly IBookFinder _bookFinder;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRawgGamesClient _rawgGamesClient;
        private readonly IMapper _mapper;

        public ItemsController(IApplicationDbContext context, IBookFinder bookFinder, IUnitOfWork unitOfWork, IRawgGamesClient rawgGamesClient, IMapper mapper)
        {
            _context = context;
            _bookFinder = bookFinder;
            _unitOfWork = unitOfWork;
            _rawgGamesClient = rawgGamesClient;
            _mapper = mapper;
        }

        [HttpGet("/shelves/{shelfId}/items/{itemId}/{slug?}")]
        public async Task<IActionResult> Detail(int shelfId, int itemId, string slug = "")
        {
            if (shelfId <= 0 || itemId <= 0)
            {
                return RedirectToAction("Error", "Home");
            }

            var userId = User.GetUserId();

            var shelf = _unitOfWork.Shelves.GetShelf(shelfId);

            if (shelf == null
                || shelf.IsDeleted
                || !shelf.IsPublic && shelf.CreatedById != userId)
                return RedirectToAction("Error", "Home");

            var item = _unitOfWork.Items.GetItem(itemId);

            if (item == null
                || item.IsDeleted
                || (!string.IsNullOrEmpty(slug) && item.Slug != slug))
            {
                return RedirectToAction("Error", "Home");
            }

            var viewModel = _mapper.Map<ItemViewModel>(item);
            viewModel.IsShelfOwner = item.Shelf.CreatedById == User.GetUserId();

            switch (item.Type)
            {
                case ItemType.Book:
                    viewModel.BookDetail = _unitOfWork.ItemBookDetails.GetBookDetailByItemId(item.Id);
                    viewModel.CoverImageUrl = viewModel.BookDetail?.ImageLink;
                    break;
                case ItemType.Game:
                    var game = await _rawgGamesClient.ReadAsync(item.RawgId.ToString());
                    viewModel.GameDetail = new GameViewModel()
                    {
                        Id = game.Id.Value,
                        Title = game.Name,
                        TitleOriginal = game.Name_original,
                        Description = game.Description,
                        ImageLink = game.Background_image.ToString(),
                        Released = game.Released
                    };
                    viewModel.CoverImageUrl = viewModel.GameDetail.ImageLink;
                    break;
            }

            if (!string.IsNullOrEmpty(item.CoverId))
            {
                var cloudinary = new Cloudinary();

                var coverImage = cloudinary.GetResource(item.CoverId);

                if (coverImage != null)
                {
                    viewModel.CoverImageUrl = coverImage.SecureUrl;
                }
            }

            return View(viewModel);
        }

        [Authorize]
        [HttpGet("searchGames")]
        public async Task<IActionResult> SearchGames([FromQuery]string query = null, [FromQuery]int shelfId = 0)
        {
            var model = new GameSearchViewModel()
            {
                Search = query,
                ShelfId = shelfId,
                GameList = new List<GameViewModel>()
            };

            var userId = User.GetUserId();

            model.UserShelves = _context.Shelves
                .Where(s => !s.IsDeleted && s.CreatedById == userId)
                .Select(p => new SelectListItem() { Text = p.Title, Value = p.Id.ToString() })
                .ToList();

            if (string.IsNullOrWhiteSpace(query))
            {
                return View(model);
            }

            var games = await _rawgGamesClient.ListAsync(null, null, query, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null);

            if (games != null && games.Results.Any())
            {
                foreach (var game in games.Results)
                {
                    model.GameList.Add(new GameViewModel()
                    {
                        Id = game.Id.Value,
                        ImageLink = game.Background_image.ToString(),
                        Title = game.Name
                    });
                }
            }

            return View(model);
        }

        [Authorize]
        [HttpGet("search")]
        public async Task<IActionResult> Search([FromQuery]string query = null, [FromQuery]int shelfId = 0)
        {
            var model = new BookSearchViewModel()
            {
                BookList = new List<BookDetailViewModel>(),
                Search = query,
                ShelfId = shelfId
            };

            var userId = User.GetUserId();

            model.UserShelves = _context.Shelves
                .Where(s => !s.IsDeleted && s.CreatedById == userId)
                .Select(p => new SelectListItem() { Text = p.Title, Value = p.Id.ToString() })
                .ToList();

            if (string.IsNullOrWhiteSpace(query))
            {
                return View(model);
            }

            var volumeList = await _bookFinder.Search(query);

            if (volumeList != null)
            {
                foreach (var volume in volumeList)
                {
                    model.BookList.Add(new BookDetailViewModel()
                    {
                        Title = volume.VolumeInfo.Title,
                        Subtitle = volume.VolumeInfo.Subtitle,
                        Description = volume.VolumeInfo.Description,
                        GoogleBookId = volume.Id,
                        PublishedYear = volume.VolumeInfo.PublishedDate,
                        PublisherName = volume.VolumeInfo.Publisher,
                        Authors = volume.VolumeInfo.Authors != null ? string.Join(", ", volume.VolumeInfo.Authors) : "",
                        ImageLink = volume.VolumeInfo.ImageLinks != null ? volume.VolumeInfo.ImageLinks.Thumbnail : "/images/book.jpg"
                    });
                }
            }

            return View(model);
        }
    }
}