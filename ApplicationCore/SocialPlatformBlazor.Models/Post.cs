using SocialPlatformBlazor.Models.BaseModels;
using System.ComponentModel.DataAnnotations;

namespace SocialPlatformBlazor.Models
{
    public class Post : BaseDeletableModel<string>
    {
        public Post()
        {
            this.Id = Guid.NewGuid().ToString();
            this.PostLikes = new HashSet<PostLike>();
            this.PostShares = new HashSet<Post>();
        }

        [MaxLength(10000)]
        public string? Description { get; set; }

        [MaxLength(50)]
        public string VisibilityType { get; set; }

        public int Likes { get; set; }

        public int Shares { get; set; }

        public string? OwnerUserId { get; set; }

        public ApplicationUser? OwnerUser { get; set; }

        public string? OwnerPageId { get; set; }

        public Page? OwnerPage { get; set; }

        public string? SharedPostId { get; set; }

        public Post SharedPost { get; set; }

        public ICollection<PostLike> PostLikes { get; set; }

        public ICollection<Post> PostShares { get; set; }

    }
}
