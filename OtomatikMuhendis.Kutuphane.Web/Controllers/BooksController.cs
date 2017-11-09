using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using OtomatikMuhendis.Kutuphane.Web.Data;
using OtomatikMuhendis.Kutuphane.Web.ViewModels;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using OtomatikMuhendis.Kutuphane.Web.Extensions;
using OtomatikMuhendis.Kutuphane.Web.Models;

namespace OtomatikMuhendis.Kutuphane.Web.Controllers
{
    [AutoValidateAntiforgeryToken]
    public class BooksController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BooksController(ApplicationDbContext context)
        {
            _context = context;
        }

        [Authorize]
        public IActionResult Create()
        {
            var userId = User.GetUserId();

            var viewModel = new BookFormViewModel
            {
                Shelves = _context.Shelves.Where(s => s.CreatedById == userId)
                    .Select(p => new SelectListItem
                    {
                        Text = p.Title,
                        Value = p.Id.ToString()
                    }).ToList()
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
                CreationDate = DateTime.Now,
                Title = viewModel.Title,
                CreatedById = userId
            };

            if (viewModel.Shelf > 0)
            {
                book.ShelfId = viewModel.Shelf;
            }
            else
            {
                var shelf = new Shelf
                {
                    CreatedById = userId,
                    CreationDate = DateTime.Now,
                    Title = "Default"
                };

                _context.Shelves.Add(shelf);

                book.Shelf = shelf;
            }

            _context.Books.Add(book);
            _context.SaveChanges();

            return RedirectToAction("Index", "Home");
        }
    }
}