using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Otomatik.Library.Web.Core.Models;
using Otomatik.Library.Web.Core.ViewModels;
using Otomatik.Library.Web.Data;
using Otomatik.Library.Web.Extensions;
using ErrorViewModel = Otomatik.Library.Web.Models.ErrorViewModel;

namespace Otomatik.Library.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index([FromQuery]string query = null)
        {
            var shelves = _context.Shelves
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
                Shelves = shelves.ToList(),
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

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
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
