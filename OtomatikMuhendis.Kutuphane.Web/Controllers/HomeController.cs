using Microsoft.AspNetCore.Mvc;
using OtomatikMuhendis.Kutuphane.Web.Core.Models;
using OtomatikMuhendis.Kutuphane.Web.Core.ViewModels;
using OtomatikMuhendis.Kutuphane.Web.Data;
using OtomatikMuhendis.Kutuphane.Web.Extensions;
using System.Diagnostics;
using System.Linq;

namespace OtomatikMuhendis.Kutuphane.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index([FromQuery]string query = null)
        {
            var shelves = _context.Shelves.Select(s => new Shelf
            {
                Id = s.Id,
                Items = s.Items.Where(b => !b.IsDeleted).Take(5).ToList(),
                Title = s.Title,
                CreatedById = s.CreatedById,
                CreatedBy = s.CreatedBy,
                IsPublic = s.IsPublic,
                CreationDate = s.CreationDate
            })
            .Where(s => !s.IsDeleted && s.IsPublic && s.Items.Any());

            if (!string.IsNullOrWhiteSpace(query))
            {
                query = query.ToLowerInvariant();

                shelves = shelves.Where(s => 
                s.Title.ToLowerInvariant().Contains(query) ||
                s.CreatedBy.Name.ToLowerInvariant().Contains(query) ||
                s.Items.Any(b => b.Title.ToLowerInvariant().Contains(query)));
            }

            shelves = shelves.OrderByDescending(b => b.UpdateDate);

            var viewModel = new HomeViewModel
            {
                Shelves = shelves,
                ShowActions = User.Identity.IsAuthenticated,
                Query = query
            };

            if (viewModel.ShowActions)
            {
                var userId = User.GetUserId();
                var userShelves = _context.Shelves.Where(s => !s.IsDeleted && s.CreatedById == userId).Take(10).ToList();
                viewModel.UserShelves = userShelves;
                
                viewModel.Stars = _context.Stars.Where(s => s.UserId == userId).ToLookup(s => s.ShelfId);
            }

            return View(viewModel);
        }

        public IActionResult About()
        {
            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        
        public JsonResult IsUserExists(string name)
        { 
            return Json(!_context.Users.Any(x => x.Name == name));
        }
    }
}
