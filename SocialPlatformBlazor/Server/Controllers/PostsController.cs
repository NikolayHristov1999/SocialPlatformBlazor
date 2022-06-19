using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SocialPlatformBlazor.Infrastructure;
using SocialPlatformBlazor.Models;
using SocialPlatformBlazor.Server.Services.Interfaces;
using SocialPlatformBlazor.Shared.ViewModels.Posts;
using System.Net;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SocialPlatformBlazor.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostsController : ControllerBase
    {
        private readonly IPostsService postsService;
        private readonly UserManager<ApplicationUser> userManager;

        public PostsController(
            IPostsService postsService,
            UserManager<ApplicationUser> _userManager)
        {
            this.postsService = postsService;
            userManager = _userManager;
        }
        // GET: api/<PostsController>
        [HttpGet]
        public async Task<IEnumerable<PostInFeedViewModel>> Get(
            [FromQuery] int lastPostNumber = 0,
            [FromQuery] int postsCount = 10)
        {
            var postsModel = new List<PostInFeedViewModel>();
            var posts = postsService.GetLastPostsAsync(lastPostNumber, postsCount);
            foreach (var post in posts)
            {
                var ownerUser = await userManager.FindByIdAsync(post.OwnerUserId);
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
                    IsLiked =  (await postsService
                        .IsPostLikedByUserAsync(post.Id, ClaimsPrincipalExtension.GetId(User)) != null),
                });
            }

            return postsModel;
        }

        // GET api/<PostsController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<PostsController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateNewPostModel postModel)
        {
            if (!ModelState.IsValid)
            {
                return UnprocessableEntity(postModel);
            }

            await postsService.AddPostAsync(postModel, ClaimsPrincipalExtension.GetId(User));

            return Ok();
        }

        // PUT api/<PostsController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<PostsController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }

        [Route("{id}/likes")]
        [HttpPost]
        public async Task<IActionResult> LikePost(string id)
        {
            await postsService.LikePostAsync(id, ClaimsPrincipalExtension.GetId(User));
            return Ok();
        }
    }
}
