using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OtomatikMuhendis.Kutuphane.Web.Data;
using OtomatikMuhendis.Kutuphane.Web.Extensions;
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

            var shelf = _context.Shelves
                .Include(b => b.Books)
                .Include(b => b.CreatedBy)
                .FirstOrDefault(s => s.Id == shelfId);

            if (shelf == null)
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
