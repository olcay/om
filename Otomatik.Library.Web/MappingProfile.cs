using AutoMapper;
using Otomatik.Library.Web.Areas.Identity.Data;
using Otomatik.Library.Web.Core.Dtos;
using Otomatik.Library.Web.Core.Models;
using Otomatik.Library.Web.Core.ViewModels;

namespace OtomatikMuhendis.Kutuphane.Web
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Shelf, ShelfViewModel>();
            CreateMap<Item, ItemViewModel>();

            CreateMap<UserNotification, UserNotificationDto>().ReverseMap();
            CreateMap<ApplicationUser, UserDto>().ReverseMap();
            CreateMap<Item, ItemDto>().ReverseMap();
            CreateMap<Shelf, ShelfDto>().ReverseMap();
            CreateMap<Notification, NotificationDto>().ReverseMap();
        }
    }
}
