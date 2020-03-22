using Microsoft.EntityFrameworkCore;
using OtomatikMuhendis.Kutuphane.Web.Core.Models;

namespace OtomatikMuhendis.Kutuphane.Web.Data
{
    public interface IApplicationDbContext
    {
        DbSet<Item> Items { get; set; }
        DbSet<Shelf> Shelves { get; set; }
        DbSet<Star> Stars { get; set; }
        DbSet<Following> Followings { get; set; }
        DbSet<Notification> Notifications { get; set; }
        DbSet<UserNotification> UserNotifications { get; set; }
        DbSet<ApplicationUser> Users { get; set; }
        DbSet<Author> Authors { get; set; }
        DbSet<BookDetail> BookDetails { get; set; }
        DbSet<BookAuthor> BookAuthors { get; set; }
        DbSet<ItemBookDetail> ItemBookDetails { get; set; }
    }
}