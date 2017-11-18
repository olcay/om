using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OtomatikMuhendis.Kutuphane.Web.Data;
using OtomatikMuhendis.Kutuphane.Web.Dtos;
using OtomatikMuhendis.Kutuphane.Web.Extensions;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using OtomatikMuhendis.Kutuphane.Web.Models;

namespace OtomatikMuhendis.Kutuphane.Web.Controllers.Api
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [Authorize]
    [AutoValidateAntiforgeryToken]
    public class NotificationsController : Controller
    {
        public ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public NotificationsController(ApplicationDbContext context,
            IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public IEnumerable<NotificationDto> GetNewNotifications()
        {
            var userId = User.GetUserId();
            var notications = _context.UserNotifications
                .Where(un => un.UserId == userId && !un.IsRead)
                .Select(un => un.Notification)
                .Include(n => n.Book)
                .Include(n => n.Shelf.CreatedBy)
                .ToList();

            return notications.Select(_mapper.Map<Notification, NotificationDto>);
        }
    }
}