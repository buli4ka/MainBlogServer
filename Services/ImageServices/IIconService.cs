using System;
using System.Threading.Tasks;
using Server.Models.UserModels;
using Server.ViewModels.ImageViewModels;

namespace Server.Services.ImageServices
{
    public interface IIconService
    {
        
        
        public Task<UserIcon> GetUserIconById(Guid imageId);
        
        public Task AddOrUpdateUserIcon(FileView file, Guid userId);

        
        public Task DeleteUserIcon(Guid imageId);
    }
}