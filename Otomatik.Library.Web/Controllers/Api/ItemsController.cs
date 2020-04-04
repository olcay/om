using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Otomatik.Library.Web.Core;
using Otomatik.Library.Web.Core.Models;
using Otomatik.Library.Web.Core.ViewModels;
using Otomatik.Library.Web.Extensions;
using Otomatik.Library.Web.Services;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Otomatik.Library.Web.Core.Dtos;
using Otomatik.Library.Web.Core.Enums;
using Otomatik.Library.Web.Core.Helpers;
using Otomatik.Library.Web.Services.ApiClients;

namespace Otomatik.Library.Web.Controllers.Api
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [Authorize]
    [AutoValidateAntiforgeryToken]
    public class ItemsController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IBookFinder _bookFinder;
        private readonly RawgGamesClient _gamesClient;

        public ItemsController(IUnitOfWork unitOfWork, IBookFinder bookFinder, RawgGamesClient gamesClient)
        {
            _unitOfWork = unitOfWork;
            _bookFinder = bookFinder;
            _gamesClient = gamesClient;
        }

        [HttpDelete("{id}")]
        public IActionResult Delete([FromRoute]int id)
        {
            if (id < 0)
            {
                return BadRequest();
            }

            var userId = User.GetUserId();

            var book = _unitOfWork.Items.GetItem(id);

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
        
        [HttpPost]
        public async Task<IActionResult> Create(BookFormViewModel viewModel, CancellationToken cancellationToken)
        {
            if (viewModel == null)
            {
                return BadRequest();
            }

            if (string.IsNullOrWhiteSpace(viewModel.Title)
                && string.IsNullOrWhiteSpace(viewModel.GBookId)
                && viewModel.ShelfId == 0)
            {
                return BadRequest();
            }

            var userId = User.GetUserId();

            var item = new Item
            {
                CreationDate = DateTime.UtcNow,
                Title = viewModel.Title,
                CreatedById = userId
            };

            if (!string.IsNullOrWhiteSpace(viewModel.GBookId))
            {
                item.Type = ItemType.Book;
                var bookDetailFromDb = _unitOfWork.BookDetails.GetBookDetail(viewModel.GBookId);

                if (bookDetailFromDb != null)
                {
                    if (viewModel.ShelfId > 0)
                    {
                        var itemFromDb = _unitOfWork.ItemBookDetails.GetItemByBookDetailId(bookDetailFromDb.Id, userId);

                        if (itemFromDb != null)
                        {
                            if (itemFromDb.ShelfId == viewModel.ShelfId)
                            {
                                if (itemFromDb.IsDeleted)
                                {
                                    itemFromDb.Reactivate();

                                    _unitOfWork.Complete();

                                    return Ok(itemFromDb.Id);
                                }

                                return BadRequest("The book is already in the specified shelf.");
                            }
                        }
                    }

                    _unitOfWork.ItemBookDetails.Save(new ItemBookDetail(item, bookDetailFromDb));
                }
                else
                {
                    var volume = await _bookFinder.GetAsync(viewModel.GBookId);

                    if (volume != null)
                    {
                        item.Title = volume.VolumeInfo.Title;

                        var bookDetail = new BookDetail
                        {
                            Subtitle = volume.VolumeInfo.Subtitle != null && volume.VolumeInfo.Subtitle.Length > 255
                                ? volume.VolumeInfo.Subtitle.Substring(0, 255)
                                : volume.VolumeInfo.Subtitle,
                            Description =
                                volume.VolumeInfo.Description != null && volume.VolumeInfo.Description.Length > 1000
                                    ? volume.VolumeInfo.Description.Substring(0, 1000)
                                    : volume.VolumeInfo.Description,
                            GoogleBookId = volume.Id,
                            PublishedYear =
                                volume.VolumeInfo.PublishedDate != null && volume.VolumeInfo.PublishedDate.Length >= 4
                                    ? Convert.ToInt16(volume.VolumeInfo.PublishedDate.Substring(0, 4))
                                    : (short?) null,
                            Publisher = volume.VolumeInfo.Publisher,
                            ImageLink = volume.VolumeInfo.ImageLinks != null
                                ? volume.VolumeInfo.ImageLinks.Thumbnail
                                : ""
                        };

                        _unitOfWork.ItemBookDetails.Save(new ItemBookDetail(item, bookDetail));

                        if (volume.VolumeInfo.Authors != null && volume.VolumeInfo.Authors.Any())
                        {
                            foreach (var authorName in volume.VolumeInfo.Authors)
                            {
                                var authorFromDb = _unitOfWork.Authors.GetAuthor(authorName);

                                Author author;

                                if (authorFromDb == null)
                                {
                                    author = new Author()
                                    {
                                        Name = authorName,
                                        CreationDate = DateTime.UtcNow
                                    };
                                }
                                else
                                {
                                    author = authorFromDb;
                                }

                                _unitOfWork.BookAuthors.Save(new BookAuthor()
                                {
                                    BookDetail = bookDetail,
                                    Author = author
                                });
                            }
                        }
                    }
                }
            }

            if (!string.IsNullOrWhiteSpace(viewModel.GameId))
            {
                if (viewModel.ShelfId > 0 && int.TryParse(viewModel.GameId, out var gameId))
                {
                    var itemFromDb = _unitOfWork.Items.GetItemByRawgId(gameId, viewModel.ShelfId);

                    if (itemFromDb != null)
                    {
                        if (itemFromDb.IsDeleted)
                        {
                            itemFromDb.Reactivate();

                            _unitOfWork.Complete();

                            return Ok(itemFromDb.Id);
                        }

                        return BadRequest("The game is already in the specified shelf.");
                    }
                }

                var game = await _gamesClient.ReadAsync(viewModel.GameId, cancellationToken);

                if (game != null)
                {
                    item.RawgId = game.Id;
                    item.Title = game.Name;
                    item.Slug = game.Slug;
                    item.Type = ItemType.Game;
                }
            }

            if (viewModel.ShelfId > 0)
            {
                item.ShelfId = viewModel.ShelfId;

                var shelf = _unitOfWork.Shelves.GetShelf(item.ShelfId);

                shelf.UpdateDate = DateTime.UtcNow;
            }
            else
            {
                var shelf = new Shelf(userId, string.Format("{0}'s shelf", User.GetUserName()));
                
                var followers = _unitOfWork.Followings.GetFollowers(userId);

                shelf.Publish(followers);

                _unitOfWork.Shelves.Save(shelf);

                item.Shelf = shelf;
            }

            _unitOfWork.Items.Save(item);
            _unitOfWork.Complete();

            return Ok(item.Id);
        }

        [HttpPost("updateTitle")]
        public IActionResult Edit(ItemDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var item = _unitOfWork.Items.GetItem(dto.Id);
            
            if (item == null)
            {
                return NotFound();
            }

            if (item.CreatedById != User.GetUserId())
            {
                return Forbid();
            }

            item.Title = dto.Title;
            item.Slug = SlugGenerator.GenerateSlug(dto.Title);

            _unitOfWork.Items.Save(item);
            _unitOfWork.Complete();

            return Ok();
        }

        [HttpPut("{id}/cover")]
        public ActionResult UploadCover([FromForm] IFormFile file, [FromRoute] int id)
        {
            if (file == null || id < 1)
            {
                return BadRequest(nameof(file));
            }

            var userId = User.GetUserId();

            var item = _unitOfWork.Items.GetItem(id);

            if (item == null)
            {
                return NotFound(nameof(item));
            }

            if (item.CreatedById != userId)
            {
                return Forbid();
            }

            var cloudinary = new Cloudinary();

            var imageUploadResult = cloudinary.Upload(new ImageUploadParams()
            {
                Folder = "covers",
                File = new FileDescription(file.Name, file.OpenReadStream())
            });

            item.CoverId = imageUploadResult.PublicId;

            _unitOfWork.Items.Save(item);
            _unitOfWork.Complete();

            return Ok(imageUploadResult.SecureUri);
        }
    }
}
