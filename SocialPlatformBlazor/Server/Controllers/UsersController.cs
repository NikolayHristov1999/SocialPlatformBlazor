
using Microsoft.AspNetCore.Authorization;
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
    [Authorize]
    public class UsersController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IPostsService postsService;
        private readonly IUserManageService userManageService;

        public UsersController(
            UserManager<ApplicationUser> _userManager,
            IPostsService postsService,
            IUserManageService userManageService)
        {
            this._userManager = _userManager;
            this.postsService = postsService;
            this.userManageService = userManageService;
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

        /// <summary>
        ///     Search for user profile based on their username and returns their data as json or not found
        /// </summary>
        /// <param name="username"></param>
        /// <returns>Their data as json or not found</returns>
        [HttpGet]
        [Route("{username}")]
        public async Task<ActionResult<UserProfileModel>> UserProfile(string username)
        {
            var user2 = await _userManager.FindByNameAsync(username);
            var loggedUserId = ClaimsPrincipalExtension.GetId(User);
            var user = await userManageService.GetUserByIdAsync(user2.Id);

            if (user == null)
            {
                return NotFound($"Unable to load user with name '{username}'.");
            }

            var userModel = new UserProfileModel()
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                UserName = user.UserName,
                MainImagePath = user.MainImagePath,
                BackgroundImagePath = user.BackgroundImagePath,
                Followers = user.Followers.Count,
                IsFollowed = user.Followers.Any(followers => followers.FollowerId == loggedUserId),
            };

            return userModel;
        }

        [HttpPost]
        [Route("{username}/follow")]
        public async Task<IActionResult> FollowOrUnfollowUser(string username)
        {
            var followedUser = await _userManager.FindByNameAsync(username);
            if (followedUser == null)
            {
                return NotFound($"Unable to find username '{username}'.");
            }

            var loggedUserId = ClaimsPrincipalExtension.GetId(User);

            if (loggedUserId == null)
            {
                return NotFound($"Unable to find logged user. Please relogin");
            }
            if (followedUser.Id == loggedUserId)
            {
                return StatusCode(409);
            }
            await userManageService.FollowAsync(followedUser.Id, loggedUserId);
            return Ok();
        }
    }
}
