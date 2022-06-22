using SocialPlatformBlazor.Shared.ViewModels.Users;
using System.Net.Http.Json;

namespace SocialPlatformBlazor.Client.Services
{
    public class UserService
    {
        private readonly HttpClient httpClient;

        public UserService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<UserProfileModel?> GetUserProfileAsync(string username)
        {
            return await httpClient.GetFromJsonAsync<UserProfileModel>($"/api/users/{username}");
        }

        public async Task<bool> FollowUserAsync(string username)
        {
            var response = await httpClient.PostAsync($"/api/users/{username}/follow", null);

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return true;
            }

            return false;
        }
    }
}
