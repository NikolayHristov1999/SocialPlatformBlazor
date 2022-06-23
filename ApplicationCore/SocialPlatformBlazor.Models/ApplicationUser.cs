using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;
using SocialPlatformBlazor.Models.BaseModels;

namespace SocialPlatformBlazor.Models
{
    public class ApplicationUser : IdentityUser, IAuditInfo, IDeletableEntity
    {
        public ApplicationUser()
            :base()
        {
            this.Posts = new HashSet<Post>();
            this.Likes = new HashSet<PostLike>();
            this.FollowedPages = new HashSet<PageFollower>();
            this.ModeratorPages = new HashSet<ModeratorPage>();
        }
        [Required]
        [MaxLength(60)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(60)]
        public string LastName { get; set; }

        [DataType("Date")]
        public DateTime DateOfBirth { get; set; }

        [Required]
        [MaxLength(80)]
        public string Country { get; set; }

        [MaxLength(80)]
        public string Town { get; set; }

        // Audit info
        public DateTime CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }

        // Deletable entity
        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }

        [MaxLength(1000)]
        public string? MainImagePath { get; set; }

        [MaxLength(1000)]
        public string? BackgroundImagePath { get; set; }

        public virtual ICollection<Post> Posts { get; set; }

        public virtual ICollection<PostLike> Likes { get; set; }

        public virtual ICollection<PageFollower> FollowedPages { get; set; }

        public virtual ICollection<ModeratorPage> ModeratorPages { get; set; }

        public virtual ICollection<UserFollower> Followers { get; set; }

        public virtual ICollection<UserFollower> Following { get; set; }

    }
}
