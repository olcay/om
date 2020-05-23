using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Otomatik.Library.Web.Core.Dtos;
using Otomatik.Library.Web.Core.Models;
using Otomatik.Library.Web.Extensions;
using AutoMapper;
using Otomatik.Library.Web.Core;

namespace Otomatik.Library.Web.Controllers.Api
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [AutoValidateAntiforgeryToken]
    public class FollowingsController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public FollowingsController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [Authorize]
        [HttpPost]
        public IActionResult Follow(FollowingDto dto)
        {
            var userId = User.GetUserId();

            var following = _unitOfWork.Followings.GetFollowing(dto.FolloweeId, userId);

            string result;

            if (following == null)
            {
                following = new Following
                {
                    FollowerId = userId,
                    FolloweeId = dto.FolloweeId
                };

                _unitOfWork.Followings.Save(following);

                result = "following";
            }
            else
            {
                _unitOfWork.Followings.Delete(following);

                result = "notfollowing";
            }

            _unitOfWork.Complete();

            return Ok(result);
        }
        
        [HttpGet("/api/{userId}/followings")]
        public IActionResult GetFollowings([FromRoute] string userId)
        {
            var followings = _unitOfWork.Followings.GetFollowings(userId);

            var users = _mapper.Map<List<UserDto>>(followings);

            return Ok(users);
        }

        [HttpGet("/api/{userId}/followers")]
        public IActionResult GetFollowers([FromRoute] string userId)
        {
            var followers = _unitOfWork.Followings.GetFollowers(userId);

            var users = _mapper.Map<List<UserDto>>(followers);

            return Ok(users);
        }
    }
}