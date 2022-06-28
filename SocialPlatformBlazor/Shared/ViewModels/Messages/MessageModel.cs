namespace SocialPlatformBlazor.Shared.ViewModels.Messages
{
    public class MessageModel
    {
        public string Message { get; set; }

        public string SenderUsername { get; set; }

        public string RecieverUsername { get; set; }

        public DateTime SendAt { get; set; }
    }
}
