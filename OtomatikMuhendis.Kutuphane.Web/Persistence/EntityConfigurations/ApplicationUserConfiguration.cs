using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OtomatikMuhendis.Kutuphane.Web.Models;

namespace OtomatikMuhendis.Kutuphane.Web.Persistence.EntityConfigurations
{
    public class ApplicationUserConfiguration
    {
        public ApplicationUserConfiguration(EntityTypeBuilder<ApplicationUser> entityBuilder)
        {
            entityBuilder.HasMany(u => u.Followees)
                .WithOne(a => a.Followee)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            entityBuilder.HasMany(u => u.Followers)
                .WithOne(a => a.Follower)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
