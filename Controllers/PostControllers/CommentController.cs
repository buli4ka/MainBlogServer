using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Server.Services.PostServices.PostCommentServices;
using Server.ViewModels.PostViewModels;

namespace Server.Controllers.PostControllers
{
    [ControllerAttributes.Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class CommentController : ControllerBase
    {
        private readonly IPostCommentService _commentService;
        
        public CommentController(IPostCommentService commentService)
        {
            _commentService = commentService;
        }
        
        [AllowAnonymous] // todo remove

        [HttpPost("CreateUpdateComment")]
        public async Task<IActionResult> CreateComment(CreateUpdateCommentView comment)
        {
            if (comment.Id != null)
            {
                await _commentService.UpdateComment(comment);
            }
            else
            {
                await _commentService.CreateComment(comment);
            }

            return Ok("Comment Created");
        }
        [AllowAnonymous] // todo remove

        [HttpPut("UpdateComment")]
        public async Task<IActionResult> UpdateComment(CreateUpdateCommentView comment)
        {
            await _commentService.UpdateComment(comment);
            return Ok("Comment Updated");
        }
        [AllowAnonymous] // todo remove

        [HttpDelete("DeleteComment/{commentId:guid}")]
        public async Task<IActionResult> DeleteComment(Guid commentId)
        {
            await _commentService.DeleteComment(commentId);
            return NoContent();
        }
    }
}