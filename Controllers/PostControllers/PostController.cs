using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Server.ControllerAttributes;
using Server.Services.PostServices;
using Server.ViewModels.PostViewModels;

namespace Server.Controllers.PostControllers
{
    [Authorize]
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
        [HttpGet("getAll")]
        public async Task<IActionResult> Get()
        {
            return Ok(await _postService.GetAll());
        }
        
        [AllowAnonymous]
        [HttpGet("getById/{postId:guid}")]
        public async Task<IActionResult> Get(Guid postId)
        {
            return Ok(await _postService.GetPostById(postId));
        }
        [AllowAnonymous]
        [HttpGet("getPreviewsById/{userId:guid}")]
        public async Task<IActionResult> GetPreview(Guid userId)
        {
            return Ok(await _postService.GetPostPreviewsById(userId));
        }

        [AllowAnonymous]
        [HttpGet("GetAllByAuthorId/{authorId:guid}")]
        public async Task<IActionResult> GetAllByAuthorId(Guid authorId)
        {
            return Ok(await _postService.GetAllByUserId(authorId));
        }

        [HttpPost("CreatePost")]
        public async Task<IActionResult> CreatePost(CreateUpdatePost post)
        {
            var response = await _postService.Create(post);
            return CreatedAtAction(nameof(CreatePost), new {id=response.Id}, response);
        }

        [HttpPut("UpdatePost")]
        public async Task<IActionResult> UpdatePost(CreateUpdatePost post)
        {
            await _postService.Update(post);
            return Ok("Post Updated");
        }

        [HttpDelete("DeletePost")]
        public async Task<IActionResult> DeletePost([FromQuery(Name = "postId")] Guid postId, [FromQuery(Name = "authorId")] Guid authorId)
        {
            await _postService.Delete(postId, authorId);
            return NoContent();
        }
        

        [HttpPost("AddLike")]
        public async Task<IActionResult> AddLike(AddLike like)
        {
            await _postService.AddRemoveLike(like);
            return Ok();
        }
        
    }
}