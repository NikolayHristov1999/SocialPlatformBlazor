using SocialPlatformBlazor.Models;

namespace SocialPlatformBlazor.Server.Services.Interfaces
{
    public interface IMessagesService
    {
        public Task<Message> AddMessageAsync(string text, string fromUserId, string toUserId);

        public Task<Message> AddMessageByUsernameAsync(
            string text, string fromUsername, string toUsername);

        public Task UnsendMessageAsync(string messageId);
    }
}
