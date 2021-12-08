using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Server.Services.PostServices;

namespace Server.Controllers.PostControllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PostController : ControllerBase
    {
        private readonly IPostService _postService;

        public PostController(IPostService postService)
        {
            _postService = postService;
        }

        [HttpGet("getById/{postId:guid}")]
        public async Task<IActionResult> Get(Guid postId)
        {
            var post = await _postService.GetPostById(postId);
            return Ok(post);
        }
    }
}