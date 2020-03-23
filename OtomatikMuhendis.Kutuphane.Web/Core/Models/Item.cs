using System;
using System.Collections.Generic;
using OtomatikMuhendis.Kutuphane.Web.Core.Enums;

namespace OtomatikMuhendis.Kutuphane.Web.Core.Models
{
    public class Item
    {
        public int Id { get; set; }

        public ApplicationUser CreatedBy { get; set; }
        
        public string CreatedById { get; set; }

        public DateTime CreationDate { get; set; }
        
        public string Title { get; set; }

        public Shelf Shelf { get; set; }
        
        public int ShelfId { get; set; }

        public ItemType Type { get; set; }

        public string CoverId { get; set; }

        public bool IsDeleted { get; private set; }

        public void Delete()
        {
            IsDeleted = true;
        }

        public void Notify(IEnumerable<ApplicationUser> usersToNotify)
        {
            var notification = Notification.ItemRemoved(this);

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
