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
    }
}
