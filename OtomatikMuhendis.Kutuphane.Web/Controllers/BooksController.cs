using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OtomatikMuhendis.Kutuphane.Web.Core.Models;
using OtomatikMuhendis.Kutuphane.Web.Core.ViewModels;
using OtomatikMuhendis.Kutuphane.Web.Data;
using OtomatikMuhendis.Kutuphane.Web.Extensions;
using System;
using System.Linq;

namespace OtomatikMuhendis.Kutuphane.Web.Controllers
{
    [AutoValidateAntiforgeryToken]
    [Route("[controller]")]
    public class BooksController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BooksController(ApplicationDbContext context)
        {
            _context = context;
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
        public IActionResult Create([FromQuery]int shelfId = 0)
        {
            var userId = User.GetUserId();

            var viewModel = new BookFormViewModel
            {
                Shelves = _context.Shelves.Where(s => s.CreatedById == userId)
                    .Select(p => new SelectListItem
                    {
                        Text = p.Title,
                        Value = p.Id.ToString()
                    }).ToList(),
                Shelf = shelfId
            };

            return View(viewModel);
        }

        [Authorize]
        [HttpPost]
        public IActionResult Create(BookFormViewModel viewModel)
        {
            var userId = User.GetUserId();

            if (!ModelState.IsValid)
            {
                viewModel.Shelves = _context.Shelves.Where(s => s.CreatedById == userId)
                    .Select(p => new SelectListItem
                    {
                        Text = p.Title,
                        Value = p.Id.ToString()
                    }).ToList();
                return View(viewModel);
            }

            var book = new Book
            {
                CreationDate = DateTime.UtcNow,
                Title = viewModel.Title,
                CreatedById = userId
            };

            if (viewModel.Shelf > 0)
            {
                book.ShelfId = viewModel.Shelf;

                var shelf = _context.Shelves.FirstOrDefault(s => s.Id == book.ShelfId);

                if (shelf == null)
                {
                    viewModel.Shelves = _context.Shelves.Where(s => s.CreatedById == userId)
                        .Select(p => new SelectListItem
                        {
                            Text = p.Title,
                            Value = p.Id.ToString()
                        }).ToList();
                    return View(viewModel);
                }

                shelf.UpdateDate = DateTime.UtcNow;
            }
            else
            {
                var shelf = new Shelf(userId, string.Format("{0}'s shelf", User.GetUserName()));

                var followers = _context.Followings
                    .Where(f => f.FolloweeId == userId)
                    .Select(f => f.Follower)
                    .ToList();

                shelf.Publish(followers);

                _context.Shelves.Add(shelf);

                book.Shelf = shelf;
            }

            _context.Books.Add(book);
            _context.SaveChanges();

            return RedirectToAction("Detail", "Shelves", new {shelfId = book.ShelfId});
        }
    }
}