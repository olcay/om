using System;
using System.ComponentModel.DataAnnotations;

namespace OtomatikMuhendis.Kutuphane.Web.Models
{
    public class Notification
    {
        public int Id { get; private set; }

        public DateTime DateTime { get; private set; }

        public NotificationType Type { get; private set; }

        public Book Book { get; private set; }

        [Required]
        public Shelf Shelf { get; private set; }

        protected Notification()
        {
        }

        private Notification(NotificationType type, Shelf shelf, Book book)
        {
            Shelf = shelf ?? throw new ArgumentNullException(nameof(shelf));
            Book = book;
            Type = type;
            DateTime = DateTime.UtcNow;
        }

        public static Notification ShelfCreated(Shelf shelf)
        {
            return new Notification(NotificationType.ShelfCreated, shelf, null);
        }

        public static Notification BookAdded(Book book)
        {
            return new Notification(NotificationType.BookAdded, book.Shelf, book);
        }

        public static Notification BookRemoved(Book book)
        {
            return new Notification(NotificationType.BookRemoved, book.Shelf, book);
        }
    }
}
