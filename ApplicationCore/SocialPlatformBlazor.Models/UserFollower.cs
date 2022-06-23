using System.ComponentModel.DataAnnotations;


namespace SocialPlatformBlazor.Models
{
    public class UserFollower
    {
        [Required]
        public string UserId { get; set; }

        public ApplicationUser User { get; set; }

        [Required]
        public string FollowerId { get; set; }

        public ApplicationUser Follower { get; set; }

        public DateTime FollowedOn { get; set; }
    }
}
