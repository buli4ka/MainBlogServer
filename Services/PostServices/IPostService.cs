using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Server.Models.PostModels;
using Server.ViewModels.PostViewModels;

namespace Server.Services.PostServices
{
    public interface IPostService
    {
        Task<List<Post>> GetAll();
        
        Task<PostView> GetPostById(Guid postId);
        
        Task<List<PostView>> GetAllByUserId(Guid userId);
        
        Task<List<PostView>> GetAllBySubscribed(Guid userId);

        Task<PostView> Create(CreateUpdatePost model);

        Task<PostView> Update(CreateUpdatePost model);
        
        Task Delete(Guid postId);


    }
}