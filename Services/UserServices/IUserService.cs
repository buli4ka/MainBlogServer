using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Server.Models.UserModels;
using Server.ViewModels.UserViewModels;

namespace Server.Services.UserServices
{
    public interface IUserService
    {
        Task<AuthenticateResponse> Authenticate(AuthenticateRequest model);
        
        Task<AuthenticateResponse> Register(RegisterRequest model);
        
        Task<IEnumerable<User>> GetAll();
        
        Task<IEnumerable<AuthorPreview>> GetUserSubscribers(Guid userId);
        
        Task<IEnumerable<AuthorPreview>> GetUserSubscribed(Guid userId);

        Task<AuthorView> GetAuthorById(Guid authorId);

        Task<UserView> GetUserById(Guid userId);

        Task<UserView> Update(Guid userId, UpdateRequest model);
        
        Task Delete(Guid userId);
        
        Task Subscribe(SubscribeRequest model);



    }
}