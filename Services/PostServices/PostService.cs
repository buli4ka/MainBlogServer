using System;
using System.Collections.Generic;
using System.IO;
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
using Server.ViewModels.UserViewModels;

namespace Server.Services.PostServices
{
    public class PostService : IPostService
    {
        private readonly Context _context;
        private readonly AppSettings _appSettings;
        private readonly IMapper _mapper;

        public PostService(IOptions<AppSettings> appSettings, Context context, IMapper mapper)
        {
            _appSettings = appSettings.Value;
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<Post>> GetAll()
        {
            return await _context.Posts.ToListAsync();
        }

        public async Task<PostView> GetPostById(Guid postId)
        {
            var post = await _context.Posts
                .Where(p => p.Id == postId)
                .Include(post => post.Author)
                .ThenInclude(author => author.UserIcon)
                .Include(post => post.PostComments)
                .Include(post => post.PostLikes)
                .Include(post => post.PostImages)
                .AsSplitQuery()
                .FirstOrDefaultAsync();

            if (post == null)
            {
                throw new KeyNotFoundException("Post not found");
            }

            return _mapper.Map<PostView>(post);
        }

        public async Task<List<PostPreview>> GetAllByUserId(Guid userId)
        {
            var user = await _context.Users
                .Where(user => user.Id == userId)
                .Include(user => user.Posts)
                .ThenInclude(post => post.PostComments)
                .Include(user => user.Posts)
                .ThenInclude(post => post.PostLikes)
                .Include(user => user.Posts)
                .ThenInclude(post => post.PostImages)
                .AsSplitQuery()
                .FirstOrDefaultAsync();

            if (user == null)
            {
                throw new KeyNotFoundException("User not found");
            }

            return user.Posts.Select(post => _mapper.Map<PostPreview>(post)).ToList();
        }


        public async Task Create(CreateUpdatePost model)
        {
            var post = _mapper.Map<Post>(model);

            if (!CheckPost.isPostValid(post))
                throw new AppException("Invalid post Data");
            post.CreatedAt = DateTime.Now;

            _context.Add(post);
            await _context.SaveChangesAsync();
        }

        public async Task Update(CreateUpdatePost model)
        {
            var post = await _context.Posts
                .Where(p => p.Id == model.Id)
                .Include(p => p.Author)
                .FirstOrDefaultAsync();

            if (post == null)
            {
                throw new KeyNotFoundException("Post not found");
            }

            if (post.Author.Id != model.AuthorId)
            {
                throw new ForbbidenEcxeption("Not Correct author");
            }

            _mapper.Map(model, post);

            if (!CheckPost.isPostValid(post))
                throw new AppException("Invalid post Data");

            post.UpdatedAt = DateTime.Now;

            _context.Update(post);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(Guid postId, Guid authorId)
        {
            var post = await _context.Posts
                .Where(p => p.Id == postId)
                .Include(p => p.Author)
                .FirstOrDefaultAsync();

            if (post == null)
                throw new KeyNotFoundException("Post not found");

            if (post.Author.Id != authorId)
            {
                throw new ForbbidenEcxeption("Not Correct author");
            }

            _context.Remove(post);
            await _context.SaveChangesAsync();

            var postDirectory = Path.GetDirectoryName(Path.Combine(Directory.GetCurrentDirectory(),
                _appSettings.PostImagesPath, postId.ToString()));
            if (postDirectory is not null)
            {
                Directory.Delete(postDirectory, true);
            }
        }
    }
}