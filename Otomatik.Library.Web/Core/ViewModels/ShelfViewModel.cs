using System.Collections.Generic;
using Otomatik.Library.Web.Core.Models;

namespace Otomatik.Library.Web.Core.ViewModels
{
    public class ShelfViewModel
    {
        public Shelf Shelf { get; set; }

        public bool ShowActions { get; set; }

        public bool IsShelfOwner { get; set; }

        public bool IsStarred { get; set; }

        public List<ItemViewModel> Items { get; set; }
    }
}
