using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Server.Models.PostModels;
using Server.ViewModels.PostViewModels;

namespace Server.Services.PostServices
{
    public interface IPostService
    {
        Task<List<PostPreview>> GetAll();
        
        Task<PostView> GetPostById(Guid postId);
        
        Task<List<PostPreview>> GetAllByUserId(Guid userId);
        
        Task Create(CreateUpdatePost model);

        Task Update(CreateUpdatePost model);
        
        Task Delete(Guid postId, Guid authorId);

        Task AddRemoveLike(AddLike requestLike);


    }
}