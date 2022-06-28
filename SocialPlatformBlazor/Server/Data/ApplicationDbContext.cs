using Duende.IdentityServer.EntityFramework.Options;

using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

using SocialPlatformBlazor.Models;

namespace SocialPlatformBlazor.Server.Data
{
    public class ApplicationDbContext : ApiAuthorizationDbContext<ApplicationUser>
    {
        public ApplicationDbContext(
            DbContextOptions options,
            IOptions<OperationalStoreOptions> operationalStoreOptions) 
            : base(options, operationalStoreOptions)
        {
        }

        public DbSet<Post> Posts { get; set; }

        public DbSet<Page> Pages { get; set; }

        public DbSet<PageFollower> PagesFollowers { get; set; }

        public DbSet<ModeratorPage> ModeratorsPages { get; set; }

        public DbSet<PostLike> PostsLikes { get; set; }

        public DbSet<UserFollower> UsersFollowers { get; set; }

        public DbSet<Message> Messages { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            //Candidate keys initializing
            builder.Entity<PageFollower>()
                .HasKey(x => new { x.PageId, x.UserId });
            builder.Entity<ModeratorPage>()
                .HasKey(x => new { x.PageId, x.UserId });
            builder.Entity<PostLike>()
                .HasKey(x => new { x.PostId, x.UserId });
            builder.Entity<UserFollower>()
                .HasKey(x => new { x.UserId, x.FollowerId });

            //When account is deleted, delete the rows first
            builder.Entity<UserFollower>()
                .HasOne(x => x.User)
                .WithMany(x => x.Followers)
                .HasForeignKey(x => x.UserId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<UserFollower>()
                .HasOne(x => x.Follower)
                .WithMany(x => x.Following)
                .HasForeignKey(x => x.FollowerId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<Message>()
                .HasOne(x => x.FromUser)
                .WithMany(x => x.SentMessages)
                .HasForeignKey(x => x.FromUserId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<Message>()
                .HasOne(x => x.ToUser)
                .WithMany(x => x.RecievedMessages)
                .HasForeignKey(x => x.ToUserId)
                .OnDelete(DeleteBehavior.NoAction);


            // needed for configuration
            base.OnModelCreating(builder);
        }

    }
}