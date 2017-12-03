using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using OtomatikMuhendis.Kutuphane.Web.Core.Models;
using OtomatikMuhendis.Kutuphane.Web.Data.EntityConfigurations;

namespace OtomatikMuhendis.Kutuphane.Web.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Book> Books { get; set; }
        public DbSet<Shelf> Shelves { get; set; }
        public DbSet<Star> Stars { get; set; }
        public DbSet<Following> Followings { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<UserNotification> UserNotifications { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            new ShelfConfiguration(builder.Entity<Shelf>());
            new BookConfiguration(builder.Entity<Book>());
            new NotificationConfiguration(builder.Entity<Notification>());
            new StarConfiguration(builder.Entity<Star>());
            new FollowingConfiguration(builder.Entity<Following>());
            new UserNotificationConfiguration(builder.Entity<UserNotification>());
            new ApplicationUserConfiguration(builder.Entity<ApplicationUser>());

            base.OnModelCreating(builder);
        }
    }
}
