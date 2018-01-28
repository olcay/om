using System.Collections.Generic;

namespace OtomatikMuhendis.Kutuphane.Web.Core.Models
{
    public class BookDetail
    {
        public int Id { get; set; }

        public string GoogleBookId { get; set; }

        public string Subtitle { get; set; }

        public string Description { get; set; }

        public IEnumerable<BookAuthor> BookAuthorList { get; set; }

        public string Publisher { get; set; }

        public short? PublishedYear { get; set; }

        public int? PageCount { get; set; }

        public string ImageLink { get; set; }

        public string Isbn10 { get; set; }

        public string Isbn13 { get; set; }
    }
}
