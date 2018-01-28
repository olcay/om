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

namespace OtomatikMuhendis.Kutuphane.Web.Controllers
{
    [AutoValidateAntiforgeryToken]
    [Route("[controller]")]
    public class BooksController : Controller
    {
        private readonly IApplicationDbContext _context;
        private readonly IBookFinder _bookFinder;

        public BooksController(IApplicationDbContext context, IBookFinder bookFinder)
        {
            _context = context;
            _bookFinder = bookFinder;
        }

        [HttpGet("{bookId}")]
        public IActionResult Detail(int bookId)
        {
            if (bookId == 0)
            {
                return RedirectToAction("Error", "Home");
            }

            var book = _context.Books
                .Include(b => b.Shelf)
                .FirstOrDefault(b => b.Id == bookId && !b.IsDeleted);

            if (book == null)
            {
                return RedirectToAction("Error", "Home");
            }

            var viewModel = new BookViewModel()
            {
                Book = book
            };

            return View(viewModel);
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
                        ImageLink = volume.VolumeInfo.ImageLinks != null ? volume.VolumeInfo.ImageLinks.Thumbnail : ""
                    });
                }
            }

            return View(model);
        }
    }
}