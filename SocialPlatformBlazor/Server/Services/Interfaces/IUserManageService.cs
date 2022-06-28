using SocialPlatformBlazor.Models;

namespace SocialPlatformBlazor.Server.Services.Interfaces
{
    public interface IUserManageService 
    {
        Task UpdateAsync(ApplicationUser user);

        /// <summary>
        ///     Follow or Unfollow user by their id
        /// </summary>
        /// <param name="followedUserId">The id of the user to be followed</param>
        /// <param name="followerUserId">The if of the user follower</param>
        /// <returns></returns>
        Task FollowAsync(string followedUserId, string followerUserId);

        /// <summary>
        ///     The application user with id {userid}
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<ApplicationUser?> GetUserByIdAsync(string userId);

    }
}
