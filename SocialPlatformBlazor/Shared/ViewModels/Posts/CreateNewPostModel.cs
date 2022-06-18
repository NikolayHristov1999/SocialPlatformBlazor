using SocialPlatformBlazor.Shared.NumericTypes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialPlatformBlazor.Shared.ViewModels.Posts
{
    public class CreateNewPostModel
    {
        [MaxLength(10000)]
        [MinLength(3)]
        public string? Description { get; set; }

        [MaxLength(50)]
        [EnumDataType(typeof(VisibilityType))]
        public string? VisibilityType { get; set; }

        public string? SharedPostId { get; set; }

        public VisibilityType VisibilityTypeParams { get; set; }
    }
}
