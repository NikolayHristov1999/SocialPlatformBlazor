using SocialPlatformBlazor.Models;

namespace SocialPlatformBlazor.Server.Services.Interfaces
{
    public interface IUserManageService 
    {
        Task UpdateAsync(ApplicationUser user);
    }
}
