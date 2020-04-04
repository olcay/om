using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Otomatik.Library.Web.Core;
using Otomatik.Library.Web.Core.ViewModels;
using Otomatik.Library.Web.Extensions;
using ErrorViewModel = Otomatik.Library.Web.Models.ErrorViewModel;

namespace Otomatik.Library.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public HomeController(ILogger<HomeController> logger, IMapper mapper, IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index([FromQuery]string query = null)
        {
            var shelves = _unitOfWork.Shelves.GetPublicShelves(query);
            
            var viewModel = new HomeViewModel
            {
                Shelves = _mapper.Map<List<ShelfViewModel>>(shelves),
                ShowActions = User.Identity.IsAuthenticated,
                Query = query
            };

            if (viewModel.ShowActions)
            {
                var userId = User.GetUserId();
                viewModel.UserShelves = _unitOfWork.Shelves.GetUserShelves(userId);

                viewModel.Stars = _unitOfWork.Stars.GetStarLookup(userId);

                foreach (var shelf in viewModel.Shelves)
                {
                    shelf.ShowActions = true;
                    shelf.IsStarred = viewModel.Stars != null && viewModel.Stars.Contains(shelf.Id);
                }
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

        public JsonResult IsUserExists([FromQuery(Name = "Input.Name")]string name)
        {
            return Json(_unitOfWork.Users.GetByUsername(name) == null);
        }
    }
}
