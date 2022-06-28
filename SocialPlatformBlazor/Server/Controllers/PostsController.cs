using AutoMapper;
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
        private readonly IMapper mapper;
        private readonly IPostsService postsService;
        private readonly UserManager<ApplicationUser> userManager;

        public PostsController(
            IMapper mapper,
            IPostsService postsService,
            UserManager<ApplicationUser> _userManager)
        {
            this.mapper = mapper;
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
                var postInFeedModel = mapper.Map<PostInFeedViewModel>(post);
                postInFeedModel.IsLiked = await postsService
                        .IsPostLikedByUserAsync(post.Id, ClaimsPrincipalExtension.GetId(User)) != null;
                postsModel.Add(postInFeedModel);
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
