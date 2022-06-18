using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialPlatformBlazor.Shared.ViewModels.Posts
{
    public class PostInFeedViewModel
    {
        public string Id { get; set; }

        public string? Description { get; set; }

        public string? OwnerUserId { get; set; }

        public string? OwnerUserFullName { get; set; }

        public string? OwnerUserMainImagePath { get; set; }

        public DateTime CreatedOn { get; set; }

        public string? SharedPostId { get; set; }

        public int Likes { get; set; }

        public int Shares { get; set; }
    }
}
