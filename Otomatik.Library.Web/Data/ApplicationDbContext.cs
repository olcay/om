using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Otomatik.Library.Web.Areas.Identity.Data;
using Otomatik.Library.Web.Core.Models;
using Otomatik.Library.Web.Data.EntityConfigurations;

namespace Otomatik.Library.Web.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>, IApplicationDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Item> Items { get; set; }
        public DbSet<Shelf> Shelves { get; set; }
        public DbSet<Star> Stars { get; set; }
        public DbSet<Following> Followings { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<UserNotification> UserNotifications { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<BookDetail> BookDetails { get; set; }
        public DbSet<BookAuthor> BookAuthors { get; set; }
        public DbSet<ItemBookDetail> ItemBookDetails { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            new ShelfConfiguration(builder.Entity<Shelf>());
            new ItemConfiguration(builder.Entity<Item>());
            new BookDetailConfiguration(builder.Entity<BookDetail>());
            new ItemBookDetailConfiguration(builder.Entity<ItemBookDetail>());
            new BookAuthorConfiguration(builder.Entity<BookAuthor>());
            new AuthorConfiguration(builder.Entity<Author>());
            new NotificationConfiguration(builder.Entity<Notification>());
            new StarConfiguration(builder.Entity<Star>());
            new FollowingConfiguration(builder.Entity<Following>());
            new UserNotificationConfiguration(builder.Entity<UserNotification>());
            new ApplicationUserConfiguration(builder.Entity<ApplicationUser>());

            base.OnModelCreating(builder);
        }
    }
}
