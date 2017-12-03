using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OtomatikMuhendis.Kutuphane.Web.Core.Dtos;
using OtomatikMuhendis.Kutuphane.Web.Core.Models;
using OtomatikMuhendis.Kutuphane.Web.Data;
using OtomatikMuhendis.Kutuphane.Web.Extensions;
using System.Linq;

namespace OtomatikMuhendis.Kutuphane.Web.Controllers.Api
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [Authorize]
    [AutoValidateAntiforgeryToken]
    public class FollowingsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public FollowingsController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public IActionResult Follow(FollowingDto dto)
        {
            var userId = User.GetUserId();

            var following =
                _context.Followings.FirstOrDefault(a => a.FollowerId == userId && a.FolloweeId == dto.FolloweeId);

            string result;

            if (following == null)
            {
                following = new Following
                {
                    FollowerId = userId,
                    FolloweeId = dto.FolloweeId
                };

                _context.Followings.Add(following);

                result = "following";
            }
            else
            {
                _context.Followings.Remove(following);

                result = "notfollowing";
            }

            _context.SaveChanges();

            return Ok(result);
        }
    }
}