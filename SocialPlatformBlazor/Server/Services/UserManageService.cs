

using SocialPlatformBlazor.Models;
using SocialPlatformBlazor.Server.Data;
using SocialPlatformBlazor.Server.Services.Interfaces;

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
    }
}
