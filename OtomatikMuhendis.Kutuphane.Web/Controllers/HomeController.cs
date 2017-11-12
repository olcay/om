using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OtomatikMuhendis.Kutuphane.Web.Data;
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
            var shelves = _context.Shelves
                .Include(b => b.Books)
                .Include(b => b.CreatedBy)
                .OrderByDescending(b => b.CreationDate);

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
