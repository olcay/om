using AutoMapper;
using OtomatikMuhendis.Kutuphane.Web.Core.Dtos;
using OtomatikMuhendis.Kutuphane.Web.Core.Models;

namespace OtomatikMuhendis.Kutuphane.Web
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<ApplicationUser, UserDto>();
            CreateMap<UserDto, ApplicationUser>();

            CreateMap<ItemDto, Item>();
            CreateMap<Item, ItemDto>();

            CreateMap<ShelfDto, Shelf>();
            CreateMap<Shelf, ShelfDto>();

            CreateMap<Notification, NotificationDto>();
            CreateMap<NotificationDto, Notification>();

            CreateMap<UserNotification, UserNotificationDto>();
            CreateMap<UserNotificationDto, UserNotification>();
        }
    }
}
