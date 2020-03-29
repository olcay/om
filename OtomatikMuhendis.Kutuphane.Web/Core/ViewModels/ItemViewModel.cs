using OtomatikMuhendis.Kutuphane.Web.Core.Models;
using OtomatikMuhendis.Kutuphane.Web.Services.ApiClients;

namespace OtomatikMuhendis.Kutuphane.Web.Core.ViewModels
{
    public class ItemViewModel
    {
        public Item Item { get; set; }

        public BookDetail BookDetail { get; set; }

        public string CoverImageUrl { get; set; }

        public GameSingle GameDetail { get; set; }
    }
}
