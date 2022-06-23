using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialPlatformBlazor.Models
{
    public class PageFollower
    {
        [Required]
        public string PageId { get; set; }

        public Page Page { get; set; }

        [Required]
        public string UserId { get; set; }

        public ApplicationUser User { get; set; }

        public DateTime FollowedOn { get; set; }
    }
}
