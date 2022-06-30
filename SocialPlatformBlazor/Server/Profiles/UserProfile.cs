using AutoMapper;
using SocialPlatformBlazor.Models;
using SocialPlatformBlazor.Shared.ViewModels.Messages;
using SocialPlatformBlazor.Shared.ViewModels.Posts;

namespace SocialPlatformBlazor.Server.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<Post, PostInFeedViewModel>()
                .ForMember(dest => dest.OwnerUserFullName, opt => opt.MapFrom(x =>
                        x.OwnerUser != null ? x.OwnerUser.FirstName + " " + x.OwnerUser.LastName : ""));
            
            CreateMap<Message, MessageModel>();
        }
        
    }
}
