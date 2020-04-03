using Otomatik.Library.Web.Core.Models;
using System.Collections.Generic;

namespace Otomatik.Library.Web.Core.ViewModels
{
    public class BookDetailViewModel
    {
        public int Id { get; set; }

        public string GoogleBookId { get; set; }

        public string Title { get; set; }

        public string Subtitle { get; set; }

        public string Description { get; set; }

        public string PublisherName { get; set; }

        public string PublishedYear { get; set; }

        public List<Shelf> ShelfList { get; set; }

        public string Authors { get; set; }

        public string ImageLink { get; internal set; }
    }
}
