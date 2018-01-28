using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OtomatikMuhendis.Kutuphane.Web.Core;
using OtomatikMuhendis.Kutuphane.Web.Core.Models;
using OtomatikMuhendis.Kutuphane.Web.Core.ViewModels;
using OtomatikMuhendis.Kutuphane.Web.Extensions;
using OtomatikMuhendis.Kutuphane.Web.Services;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace OtomatikMuhendis.Kutuphane.Web.Controllers.Api
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [Authorize]
    [AutoValidateAntiforgeryToken]
    public class BooksController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IBookFinder _bookFinder;

        public BooksController(IUnitOfWork unitOfWork, IBookFinder bookFinder)
        {
            _unitOfWork = unitOfWork;
            _bookFinder = bookFinder;
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
        
        [HttpPost]
        public async Task<IActionResult> Create(BookFormViewModel viewModel)
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

            var book = new Book
            {
                CreationDate = DateTime.UtcNow,
                Title = viewModel.Title,
                CreatedById = userId
            };

            if (!string.IsNullOrWhiteSpace(viewModel.GBookId))
            {
                var bookDetailFromDb = _unitOfWork.BookDetails.GetBookDetail(viewModel.GBookId);

                if (bookDetailFromDb != null)
                {
                    book.BookDetail = bookDetailFromDb;
                }
                else
                {
                    var volume = await _bookFinder.GetAsync(viewModel.GBookId);

                    if (volume != null)
                    {
                        book.Title = volume.VolumeInfo.Title;

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

                        _unitOfWork.BookDetails.Save(bookDetail);

                        book.BookDetail = bookDetail;

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

            if (viewModel.ShelfId > 0)
            {
                book.ShelfId = viewModel.ShelfId;

                var shelf = _unitOfWork.Shelves.GetShelf(book.ShelfId);

                shelf.UpdateDate = DateTime.UtcNow;
            }
            else
            {
                var shelf = new Shelf(userId, string.Format("{0}'s shelf", User.GetUserName()));
                
                var followers = _unitOfWork.Followings.GetFollowers(userId);

                shelf.Publish(followers);

                _unitOfWork.Shelves.Save(shelf);

                book.Shelf = shelf;
            }

            _unitOfWork.Books.Save(book);
            _unitOfWork.Complete();

            return Ok(book.Id);
        }

    }
}
