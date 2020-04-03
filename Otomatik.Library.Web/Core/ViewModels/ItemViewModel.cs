using Otomatik.Library.Web.Core.Models;

namespace Otomatik.Library.Web.Core.ViewModels
{
    public class ItemViewModel : Item
    {
        public BookDetail BookDetail { get; set; }

        public string CoverImageUrl { get; set; }

        public GameViewModel GameDetail { get; set; }

        public bool IsShelfOwner { get; set; }
    }
}
