using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using OtomatikMuhendis.Kutuphane.Web.Data;
using OtomatikMuhendis.Kutuphane.Web.ViewModels;
using System.Linq;

namespace OtomatikMuhendis.Kutuphane.Web.Controllers
{
    public class BooksController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BooksController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Create()
        {
            var viewModel = new BookFormViewModel
            {
                Shelves = _context.Shelves.Select(p => new SelectListItem
                {
                    Text = p.Title,
                    Value = p.Id.ToString()
                }).ToList()
            };

            return View(viewModel);
        }
    }
}