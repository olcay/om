using Microsoft.AspNetCore.Mvc;
using OtomatikMuhendis.Kutuphane.Web.Data;
using OtomatikMuhendis.Kutuphane.Web.Extensions;
using OtomatikMuhendis.Kutuphane.Web.Models;
using OtomatikMuhendis.Kutuphane.Web.ViewModels;
using System.Linq;

namespace OtomatikMuhendis.Kutuphane.Web.Controllers
{
    [AutoValidateAntiforgeryToken]
    [Route("[controller]")]
    public class ShelvesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ShelvesController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet("{shelfId}")]
        public IActionResult Detail(int shelfId)
        {
            if (shelfId == 0)
            {
                return RedirectToAction("Error", "Home");
            }

            var userId = User.GetUserId();

            var shelf = _context.Shelves.Select(s => new Shelf
            {
                Id = s.Id,
                Books = s.Books.Where(b => !b.IsDeleted).ToList(),
                Title = s.Title,
                CreatedById = s.CreatedById,
                CreatedBy = s.CreatedBy,
                IsPublic = s.IsPublic
            }).FirstOrDefault(s => s.Id == shelfId
                                   && !s.IsDeleted);

            if (shelf == null || shelf.IsDeleted || (!shelf.IsPublic && shelf.CreatedById != userId))
            {
                return RedirectToAction("Error", "Home");
            }

            var viewModel = new ShelfViewModel()
            {
                ShowActions = User.Identity.IsAuthenticated,
                IsShelfOwner = userId == shelf.CreatedById,
                Shelf = shelf
            };

            return View(viewModel);
        }
    }
}
