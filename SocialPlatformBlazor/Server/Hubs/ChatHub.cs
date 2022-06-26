using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using SocialPlatformBlazor.Infrastructure;
using SocialPlatformBlazor.Models;
using SocialPlatformBlazor.Shared.ViewModels.Messages;
using System.Security.Claims;

namespace SocialPlatformBlazor.Server.Hubs
{
    [Authorize]
    public class ChatHub : Hub
    {
        private readonly UserManager<ApplicationUser> userManager;

        public ChatHub(UserManager<ApplicationUser> _userManager)
        {
            userManager = _userManager;
        }
        public async Task SendMessage(string recieverUsername, string message)
        {
            var user = await userManager.FindByIdAsync(Context.UserIdentifier);
            var reciever = await userManager.FindByNameAsync(recieverUsername);

            if (user == null || reciever == null)
            {
                return;
            }

            var messageModel = new MessageModel()
            {
                RecieverUsername = recieverUsername,
                SenderUsername = user.UserName,
                IsSeen = false,
                Message = message,
                SendAt = DateTime.UtcNow
            };

            
            await Clients.User(reciever.Id).SendAsync("ReceiveMessage", messageModel);
            await Clients.User(Context?.UserIdentifier!).SendAsync("ReceiveMessage", messageModel);
        }
    }
}
