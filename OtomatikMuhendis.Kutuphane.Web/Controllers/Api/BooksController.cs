using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OtomatikMuhendis.Kutuphane.Web.Data;
using OtomatikMuhendis.Kutuphane.Web.Extensions;
using System.Linq;

namespace OtomatikMuhendis.Kutuphane.Web.Controllers.Api
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [Authorize]
    [AutoValidateAntiforgeryToken]
    public class BooksController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BooksController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpDelete("{bookId}")]
        public IActionResult Delete([FromRoute]int bookId)
        {
            if (bookId < 0)
            {
                return BadRequest();
            }

            var userId = User.GetUserId();

            var book = _context.Books
                .Include(b => b.Shelf)
                .Single(b => b.Id == bookId
                    && b.CreatedById == userId);

            if (book == null || book.IsDeleted)
            {
                return NotFound();
            }

            var followers = _context.Followings
                .Where(f => f.FolloweeId == userId)
                .Select(f => f.Follower)
                .ToList();

            book.Delete(followers);

            _context.SaveChanges();

            return Ok();
        }
    }
}
