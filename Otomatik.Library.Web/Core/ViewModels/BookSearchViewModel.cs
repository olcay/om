using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;
using Otomatik.Library.Web.Core.Models;

namespace Otomatik.Library.Web.Core.ViewModels
{
    public class BookSearchViewModel
    {
        public string Search { get; set; }

        public IList<BookDetailViewModel> BookList;

        public IEnumerable<SelectListItem> UserShelves { get; set; }

        public int ShelfId { get; set; }
    }
}
