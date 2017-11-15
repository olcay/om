using Microsoft.AspNetCore.Mvc;
using OtomatikMuhendis.Kutuphane.Web.Data;
using OtomatikMuhendis.Kutuphane.Web.Extensions;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using OtomatikMuhendis.Kutuphane.Web.Dtos;

namespace OtomatikMuhendis.Kutuphane.Web.Controllers.Api
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [Authorize]
    [AutoValidateAntiforgeryToken]
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

        [HttpPatch("{shelfId}/public")]
        public IActionResult MakePublic([FromRoute]int shelfId)
        {
            if (shelfId < 0)
            {
                return BadRequest();
            }

            var userId = User.GetUserId();

            var shelf = _context.Shelves
                .FirstOrDefault(s => s.Id == shelfId
                                     && s.CreatedById == userId);

            if (shelf == null || shelf.IsPublic)
            {
                return NotFound();
            }

            shelf.IsPublic = true;

            _context.SaveChanges();

            return Ok();
        }

        [HttpPatch("{shelfId}/private")]
        public IActionResult MakePrivate([FromRoute]int shelfId)
        {
            if (shelfId < 0)
            {
                return BadRequest();
            }

            var userId = User.GetUserId();

            var shelf = _context.Shelves
                .FirstOrDefault(s => s.Id == shelfId
                                     && s.CreatedById == userId);

            if (shelf == null || !shelf.IsPublic)
            {
                return NotFound();
            }

            shelf.IsPublic = false;

            _context.SaveChanges();

            return Ok();
        }

        [HttpDelete("{shelfId}")]
        public IActionResult Delete([FromRoute]int shelfId)
        {
            if (shelfId < 0)
            {
                return BadRequest();
            }

            var userId = User.GetUserId();

            var shelf = _context.Shelves
                .FirstOrDefault(s => s.Id == shelfId
                                     && s.CreatedById == userId);

            if (shelf == null || shelf.IsDeleted)
            {
                return NotFound();
            }

            shelf.IsDeleted = true;

            _context.SaveChanges();

            return Ok();
        }
    }
}
