using SocialPlatformBlazor.Models;
using SocialPlatformBlazor.Shared.ViewModels.Posts;

namespace SocialPlatformBlazor.Server.Services.Interfaces
{
    public interface IPostsService
    {
        Task AddPostAsync(CreateNewPostModel postModel, string ownerUserId, string? ownerPageId = null);
        IEnumerable<Post> GetLastPostsAsync(int postsCount = 10);
    }
}
