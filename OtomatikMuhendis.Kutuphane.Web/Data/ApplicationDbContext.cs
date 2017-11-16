using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using OtomatikMuhendis.Kutuphane.Web.Models;
using OtomatikMuhendis.Kutuphane.Web.Persistence.EntityConfigurations;

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
            builder.Entity<Star>().HasKey(s => new {s.ShelfId, s.UserId});
            builder.Entity<Following>().HasKey(f => new { f.FollowerId, f.FolloweeId });
            builder.Entity<UserNotification>().HasKey(un => new { un.UserId, un.NotificationId });

            base.OnModelCreating(builder);

            new ApplicationUserConfiguration(builder.Entity<ApplicationUser>());
        }
    }
}
