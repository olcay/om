using System;
using Otomatik.Library.Web.Areas.Identity.Data;
using Otomatik.Library.Web.Core.Enums;

namespace Otomatik.Library.Web.Core.Models
{
    public class Notification
    {
        public int Id { get; private set; }

        public DateTime DateTime { get; private set; }

        public NotificationType Type { get; private set; }

        public ApplicationUser CreatedBy { get; set; }

        public string CreatedById { get; set; }

        public Item Item { get; private set; }
        
        public Shelf Shelf { get; private set; }

        public int ShelfId { get; set; }

        protected Notification()
        {
        }

        private Notification(NotificationType type, Shelf shelf, Item item)
        {
            Shelf = shelf ?? throw new ArgumentNullException(nameof(shelf));
            Item = item;
            Type = type;
            DateTime = DateTime.UtcNow;
        }

        public static Notification ShelfStarred(Shelf shelf)
        {
            return new Notification(NotificationType.ShelfStarred, shelf, null);
        }

        public static Notification ShelfCreated(Shelf shelf)
        {
            return new Notification(NotificationType.ShelfCreated, shelf, null);
        }

        public static Notification ItemAdded(Item item)
        {
            return new Notification(NotificationType.BookAdded, item.Shelf, item);
        }

        public static Notification ItemRemoved(Item item)
        {
            return new Notification(NotificationType.BookRemoved, item.Shelf, item);
        }
    }
}
