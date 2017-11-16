using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OtomatikMuhendis.Kutuphane.Web.Models
{
    public class Book
    {
        public int Id { get; set; }

        public ApplicationUser CreatedBy { get; set; }

        [Required]
        public string CreatedById { get; set; }

        public DateTime CreationDate { get; set; }

        [Required]
        [StringLength(255)]
        public string Title { get; set; }

        public Shelf Shelf { get; set; }

        [Required]
        public int ShelfId { get; set; }

        public bool IsDeleted { get; private set; }

        public void Delete(List<ApplicationUser> usersToNotify)
        {
            IsDeleted = true;

            var notification = Notification.BookRemoved(this);

            foreach (var follower in usersToNotify)
            {
                follower.Notify(notification);
            }
        }
    }
}
