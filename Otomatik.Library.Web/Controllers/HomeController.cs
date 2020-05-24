using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Otomatik.Library.Web.Core;
using ErrorViewModel = Otomatik.Library.Web.Models.ErrorViewModel;

namespace Otomatik.Library.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public HomeController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            return User.Identity.IsAuthenticated ? View("AuthenticatedIndex") : View();
        }

        public IActionResult About()
        {
            return View();
        }

        public IActionResult ToDo()
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
