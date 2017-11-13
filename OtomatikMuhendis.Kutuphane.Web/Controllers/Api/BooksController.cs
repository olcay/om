using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OtomatikMuhendis.Kutuphane.Web.Data;
using OtomatikMuhendis.Kutuphane.Web.Extensions;

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
                .FirstOrDefault(s => s.Id == bookId
                    && s.CreatedById == userId);

            if (book == null)
            {
                return NotFound();
            }

            book.IsDeleted = true;

            _context.SaveChanges();

            return Ok();
        }
    }
}
