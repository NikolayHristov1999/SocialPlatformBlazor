using SocialPlatformBlazor.Models;
using SocialPlatformBlazor.Server.Data;
using SocialPlatformBlazor.Server.Services.Interfaces;
using SocialPlatformBlazor.Shared.ViewModels.Posts;

namespace SocialPlatformBlazor.Server.Services
{
    public class PostsService : IPostsService
    {
        private readonly ApplicationDbContext db;

        public PostsService(ApplicationDbContext _db)
        {
            db = _db;
        }

        /// <summary>
        ///     Save post to the database (If post model is corrent and the data is valid)
        /// </summary>
        /// <param name="postModel">The model that will be casted to Post entity</param>
        /// <param name="ownerUserId">The owner of the post (if it is from a page still shouldn't be null)</param>
        /// <param name="ownerPageId">Will be null if post isn't created from a page</param>
        /// <returns></returns>
        public async Task AddPostAsync(CreateNewPostModel postModel, string ownerUserId, string? ownerPageId = null)
        {
            var post = new Post
            {
                Description = postModel.Description,
                OwnerUserId = ownerUserId,
                OwnerPageId = ownerPageId,
                SharedPostId = postModel.SharedPostId,
                VisibilityType = postModel.VisibilityType ?? "public",
            };

            await db.Posts.AddAsync(post);
            await db.SaveChangesAsync();
        }

        public IEnumerable<Post> GetLastPostsAsync(int postsCount = 10)
        {
            return db.Posts.OrderByDescending(x => x.CreatedOn).Take(10).ToList();
        }

        /// <summary>
        ///     Like a post if it is not liked. Otherwise dislike it. Shouldn't be able to like own posts.
        /// </summary>
        /// <param name="postId">The id of the post to be liked</param>
        /// <param name="userId">The user that likes the post</param>
        /// <returns></returns>
        public async Task LikePostAsync(string postId, string userId)
        {
            var post = await db.Posts.FindAsync(postId);
            if (post == null || post.OwnerUserId == userId)
            {
                return;
            }

            var postLiked = db.PostsLikes.FirstOrDefault(x => x.PostId == postId && x.UserId == userId);

            if (postLiked == null)
            {
                if (post != null) {
                    postLiked = new PostLike
                    {
                        UserId = userId,
                        PostId = postId,
                        LikedOn = DateTime.UtcNow
                    };
                    await db.PostsLikes.AddAsync(postLiked);
                    post.Likes++;
                }
            }
            else
            {
                db.Remove(postLiked);
                post.Likes--;
            }

            db.Posts.Update(post);
            await db.SaveChangesAsync();
        }
    }
}
