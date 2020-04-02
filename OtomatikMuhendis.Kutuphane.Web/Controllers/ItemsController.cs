using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OtomatikMuhendis.Kutuphane.Web.Core.ViewModels;
using OtomatikMuhendis.Kutuphane.Web.Data;
using OtomatikMuhendis.Kutuphane.Web.Extensions;
using OtomatikMuhendis.Kutuphane.Web.Services;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CloudinaryDotNet;
using OtomatikMuhendis.Kutuphane.Web.Core;
using OtomatikMuhendis.Kutuphane.Web.Core.Enums;
using OtomatikMuhendis.Kutuphane.Web.Services.ApiClients;

namespace OtomatikMuhendis.Kutuphane.Web.Controllers
{
    [AutoValidateAntiforgeryToken]
    [Route("[controller]")]
    public class ItemsController : Controller
    {
        private readonly IApplicationDbContext _context;
        private readonly IBookFinder _bookFinder;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRawgGamesClient _rawgGamesClient;

        public ItemsController(IApplicationDbContext context, IBookFinder bookFinder, IUnitOfWork unitOfWork, IRawgGamesClient rawgGamesClient)
        {
            _context = context;
            _bookFinder = bookFinder;
            _unitOfWork = unitOfWork;
            _rawgGamesClient = rawgGamesClient;
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

            var viewModel = new ItemViewModel()
            {
                Item = item,
                IsShelfOwner = item.Shelf.CreatedById == User.GetUserId()
            };

            switch (item.Type)
            {
                case ItemType.Book:
                    viewModel.BookDetail = _unitOfWork.ItemBookDetails.GetBookDetailByItemId(item.Id);
                    viewModel.CoverImageUrl = viewModel.BookDetail?.ImageLink;
                    break;
                case ItemType.Game:
                    viewModel.GameDetail = await _rawgGamesClient.ReadAsync(item.RawgId.ToString());
                    viewModel.CoverImageUrl = viewModel.GameDetail.Background_image.ToString();
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