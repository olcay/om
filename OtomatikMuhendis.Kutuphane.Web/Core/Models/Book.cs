using System;
using System.Collections.Generic;

namespace OtomatikMuhendis.Kutuphane.Web.Core.Models
{
    public class Book
    {
        public int Id { get; set; }

        public ApplicationUser CreatedBy { get; set; }
        
        public string CreatedById { get; set; }

        public DateTime CreationDate { get; set; }
        
        public string Title { get; set; }

        public Shelf Shelf { get; set; }
        
        public int ShelfId { get; set; }

        public BookDetail BookDetail { get; set; }

        public int? BookDetailId { get; set; }

        public bool IsDeleted { get; private set; }

        public void Delete()
        {
            IsDeleted = true;
        }

        public void Notify(IEnumerable<ApplicationUser> usersToNotify)
        {
            var notification = Notification.BookRemoved(this);

            foreach (var follower in usersToNotify)
            {
                follower.Notify(notification);
            }
        }

        internal void Reactivate()
        {
            IsDeleted = false;
        }
    }
}
