using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialPlatformBlazor.Shared.ViewModels.Users
{
    /// <summary>
    ///     View model for a user 
    /// </summary>
    public class UserProfileModel
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string UserName { get; set; }

        public string? MainImagePath { get; set; }

        public string? BackgroundImagePath { get; set; }

        public int Followers { get; set; }

        public bool IsFollowed { get; set; }
    }
}
