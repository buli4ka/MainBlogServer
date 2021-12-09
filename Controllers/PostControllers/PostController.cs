using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Server.Services.PostServices;
using Server.ViewModels.PostViewModels;

namespace Server.Controllers.PostControllers
{
    [ControllerAttributes.Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class PostController : ControllerBase
    {
        private readonly IPostService _postService;

        public PostController(IPostService postService)
        {
            _postService = postService;
        }

        [AllowAnonymous]
        [HttpGet("getById/{postId:guid}")]
        public async Task<IActionResult> Get(Guid postId)
        {
            return Ok(await _postService.GetPostById(postId));
        }

        [AllowAnonymous]
        [HttpGet("GetAllByAuthorId/{authorId:guid}")]
        public async Task<IActionResult> GetAllByAuthorId(Guid authorId)
        {
            return Ok(await _postService.GetAllByUserId(authorId));
        }
        [AllowAnonymous]

        [HttpPost("CreatePost")]
        public async Task<IActionResult> CreatePost(CreateUpdatePost post)
        {
            await _postService.Create(post);
            return Ok("Post Created");
        }
        [AllowAnonymous]

        [HttpPut("UpdatePost")]
        public async Task<IActionResult> UpdatePost(CreateUpdatePost post)
        {
            await _postService.Update(post);
            return Ok("Post Updated");
        }
        [AllowAnonymous]

        [HttpDelete("DeletePost")]
        public async Task<IActionResult> DeletePost([FromQuery(Name = "postId")] Guid postId, [FromQuery(Name = "authorId")] Guid authorId)
        {
            await _postService.Delete(postId, authorId);
            return NoContent();
        }
        
    }
}