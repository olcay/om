﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OtomatikMuhendis.Kutuphane.Web.Extensions;
using OtomatikMuhendis.Kutuphane.Web.Models;
using OtomatikMuhendis.Kutuphane.Web.Persistence;
using OtomatikMuhendis.Kutuphane.Web.ViewModels;

namespace OtomatikMuhendis.Kutuphane.Web.Controllers
{
    [AutoValidateAntiforgeryToken]
    [Route("[controller]")]
    public class ShelvesController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public ShelvesController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet("{shelfId}")]
        public IActionResult Detail(int shelfId)
        {
            if (shelfId == 0)
                return RedirectToAction("Error", "Home");

            var userId = User.GetUserId();

            var shelf = _unitOfWork.Shelves.GetShelf(shelfId);

            if (shelf == null || shelf.IsDeleted || !shelf.IsPublic && shelf.CreatedById != userId)
                return RedirectToAction("Error", "Home");

            var viewModel = new ShelfViewModel
            {
                ShowActions = User.Identity.IsAuthenticated,
                IsShelfOwner = userId == shelf.CreatedById,
                Shelf = shelf
            };

            return View(viewModel);
        }

        [Authorize]
        public IActionResult Create()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        public IActionResult Create(ShelfFormViewModel viewModel)
        {
            var userId = User.GetUserId();

            if (!ModelState.IsValid)
                return View();

            var shelf = new Shelf(userId, viewModel.Title);

            if (viewModel.IsPublic)
                shelf.Publish(_unitOfWork.Followings.GetFollowers(userId));

            _unitOfWork.Shelves.Save(shelf);
            _unitOfWork.Complete();

            return RedirectToAction("Detail", "Shelves", new {shelfId = shelf.Id});
        }
    }
}