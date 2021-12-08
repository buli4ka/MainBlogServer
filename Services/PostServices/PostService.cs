using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Server.DatabaseConfig;
using Server.Models.PostModels;
using Server.Utils;
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
            return await _context.Set<Post>().ToListAsync();
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
                .FirstOrDefaultAsync();

            if (post == null)
            {
                throw new KeyNotFoundException("Post not found");
            }

            return _mapper.Map<PostView>(post);
        }

        public async Task<List<PostView>> GetAllByUserId(Guid userId)
        {
            throw new NotImplementedException();
        }

        public async Task<List<PostView>> GetAllBySubscribed(Guid userId)
        {
            throw new NotImplementedException();
        }

        public async Task<PostView> Create(CreateUpdatePost model)
        {
            throw new NotImplementedException();
        }

        public async Task<PostView> Update(CreateUpdatePost model)
        {
            throw new NotImplementedException();
        }

        public async Task Delete(Guid postId)
        {
            throw new NotImplementedException();
        }
    }
}