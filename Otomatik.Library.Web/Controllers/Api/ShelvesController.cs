using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Otomatik.Library.Web.Core.Dtos;
using Otomatik.Library.Web.Data;
using Otomatik.Library.Web.Extensions;
using System.Linq;
using AutoMapper;
using Otomatik.Library.Web.Core;
using Otomatik.Library.Web.Core.Helpers;
using Otomatik.Library.Web.Core.ViewModels;

namespace Otomatik.Library.Web.Controllers.Api
{
    [Produces("application/json")]
    [Route("api/shelves")]
    [AutoValidateAntiforgeryToken]
    public class ShelvesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private const int PageSize = 20;

        public ShelvesController(ApplicationDbContext context, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _context = context;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
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

        [HttpGet]
        public IActionResult Get(string query)
        {
            var shelves = _unitOfWork.Shelves.GetPublicShelves(query);

            var shelfDtos = _mapper.Map<List<ShelfDto>>(shelves);

            return Ok(shelfDtos);
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
