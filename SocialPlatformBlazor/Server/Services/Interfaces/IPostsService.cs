﻿using SocialPlatformBlazor.Models;
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
        IEnumerable<Post> GetLastPostsAsync(int startPost = 0, int postsCount = 10);

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
        IEnumerable<Post> GetLastPostsForUserAsync(string userId, int startPost = 0, int postsCount = 10);

        /// <summary>
        ///     Like a post if it is not liked. Otherwise dislike it. Shouldn't be able to like own posts.
        /// </summary>
        /// <param name="id">The id of the post to be liked</param>
        /// <param name="userId">The user that likes the post</param>
        /// <returns></returns>
        Task LikePostAsync(string id, string userId);


        /// <summary>
        ///     Search db for a post liked by a user
        /// </summary>
        /// <param name="postId">Id of the post</param>
        /// <param name="userId">Id of the user</param>
        /// <returns>PostLike model if found or null</returns>
        Task<PostLike?> IsPostLikedByUserAsync(string postId, string userId);
    }
}
