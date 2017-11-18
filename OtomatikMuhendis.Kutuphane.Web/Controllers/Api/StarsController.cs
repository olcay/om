using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OtomatikMuhendis.Kutuphane.Web.Data;
using OtomatikMuhendis.Kutuphane.Web.Dtos;
using OtomatikMuhendis.Kutuphane.Web.Extensions;
using OtomatikMuhendis.Kutuphane.Web.Models;

namespace OtomatikMuhendis.Kutuphane.Web.Controllers.Api
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [Authorize]
    [AutoValidateAntiforgeryToken]
    public class StarsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public StarsController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public IActionResult Star(StarDto dto)
        {
            var userId = User.GetUserId();

            var star = _context.Stars.FirstOrDefault(a => a.UserId == userId && a.ShelfId == dto.ShelfId);

            string result;

            if (star == null)
            {
                star = new Star
                {
                    ShelfId = dto.ShelfId,
                    UserId = User.GetUserId()
                };

                _context.Stars.Add(star);

                result = "starred";
            }
            else
            {
                _context.Stars.Remove(star);

                result = "unstarred";
            }

            _context.SaveChanges();

            return Ok(result);
        }
    }
}
