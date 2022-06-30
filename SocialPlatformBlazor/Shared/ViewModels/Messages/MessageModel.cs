namespace SocialPlatformBlazor.Shared.ViewModels.Messages
{
    public class MessageModel
    {
        public string Text { get; set; }

        public string FromUserUsername { get; set; }

        public string ToUserUsername { get; set; }

        public DateTime CreatedOn { get; set; }
    }
}
