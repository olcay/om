using AutoMapper;
using OtomatikMuhendis.Kutuphane.Web.Dtos;
using OtomatikMuhendis.Kutuphane.Web.Models;

namespace OtomatikMuhendis.Kutuphane.Web
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<ApplicationUser, UserDto>();
            CreateMap<UserDto, ApplicationUser>();

            CreateMap<BookDto, Book>();
            CreateMap<Book, BookDto>();

            CreateMap<ShelfDto, Shelf>();
            CreateMap<Shelf, ShelfDto>();

            CreateMap<Notification, NotificationDto>();
            CreateMap<NotificationDto, Notification>();

            CreateMap<UserNotification, UserNotificationDto>();
            CreateMap<UserNotificationDto, UserNotification>();
        }
    }
}
