using Microsoft.AspNetCore.Mvc;
using OtomatikMuhendis.Kutuphane.Web.Data;
using OtomatikMuhendis.Kutuphane.Web.Extensions;
using System.Linq;
using OtomatikMuhendis.Kutuphane.Web.Dtos;

namespace OtomatikMuhendis.Kutuphane.Web.Controllers.Api
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class ShelvesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ShelvesController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public IActionResult Edit(ShelfDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var userId = User.GetUserId();

            var shelf = _context.Shelves
                .FirstOrDefault(s => s.Id == dto.ShelfId
                    && s.CreatedById == userId);

            if (shelf == null)
            {
                return NotFound();
            }

            shelf.Title = dto.Title;

            _context.SaveChanges();

            return Ok();
        }
    }
}
