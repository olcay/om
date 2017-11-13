using Microsoft.AspNetCore.Mvc;
using OtomatikMuhendis.Kutuphane.Web.Data;
using OtomatikMuhendis.Kutuphane.Web.Models;
using OtomatikMuhendis.Kutuphane.Web.ViewModels;
using System.Diagnostics;
using System.Linq;

namespace OtomatikMuhendis.Kutuphane.Web.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext _context;

        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var shelves = _context.Shelves.Select(s => new Shelf
            {
                Id = s.Id,
                Books = s.Books.Where(b => !b.IsDeleted).Take(5).ToList(),
                Title = s.Title,
                CreatedById = s.CreatedById,
                CreatedBy = s.CreatedBy,
                IsPublic = s.IsPublic
            })
            .Where(s => !s.IsDeleted && s.IsPublic)
            .OrderByDescending(b => b.UpdateDate);

            var viewModel = new ShelvesViewModel
            {
                Shelves = shelves,
                ShowActions = User.Identity.IsAuthenticated
            };

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
