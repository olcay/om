using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Otomatik.Library.Web.Core.Models;

namespace Otomatik.Library.Web.Data.EntityConfigurations
{
    public class FollowingConfiguration
    {
        public FollowingConfiguration(EntityTypeBuilder<Following> entityBuilder)
        {
            entityBuilder.HasKey(f => new { f.FollowerId, f.FolloweeId });
        }
    }
}
