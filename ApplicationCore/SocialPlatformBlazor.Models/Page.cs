using SocialPlatformBlazor.Models.BaseModels;
using System.ComponentModel.DataAnnotations;

namespace SocialPlatformBlazor.Models
{
    public class Page : BaseDeletableModel<string>
    {
        public Page()
        {
            this.Id = Guid.NewGuid().ToString();
            this.Posts = new HashSet<Post>();
            this.Followers = new HashSet<PageFollower>();
            this.Moderators = new HashSet<ModeratorPage>();
        }

        [MaxLength(100)]
        public string Name { get; set; }

        [MaxLength(1000)]
        public string Description { get; set; }

        [MaxLength(40)]
        public string Type { get; set; }

        public bool Verified { get; set; }

        public int FollowersCount { get; set; }

        [MaxLength(1000)]
        public string? MainImagePath { get; set; }

        [MaxLength(1000)]
        public string? BackgroundImagePath { get; set; }

        public virtual ICollection<Post> Posts { get; set; }

        public virtual ICollection<ModeratorPage> Moderators { get; set; }

        public virtual ICollection<PageFollower> Followers { get; set; }


    }
}
