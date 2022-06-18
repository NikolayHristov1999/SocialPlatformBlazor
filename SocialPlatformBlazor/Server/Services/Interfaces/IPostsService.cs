using SocialPlatformBlazor.Models;
using SocialPlatformBlazor.Shared.ViewModels.Posts;

namespace SocialPlatformBlazor.Server.Services.Interfaces
{
    public interface IPostsService
    {
        /// <summary>
        ///     Save post to the database (If post model is corrent and the data is valid)
        /// </summary>
        /// <param name="postModel">The model that will be casted to Post entity</param>
        /// <param name="ownerUserId">The owner of the post (if it is from a page still shouldn't be null)</param>
        /// <param name="ownerPageId">Will be null if post isn't created from a page</param>
        /// <returns></returns>
        Task AddPostAsync(CreateNewPostModel postModel, string ownerUserId, string? ownerPageId = null);

        IEnumerable<Post> GetLastPostsAsync(int postsCount = 10);

        /// <summary>
        ///     Like a post if it is not liked. Otherwise dislike it. Shouldn't be able to like own posts.
        /// </summary>
        /// <param name="id">The id of the post to be liked</param>
        /// <param name="userId">The user that likes the post</param>
        /// <returns></returns>
        Task LikePostAsync(string id, string userId);
    }
}
