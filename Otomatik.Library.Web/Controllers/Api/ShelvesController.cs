using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Otomatik.Library.Web.Core.Dtos;
using Otomatik.Library.Web.Extensions;
using System.Linq;
using AutoMapper;
using Otomatik.Library.Web.Core;
using Otomatik.Library.Web.Core.ViewModels;

namespace Otomatik.Library.Web.Controllers.Api
{
    [Produces("application/json")]
    [Route("api/shelves")]
    [AutoValidateAntiforgeryToken]
    public class ShelvesController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private const int PageSize = 20;

        public ShelvesController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [Authorize]
        [HttpGet("/api/shelves")]
        public IActionResult GetPublicShelves([FromQuery] string query, [FromQuery] int page = 1)
        {
            var userId = User.GetUserId();

            var shelves = _unitOfWork.Shelves.GetPublicShelves(query);

            var offset = page > 0 ? (page - 1) * PageSize : 0;

            var paginated = new PaginatedViewModel<ShelfDto>()
            {
                CurrentPage = page,
                Total = shelves.Count(),
                List = _mapper.Map<List<ShelfDto>>(shelves.Skip(offset).Take(PageSize))
            };

            var stars = _unitOfWork.Stars.GetStarLookup(userId);

            if (stars.Any())
            {
                foreach (var shelf in paginated.List)
                {
                    shelf.IsStarred = stars.Contains(shelf.Id);
                }
            }

            paginated.PageCount = (int) Math.Ceiling((decimal) paginated.Total / PageSize);

            return Ok(paginated);
        }

        [Authorize]
        [HttpGet("/api/me/shelves")]
        public IActionResult GetMyShelves(string query, int limit = 0)
        {
            var userId = User.GetUserId();
            var shelves = _unitOfWork.Shelves.GetUserShelves(userId, query, limit);

            var shelfDtos = _mapper.Map<List<ShelfDto>>(shelves);

            return Ok(shelfDtos);
        }

        [HttpGet("/api/{userId}/shelves")]
        public IActionResult GetShelvesByUser([FromRoute] string userId, [FromQuery] int page = 1)
        {
            var loggedInUserId = User.GetUserId();
            var shelves = _unitOfWork.Shelves.GetShelvesByUser(userId);

            if (userId != loggedInUserId)
            {
                shelves = shelves.Where(s => s.IsPublic);
            }

            var offset = page > 0 ? (page - 1) * PageSize : 0;
            
            var paginated = new PaginatedViewModel<ShelfDto>()
            {
                CurrentPage = page,
                Total = shelves.Count(),
                List = _mapper.Map<List<ShelfDto>>(shelves.Skip(offset).Take(PageSize))
            };

            paginated.PageCount = (int)Math.Ceiling((decimal)paginated.Total / PageSize);

            return Ok(paginated);
        }

        [HttpGet("/api/{userId}/starredShelves")]
        public IActionResult GetStarredShelvesByUser([FromRoute] string userId, [FromQuery] int page = 1)
        {
            var shelves = _unitOfWork.Shelves.GetStarredShelves(userId);

            var offset = page > 0 ? (page - 1) * PageSize : 0;

            var paginated = new PaginatedViewModel<ShelfDto>()
            {
                CurrentPage = page,
                Total = shelves.Count(),
                List = _mapper.Map<List<ShelfDto>>(shelves.Skip(offset).Take(PageSize))
            };

            paginated.PageCount = (int)Math.Ceiling((decimal)paginated.Total / PageSize);

            return Ok(paginated);
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

            var shelf = _unitOfWork.Shelves.GetShelf(dto.Id, userId);

            if (shelf == null)
            {
                return NotFound();
            }

            shelf.SetTitle(dto.Title);

            _unitOfWork.Complete();

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

            var shelf = _unitOfWork.Shelves.GetShelf(shelfId, userId);

            if (shelf == null || shelf.IsPublic)
            {
                return NotFound();
            }

            shelf.IsPublic = true;

            _unitOfWork.Complete();

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

            var shelf = _unitOfWork.Shelves.GetShelf(shelfId, userId);

            if (shelf == null || !shelf.IsPublic)
            {
                return NotFound();
            }

            shelf.IsPublic = false;

            _unitOfWork.Complete();

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

            var shelf = _unitOfWork.Shelves.GetShelf(shelfId, userId);

            if (shelf == null || shelf.IsDeleted)
            {
                return NotFound();
            }

            shelf.IsDeleted = true;

            _unitOfWork.Complete();

            return Ok();
        }
    }
}
