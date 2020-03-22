using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OtomatikMuhendis.Kutuphane.Web.Core.Dtos;
using OtomatikMuhendis.Kutuphane.Web.Core.Models;
using OtomatikMuhendis.Kutuphane.Web.Data;
using OtomatikMuhendis.Kutuphane.Web.Extensions;
using System.Collections.Generic;
using System.Linq;

namespace OtomatikMuhendis.Kutuphane.Web.Controllers.Api
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [Authorize]
    [AutoValidateAntiforgeryToken]
    public class NotificationsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public NotificationsController(ApplicationDbContext context,
            IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public IEnumerable<UserNotificationDto> GetNewNotifications()
        {
            var userId = User.GetUserId();
            var notifications = _context.UserNotifications
                .Where(un => un.UserId == userId)
                .Include(n => n.Notification.Item)
                .Include(n => n.Notification.Shelf.CreatedBy)
                .OrderByDescending(n => n.Notification.DateTime)
                .Take(10)
                .ToList();

            return notifications.Select(_mapper.Map<UserNotification, UserNotificationDto>);
        }

        [HttpPost]
        public IActionResult MarkAsRead()
        {
            var userId = User.GetUserId();
            var notifications = _context.UserNotifications
                .Where(un => un.UserId == userId && !un.IsRead)
                .ToList();

            notifications.ForEach(n => n.Read());

            _context.SaveChanges();

            return Ok();
        }
    }
}