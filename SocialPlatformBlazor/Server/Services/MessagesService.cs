using Microsoft.AspNetCore.Identity;
using SocialPlatformBlazor.Models;
using SocialPlatformBlazor.Server.Data;
using SocialPlatformBlazor.Server.Services.Interfaces;

namespace SocialPlatformBlazor.Server.Services
{
    public class MessagesService : IMessagesService
    {
        private readonly ApplicationDbContext db;
        private readonly UserManager<ApplicationUser> userManager;

        public MessagesService(
            ApplicationDbContext _db,
            UserManager<ApplicationUser> _userManager)
        {
            db = _db;
            userManager = _userManager;
        }
        /// <summary>
        ///     Add message to the database based on their id
        /// </summary>
        /// <param name="text">Message's text</param>
        /// <param name="fromUserId">Id of the sender of the message</param>
        /// <param name="toUserId">Id of the reciever of the message</param>
        /// <returns></returns>
        public async Task<Message> AddMessageAsync(string text, string fromUserId, string toUserId)
        {
            var message = new Message
            {
                Text = text,
                FromUserId = fromUserId,
                ToUserId = toUserId,
                CreatedOn = DateTime.UtcNow,
            };

            await db.Messages.AddAsync(message);
            await db.SaveChangesAsync();

            return message;
        }
        /// <summary>
        ///     Add message to the database by their username (Still saved based on their id)
        /// </summary>
        /// <param name="text">Message's text</param>
        /// <param name="fromUsername">The username of the sender</param>
        /// <param name="toUsername">The username of the reciever</param>
        /// <returns></returns>
        public async Task<Message> AddMessageByUsernameAsync(string text, string fromUsername, string toUsername)
        {
            // Find users based on their usernames and use their id to save the message in the db
            var fromUser = await userManager.FindByNameAsync(fromUsername);
            var toUser = await userManager.FindByNameAsync(toUsername);

            var message = await AddMessageAsync(text, fromUser.Id, toUser.Id);

            return message;
        }

        public async Task UnsendMessageAsync(string messageId)
        {
            var message = await db.Messages.FindAsync(messageId);

            if (message == null)
            {
                return;
            }

            message.IsDeleted = true;
            await db.SaveChangesAsync();
        }
    }
}
