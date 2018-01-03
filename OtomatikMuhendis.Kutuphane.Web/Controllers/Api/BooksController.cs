using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OtomatikMuhendis.Kutuphane.Web.Core;
using OtomatikMuhendis.Kutuphane.Web.Extensions;

namespace OtomatikMuhendis.Kutuphane.Web.Controllers.Api
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [Authorize]
    [AutoValidateAntiforgeryToken]
    public class BooksController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public BooksController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpDelete("{bookId}")]
        public IActionResult Delete([FromRoute]int bookId)
        {
            if (bookId < 0)
            {
                return BadRequest();
            }

            var userId = User.GetUserId();

            var book = _unitOfWork.Books.GetBook(bookId);

            if (book == null || book.IsDeleted)
            {
                return NotFound();
            }

            if (book.CreatedById != userId)
            {
                return Unauthorized();
            }
            
            book.Delete();

            var followers = _unitOfWork.Followings.GetFollowers(userId);

            if (followers != null && followers.Any())
            {
                book.Notify(followers);
            }

            _unitOfWork.Complete();

            return Ok();
        }
    }
}
