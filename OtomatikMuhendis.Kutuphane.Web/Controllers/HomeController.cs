using Microsoft.AspNetCore.Mvc;
using OtomatikMuhendis.Kutuphane.Web.Models;
using System.Diagnostics;

namespace OtomatikMuhendis.Kutuphane.Web.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
