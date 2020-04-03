using System.Collections.Generic;
using Otomatik.Library.Web.Core.Models;

namespace Otomatik.Library.Web.Core.ViewModels
{
    public class ShelfViewModel : Shelf
    {
        public bool ShowActions { get; set; }

        public bool IsShelfOwner { get; set; }

        public bool IsStarred { get; set; }

        public List<ItemViewModel> Items { get; set; }
    }
}
