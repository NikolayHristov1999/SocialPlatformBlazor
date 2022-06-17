using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SocialPlatformBlazor.Models;
using SocialPlatformBlazor.Server.Data;
using SocialPlatformBlazor.Shared.ViewModels.Users;
using System.Security.Claims;

namespace SocialPlatformBlazor.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [AllowAnonymous]
    public class UsersController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public UsersController(UserManager<ApplicationUser> _userManager)
        {
            this._userManager = _userManager;
        }

        [HttpGet]
        [AllowAnonymous]
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
    }
}
