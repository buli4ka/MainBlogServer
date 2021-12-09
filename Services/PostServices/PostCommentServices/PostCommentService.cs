using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Server.DatabaseConfig;
using Server.Exceptions;
using Server.Models.PostModels;
using Server.Utils;
using Server.Utils.PostValidation;
using Server.ViewModels.PostViewModels;

namespace Server.Services.PostServices.PostCommentServices
{
    public class PostCommentService : IPostCommentService
    {
        private readonly Context _context;
        private readonly AppSettings _appSettings;
        private readonly IMapper _mapper;

        public PostCommentService(IOptions<AppSettings> appSettings, Context context, IMapper mapper)
        {
            _appSettings = appSettings.Value;
            _context = context;
            _mapper = mapper;
        }


        public async Task<PostCommentView> GetCommentById(Guid commentId)
        {
            var comment = await _context.PostComments
                .Where(c => c.Id == commentId)
                .Include(c => c.User)
                // .Include(c => c.PostSubComments)
                .FirstOrDefaultAsync();
            if (comment == null)
            {
                throw new KeyNotFoundException("Comment not found");
            }
            return _mapper.Map<PostCommentView>(comment);
        }

        public async Task CreateComment(CreateUpdateCommentView requestComment)
        {
            var comment = _mapper.Map<PostComment>(requestComment);
            if (!CheckPost.IsCommentValid(comment))
            {
                throw new AppException("Invalid comment Data");
            }
            comment.CreatedAt = DateTime.Now;
            _context.Add(comment);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateComment(CreateUpdateCommentView requestComment)
        {
            var comment = await _context.PostComments
                .Where(c => c.Id == requestComment.Id)
                .Include(c => c.User)
                .Include(c => c.Post)
                .FirstOrDefaultAsync();
            
            if (comment == null)
            {
                throw new KeyNotFoundException("Comment not found");
            }
            
            if (comment.User.Id != comment.UserId || comment.Post.Id != comment.PostId)
            {
                throw new ForbbidenEcxeption("Not Correct Data");
            }

            _mapper.Map(requestComment, comment);
            if (!CheckPost.IsCommentValid(comment))
            {
                throw new AppException("Invalid comment Data");
            }
            comment.UpdatedAt = DateTime.Now;
            _context.Update(comment);
            await _context.SaveChangesAsync();

        }

        public async Task DeleteComment(Guid commentId)
        {
            var comment = await _context.PostComments.FindAsync(commentId);
            if (comment == null)
                throw new KeyNotFoundException("Comment not found");
            
            _context.Remove(comment);
            await _context.SaveChangesAsync();
                
        }
    }
}