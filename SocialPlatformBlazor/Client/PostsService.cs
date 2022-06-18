using SocialPlatformBlazor.Shared.NumericTypes;
using SocialPlatformBlazor.Shared.ViewModels.Posts;
using System.Net.Http.Json;

namespace SocialPlatformBlazor.Client
{
    public class PostsService
    {
        private readonly HttpClient httpClient;

        public bool ShowingCreateNewPostDialog { get; set; }

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

        public async Task<IEnumerable<PostInFeedViewModel>> LoadPostsAsync()
        {
            var postsRecieved = await httpClient.GetFromJsonAsync<IEnumerable<PostInFeedViewModel>>("/api/posts");
            if (postsRecieved != null)
            {
                return postsRecieved;
            }
            return new List<PostInFeedViewModel>();
        }
    }
}
