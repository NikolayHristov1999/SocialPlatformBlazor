using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using SocialPlatformBlazor.Models;
using SocialPlatformBlazor.Server.Services.Interfaces;
using SocialPlatformBlazor.Shared.ViewModels.Messages;

namespace SocialPlatformBlazor.Server.Hubs
{
    [Authorize]
    public class ChatHub : Hub
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IMapper mapper;
        private readonly IMessagesService messagesService;

        public ChatHub(
            UserManager<ApplicationUser> _userManager,
            IMapper mapper,
            IMessagesService messagesService)
        {
            userManager = _userManager;
            this.mapper = mapper;
            this.messagesService = messagesService;
        }

        public async Task SendMessage(string recieverUsername, string text)
        {
            var user = await userManager.FindByIdAsync(Context.UserIdentifier);
            var reciever = await userManager.FindByNameAsync(recieverUsername);

            if (user == null || reciever == null)
            {
                return;
            }

            var message = await messagesService.AddMessageAsync(text, user.Id, reciever.Id);
            var messageModel = mapper.Map<MessageModel>(message);

            
            await Clients.User(reciever.Id).SendAsync("ReceiveMessage", messageModel);
            await Clients.User(Context?.UserIdentifier!).SendAsync("ReceiveMessage", messageModel);
        }
    }
}
