﻿using Otomatik.Library.Web.Areas.Identity.Data;

namespace Otomatik.Library.Web.Core.Models
{
    public class Following
    {
        public string FollowerId { get; set; }

        public string FolloweeId { get; set; }

        public ApplicationUser Follower { get; set; }

        public ApplicationUser Followee { get; set; }
    }
}