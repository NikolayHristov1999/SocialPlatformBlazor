using System.ComponentModel.DataAnnotations;

namespace SocialPlatformBlazor.Models
{
    public class ModeratorPage
    {
        [Required]
        public string UserId { get; set; }

        public ApplicationUser User { get; set; }

        [Required]
        public string PageId { get; set; }

        public Page Page { get; set; }

        [Required]
        [MaxLength(100)]
        public string Priviliges { get; set; }
    }
}
