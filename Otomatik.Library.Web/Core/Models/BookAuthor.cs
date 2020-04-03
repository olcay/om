namespace Otomatik.Library.Web.Core.Models
{
    public class BookAuthor
    {
        public BookDetail BookDetail { get; set; }

        public Author Author { get; set; }

        public int BookDetailId { get; set; }

        public int AuthorId { get; set; }
    }
}
