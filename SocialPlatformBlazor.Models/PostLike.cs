
using System.ComponentModel.DataAnnotations;

namespace SocialPlatformBlazor.Models
{
    public class PostLike
    {
        [Required]
        public string UserId { get; set; }

        public ApplicationUser User { get; set; }

        [Required]
        public string PostId { get; set; }

        public Post Post { get; set; }

        public DateTime LikedOn { get; set; }
    }
}
