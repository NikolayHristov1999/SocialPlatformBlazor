namespace SocialPlatformBlazor.Shared.ViewModels.Users
{
    public class BaseUserInfoModel
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string UserName { get; set; }

        public string? MainImagePath { get; set; }

        public string? BackgroundImagePath { get; set; }
    }
}
