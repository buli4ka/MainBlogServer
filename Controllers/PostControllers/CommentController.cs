using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Server.ControllerAttributes;
using Server.Services.PostServices.PostCommentServices;
using Server.ViewModels.PostViewModels;

namespace Server.Controllers.PostControllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class CommentController : ControllerBase
    {
        private readonly IPostCommentService _commentService;
        
        public CommentController(IPostCommentService commentService)
        {
            _commentService = commentService;
        }
        

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

        [HttpPut("UpdateComment")]
        public async Task<IActionResult> UpdateComment(CreateUpdateCommentView comment)
        {
            await _commentService.UpdateComment(comment);
            return Ok("Comment Updated");
        }

        [HttpDelete("DeleteComment/{commentId:guid}")]
        public async Task<IActionResult> DeleteComment(Guid commentId)
        {
            await _commentService.DeleteComment(commentId);
            return NoContent();
        }
    }
}