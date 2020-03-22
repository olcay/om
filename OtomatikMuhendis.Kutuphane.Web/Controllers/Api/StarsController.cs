using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OtomatikMuhendis.Kutuphane.Web.Core.Dtos;
using OtomatikMuhendis.Kutuphane.Web.Core.Models;
using OtomatikMuhendis.Kutuphane.Web.Data;
using OtomatikMuhendis.Kutuphane.Web.Extensions;
using System.Linq;
using Microsoft.EntityFrameworkCore;

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

            var shelf = _context.Shelves.Include(s => s.CreatedBy).FirstOrDefault(s => s.Id == dto.ShelfId);

            if (shelf == null)
            {
                return NotFound(nameof(shelf));
            }
            
            var star = _context.Stars.FirstOrDefault(a => a.UserId == userId && a.ShelfId == shelf.Id);

            string result;

            if (star == null)
            {
                star = new Star
                {
                    ShelfId = dto.ShelfId,
                    UserId = User.GetUserId()
                };

                _context.Stars.Add(star);

                shelf.Star();

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