using System;
using System.Collections.Generic;
using Otomatik.Library.Web.Areas.Identity.Data;
using Otomatik.Library.Web.Core.Enums;
using Otomatik.Library.Web.Core.Helpers;

namespace Otomatik.Library.Web.Core.Models
{
    public class Item
    {
        public int Id { get; set; }

        public ApplicationUser CreatedBy { get; set; }
        
        public string CreatedById { get; set; }

        public DateTime CreationDate { get; set; }
        
        public string Title { get; private set; }

        public Shelf Shelf { get; set; }
        
        public int ShelfId { get; set; }

        public ItemType Type { get; set; }

        public string CoverId { get; set; }

        public int? RawgId { get; set; }

        public string Slug { get; set; }

        public bool IsDeleted { get; private set; }

        public string Description { get; private set; }

        public bool IsPlayed { get; set; }

        public bool IsFinished { get; set; }

        public Item()
        {
            
        }

        public Item(string title, string userId)
        {
            CreationDate = DateTime.UtcNow;
            SetTitle(title);
            CreatedById = userId;
        }

        public void Delete()
        {
            IsDeleted = true;
        }

        public void SetTitle(string title)
        {
            Title = !string.IsNullOrWhiteSpace(title) && title.Length > 255
                ? title.Substring(255)
                : title; ;
            Slug = SlugGenerator.GenerateSlug(Title);
        }

        public void SetDescription(string description)
        {
            Description = !string.IsNullOrWhiteSpace(description) && description.Length > 1000
                ? description.Substring(1000)
                : description;
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
