

using Microsoft.EntityFrameworkCore;
using SocialPlatformBlazor.Models;
using SocialPlatformBlazor.Server.Data;
using SocialPlatformBlazor.Server.Services.Interfaces;
using SocialPlatformBlazor.Shared.ViewModels.Users;

namespace SocialPlatformBlazor.Server.Services
{
    public class UserManageService : IUserManageService
    {
        private readonly ApplicationDbContext _db;

        public UserManageService(
            ApplicationDbContext _db)
        {
            this._db = _db;
        }
        
        public async Task UpdateAsync(ApplicationUser user) 
        {
            _db.Users.Update(user);
            try
            {
                await _db.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                
            }
        }

        public async Task FollowAsync(string followedUserId, string followerUserId)
        {
            var userFollower = await _db.UsersFollowers.FindAsync(followedUserId, followerUserId);
            if (userFollower == null)
            {
                userFollower = new UserFollower()
                {
                    UserId = followedUserId,
                    FollowerId = followerUserId,
                    FollowedOn = DateTime.UtcNow
                };
                await _db.UsersFollowers.AddAsync(userFollower);
            }
            else
            {
                _db.UsersFollowers.Remove(userFollower);
            }
            
            await _db.SaveChangesAsync();
        }

        public async Task<ApplicationUser?> GetUserByIdAsync(string userId)
        {
            return await _db.Users
                .Where(x => x.Id == userId)
                .Include(x => x.Followers)
                .FirstOrDefaultAsync();
        }
    }
}
