using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OtomatikMuhendis.Kutuphane.Web.Core.Models;

namespace OtomatikMuhendis.Kutuphane.Web.Data.EntityConfigurations
{
    public class FollowingConfiguration
    {
        public FollowingConfiguration(EntityTypeBuilder<Following> entityBuilder)
        {
            entityBuilder.HasKey(f => new { f.FollowerId, f.FolloweeId });
        }
    }
}
