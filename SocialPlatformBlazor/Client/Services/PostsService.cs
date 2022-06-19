using SocialPlatformBlazor.Shared.NumericTypes;
using SocialPlatformBlazor.Shared.ViewModels.Posts;
using System.Net.Http.Json;

namespace SocialPlatformBlazor.Client.Services
{
    public class PostsService
    {
        private readonly HttpClient httpClient;

        public bool ShowingCreateNewPostDialog { get; set; } = false;

        public int MyProperty { get; set; }

        public PostsService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public void ShowCreateNewPostDialog()
        {
            ShowingCreateNewPostDialog = true;
        }


        public void CloseCreateNewPostDialog()
        {
            ShowingCreateNewPostDialog = false;
        }

        public CreateNewPostModel InitCreateNewPostModel()
        {
            var createNewPostModel = new CreateNewPostModel()
            {
                VisibilityType = VisibilityType.Public.ToString(),
            };

            return createNewPostModel;
        }

        public async Task AddPostAsync(CreateNewPostModel post)
        {
            await httpClient.PostAsJsonAsync("api/posts", post);

        }
        /// <summary>
        ///     Load posts from server. Default value is /api/posts
        /// </summary>
        /// <param name="apiUrl">
        ///     The url of the api server.
        ///     Default value is /api/posts
        /// </param>
        /// <param name="lastPostNumber">
        ///     The last post that was recieved
        ///     Default value is 0
        /// </param>
        /// <param name="numberOfPostsToLoad">
        ///     The number of posts to be loaded
        ///     Default value is 10
        /// </param>
        /// <returns></returns>
        public async Task<IEnumerable<PostInFeedViewModel>> LoadPostsAsync(
            string apiUrl = "/api/posts",
            int lastPostNumber = 0, 
            int numberOfPostsToLoad = 10)
        {
            var postsRecieved = await httpClient.GetFromJsonAsync<IEnumerable<PostInFeedViewModel>>(
                apiUrl + $"?lastPostNumber={lastPostNumber}&postsCount={numberOfPostsToLoad}");
            if (postsRecieved != null)
            {
                return postsRecieved;
            }
            return new List<PostInFeedViewModel>();
        }

        /// <summary>
        ///     Load posts from server.
        /// </summary>
        /// <param name="lastPostNumber">
        ///     The last post that was recieved
        ///     Default value is 0
        /// </param>
        /// <param name="numberOfPostsToLoad">
        ///     The number of posts to be loaded
        ///     Default value is 10
        /// </param>
        /// <returns></returns>
        public async Task<IEnumerable<PostInFeedViewModel>> LoadUserPostsAsync(
            string userName,
            int lastPostNumber = 0, 
            int numberOfPostsToLoad = 10)
        {
            return await LoadPostsAsync(
                $"api/users/{userName}/posts",
                lastPostNumber,
                numberOfPostsToLoad);
        }

        public async Task LikeOrDislikePost(string postId)
        {
            await httpClient.PostAsync($"/api/posts/{postId}/likes", null);
        }
    }
}
