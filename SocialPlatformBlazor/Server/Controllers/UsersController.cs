
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SocialPlatformBlazor.Infrastructure;
using SocialPlatformBlazor.Models;
using SocialPlatformBlazor.Server.Services.Interfaces;
using SocialPlatformBlazor.Shared.ViewModels.Posts;
using SocialPlatformBlazor.Shared.ViewModels.Users;
using System.Security.Claims;

namespace SocialPlatformBlazor.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IPostsService postsService;

        public UsersController(
            UserManager<ApplicationUser> _userManager,
            IPostsService postsService)
        {
            this._userManager = _userManager;
            this.postsService = postsService;
        }

        [HttpGet]
        public async Task<ActionResult<BaseUserInfoModel>> BasicUserInfo()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var currentUser = await _userManager.FindByIdAsync(userId);
            if (currentUser == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }
            var userModel = new BaseUserInfoModel()
            {
                FirstName = currentUser.FirstName,
                LastName = currentUser.LastName,
                Email = currentUser.Email,
                UserName = currentUser.UserName,
                MainImagePath = currentUser.MainImagePath,
                BackgroundImagePath = currentUser.BackgroundImagePath
            };

            return userModel;
        }

        [HttpGet]
        [Route("{username}/posts")]
        public async Task<ActionResult<IEnumerable<PostInFeedViewModel>>> GetUserPosts(
            string username,
            [FromQuery] int lastPostNumber = 0,
            [FromQuery] int postsCount = 10)
        {
            var postsModel = new List<PostInFeedViewModel>();
            var user = await _userManager.FindByNameAsync(username);
            if (user == null)
            {
                return NotFound($"Unable to load user with name '{username}'.");
            }

            var posts = postsService.GetLastPostsForUserAsync(user.Id, lastPostNumber, postsCount);
            foreach (var post in posts)
            {
                var ownerUser = await _userManager.FindByIdAsync(post.OwnerUserId);
                postsModel.Add(new PostInFeedViewModel
                {
                    Id = post.Id,
                    CreatedOn = post.CreatedOn,
                    Description = post.Description,
                    OwnerUserId = post.OwnerUserId,
                    OwnerUserFullName = ownerUser.FirstName + " " + ownerUser.LastName,
                    OwnerUserMainImagePath = ownerUser.MainImagePath,
                    OwnerUserUsername = ownerUser.UserName,
                    Likes = post.Likes,
                    Shares = post.Shares,
                    SharedPostId = post.SharedPostId,
                    IsLiked = (await postsService
                        .IsPostLikedByUserAsync(post.Id, ClaimsPrincipalExtension.GetId(User)) != null),
                });
            }

            return postsModel;
        }
    }
}
