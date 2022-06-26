using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using SocialPlatformBlazor.Models;
using System.Security.Claims;

namespace SocialPlatformBlazor.Server.Hubs.Configurations
{
    public class NameBasedUserIdProvider : IUserIdProvider
    {
        private readonly UserManager<ApplicationUser> userManager;

        public NameBasedUserIdProvider(UserManager<ApplicationUser> userManager)
        {
            this.userManager = userManager;
        }
        public virtual string GetUserId(HubConnectionContext connection)
        {
            return connection.User?.FindFirst(ClaimTypes.Email)?.Value!;
        }
    }
}
