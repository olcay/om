using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using OtomatikMuhendis.Kutuphane.Tests.Extensions;
using OtomatikMuhendis.Kutuphane.Web.Controllers.Api;
using OtomatikMuhendis.Kutuphane.Web.Core;
using OtomatikMuhendis.Kutuphane.Web.Core.Models;
using OtomatikMuhendis.Kutuphane.Web.Core.Repositories;
using OtomatikMuhendis.Kutuphane.Web.Services;
using Xunit;

namespace OtomatikMuhendis.Kutuphane.Tests.Controllers.Api
{
    public class ItemsControllerTests
    {
        private ItemsController _controller;
        private Mock<IItemRepository> _mockBookRepository;
        private string _userId;
        
        public ItemsControllerTests()
        {
            _mockBookRepository = new Mock<IItemRepository>();
            var mockFollowingRepository = new Mock<IFollowingRepository>();

            var mockUoW = new Mock<IUnitOfWork>();
            mockUoW.SetupGet(u => u.Items).Returns(_mockBookRepository.Object);
            mockUoW.SetupGet(u => u.Followings).Returns(mockFollowingRepository.Object);

            var mockBookFinder = new Mock<IBookFinder>();
            
            _controller = new ItemsController(mockUoW.Object, mockBookFinder.Object);
            _userId = "1";
            _controller.MockCurrentUser(_userId, "user1@domain.com");
        }

        [Fact]
        public void Delete_NoBookWithGivenIdExists_ShouldReturnNotFound()
        {
            var result = _controller.Delete(1);

            result.Should().BeOfType<NotFoundResult>();
        }

        [Fact]
        public void Delete_BookIsDeleted_ShouldReturnNotFound()
        {
            //Arrange
            var book = new Item();
            book.Delete();
            _mockBookRepository.Setup(r => r.GetItem(1)).Returns(book);

            //Act
            var result = _controller.Delete(1);
            
            //Assert
            result.Should().BeOfType<NotFoundResult>();

        }

        [Fact]
        public void Delete_UserDeletingAnotherUsersBook_ShouldReturnUnauthorized()
        {
            //Arrange
            var book = new Item {CreatedById = _userId + "-"};
            _mockBookRepository.Setup(r => r.GetItem(1)).Returns(book);

            //Act
            var result = _controller.Delete(1);

            //Assert
            result.Should().BeOfType<UnauthorizedResult>();
        }

        [Fact]
        public void Delete_ValidRequest_ShouldReturnOK()
        {
            //Arrange
            var book = new Item { CreatedById = _userId };
            _mockBookRepository.Setup(r => r.GetItem(1)).Returns(book);

            //Act
            var result = _controller.Delete(1);

            //Assert
            result.Should().BeOfType<OkResult>();
        }
    }
}
