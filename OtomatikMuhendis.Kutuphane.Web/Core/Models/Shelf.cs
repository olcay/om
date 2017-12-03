using System;
using System.Collections.Generic;

namespace OtomatikMuhendis.Kutuphane.Web.Core.Models
{
    public class Shelf
    {
        public int Id { get; set; }
        
        public string Title { get; set; }

        public ApplicationUser CreatedBy { get; set; }
        
        public string CreatedById { get; set; }

        public DateTime CreationDate { get; set; }

        public IEnumerable<Book> Books { get; set; }

        public IEnumerable<Star> Stars { get; set; }

        public bool IsDeleted { get; set; }

        public bool IsPublic { get; set; }

        public DateTime UpdateDate { get; set; }
        
        public int StarsCount { get; set; }

        public Shelf()
        {
            
        }

        public Shelf(string createdById, string title)
        {
            CreatedById = createdById ?? throw new ArgumentNullException(nameof(createdById));
            Title = title ?? throw new ArgumentNullException(nameof(title));

            CreationDate = DateTime.UtcNow;
            UpdateDate = DateTime.UtcNow;
        }

        public void Publish(IEnumerable<ApplicationUser> usersToNotify)
        {
            IsPublic = true;
            
            var notification = Notification.ShelfCreated(this);

            foreach (var follower in usersToNotify)
            {
                follower.Notify(notification);
            }
            
        }
    }
}
