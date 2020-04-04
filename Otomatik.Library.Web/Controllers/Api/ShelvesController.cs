using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Otomatik.Library.Web.Core.Dtos;
using Otomatik.Library.Web.Data;
using Otomatik.Library.Web.Extensions;
using System.Linq;
using Otomatik.Library.Web.Core;
using Otomatik.Library.Web.Core.Helpers;

namespace Otomatik.Library.Web.Controllers.Api
{
    [Produces("application/json")]
    [Route("api/shelves")]
    [AutoValidateAntiforgeryToken]
    public class ShelvesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IUnitOfWork _unitOfWork;

        public ShelvesController(ApplicationDbContext context, IUnitOfWork unitOfWork)
        {
            _context = context;
            _unitOfWork = unitOfWork;
        }

        [Authorize]
        [HttpGet("/api/me/shelves")]
        public IActionResult GetShelvesByUser(string query, int limit = 0)
        {
            var userId = User.GetUserId();
            var shelves = _unitOfWork.Shelves.GetUserShelves(userId, query, limit);

            return Ok(shelves);
        }

        [HttpGet]
        public IActionResult Get(string query)
        {
            var shelves = _unitOfWork.Shelves.GetPublicShelves(query);

            return Ok(shelves);
        }

        [Authorize]
        [HttpPost]
        public IActionResult Edit(ShelfDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var userId = User.GetUserId();

            var shelf = _context.Shelves
                .FirstOrDefault(s => s.Id == dto.Id
                    && s.CreatedById == userId);

            if (shelf == null)
            {
                return NotFound();
            }

            shelf.Title = dto.Title;
            shelf.Slug = SlugGenerator.GenerateSlug(dto.Title);

            _context.SaveChanges();

            return Ok();
        }

        [Authorize]
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

        [Authorize]
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

        [Authorize]
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
