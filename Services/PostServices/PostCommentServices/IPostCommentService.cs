using System;
using System.Threading.Tasks;
using Server.ViewModels.PostViewModels;

namespace Server.Services.PostServices.PostCommentServices
{
    public interface IPostCommentService
    {

        // Task<PostCommentView> AddSubComment(PostSubCommentView subComment, Guid commentId);
        Task<PostCommentView> GetCommentById(Guid commentId);
        Task CreateComment(CreateUpdateCommentView requestComment);
        
        Task UpdateComment(CreateUpdateCommentView requestComment);
        
        Task DeleteComment(Guid commentId);

    }
}