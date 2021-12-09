using System;
using System.IO;
using System.Threading.Tasks;
using Server.Models.PostModels;
using Server.ViewModels.ImageViewModels;

namespace Server.Services.ImageServices
{
    public interface IImageService
    {
        public Task<PostImage> GetPostImageById(Guid imageId);
        
        public Task AddPostImage(FileView file, Guid postId);
        
        public Task UpdatePostImage(FileView file, Guid postId, Guid imageId);
        public Task DeletePostImage(Guid imageId);
        
        

    }
}