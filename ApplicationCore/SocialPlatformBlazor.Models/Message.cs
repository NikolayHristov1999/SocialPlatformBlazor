using SocialPlatformBlazor.Models.BaseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialPlatformBlazor.Models
{
    public class Message : BaseDeletableModel<string>
    {
        public Message()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        public string FromUserId { get; set; }

        public ApplicationUser FromUser { get; set; }

        public string ToUserId { get; set; }

        public ApplicationUser ToUser { get; set; }

        public string Text { get; set; }
    }
}
