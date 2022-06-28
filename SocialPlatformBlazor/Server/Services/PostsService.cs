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

        /// <summary>
        ///     Returns the latest posts(ordered by date descending)
        ///     from start to end(default 0 for start and 10 for posts count)
        /// </summary>
        /// <param name="startPost">
        ///     The number of the post to start (or lastly recieved post)
        ///     Default is 0
        /// </param>
        /// <param name="postsCount">
        ///     The number of posts to be returned
        /// </param>
        /// <returns>Posts</returns>
        public IEnumerable<Post> GetLastPostsAsync(int startPost = 0, int postsCount = 10)
        {
            return db.Posts
                .OrderByDescending(x => x.CreatedOn)
                .Skip(startPost)
                .Take(postsCount)
                .ToList();
        }

        /// <summary>
        ///     Returns the latest posts for a user(ordered by date descending)
        ///     from start to end(default 0 for start and 10 for posts count)
        /// </summary>
        /// <param name="userId">
        ///     Id of the user that owns the posts
        /// </param>
        /// <param name="startPost">
        ///     The number of the post to start (or lastly recieved post)
        ///     Default is 0
        /// </param>
        /// <param name="postsCount">
        ///     The number of posts to be returned
        /// </param>
        /// <returns>Posts</returns>
        public IEnumerable<Post> GetLastPostsForUserAsync(string userId, int startPost = 0, int postsCount = 10)
        {
            return db.Posts
                .Where(x => x.OwnerUserId == userId)
                .OrderByDescending(x => x.CreatedOn)
                .Skip(startPost)
                .Take(postsCount)
                .ToList();
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

            var postLiked = await IsPostLikedByUserAsync(postId, userId);

            if (postLiked == null)
            {
                if (post != null)
                {
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

            db.Posts.Update(post!);
            await db.SaveChangesAsync();
        }

        public async Task<PostLike?> IsPostLikedByUserAsync(string postId, string userId)
        {
            return await db.PostsLikes.FindAsync(postId, userId);
        }
    }
}
