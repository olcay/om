using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace OtomatikMuhendis.Kutuphane.Web.Core.ViewModels
{
    public class GameSearchViewModel
    {
        public string Search { get; set; }

        public IList<GameViewModel> GameList;

        public IEnumerable<SelectListItem> UserShelves { get; set; }

        public int ShelfId { get; set; }
    }
}
