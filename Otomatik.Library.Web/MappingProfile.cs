using AutoMapper;
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
        }
    }
}
