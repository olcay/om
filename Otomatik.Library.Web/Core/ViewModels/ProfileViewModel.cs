using Otomatik.Library.Web.Areas.Identity.Data;

namespace Otomatik.Library.Web.Core.ViewModels
{
    public class ProfileViewModel
    {
        public ApplicationUser User { get; set; }

        public bool IsFollowing { get; set; }

        public bool IsProfileOwner { get; set; }

        public string Tab { get; set; }

        public string ImageUrl { get; set; }

        public bool ShowActions { get; set; }
        
        public int FollowersCount { get; set; }

        public int FollowingCount { get; set; }

        public int ShelvesCount { get; set; }

        public int Page { get; set; }
    }
}
