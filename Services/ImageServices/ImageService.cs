using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.Extensions.Options;
using Server.DatabaseConfig;
using Server.Exceptions;
using Server.Models.PostModels;
using Server.Utils;
using Server.ViewModels.ImageViewModels;

namespace Server.Services.ImageServices
{
    public class ImageService : IImageService
    {
        private readonly Context _context;
        private readonly AppSettings _appSettings;
        private readonly FileOperations<PostImage> _fileOperations;


        public ImageService(IOptions<AppSettings> appSettings, Context context)
        {
            _context = context;
            _appSettings = appSettings.Value;
            _fileOperations = new FileOperations<PostImage>(appSettings.Value);
        }

        public async Task<PostImage> GetPostImageById(Guid imageId)
        {
            return await _context.PostImages.FindAsync(imageId);
        }

        public async Task AddPostImage(FileView file, Guid postId)
        {
            var image = file.File;

            _fileOperations.ThrowLengthError(image.Length);

            var relativePath = $"{_appSettings.PostImagesPath}/{postId}/";

            var result = await _fileOperations.GetAndCreateImage(image, relativePath);
            result.PostId = postId;
            
            _context.Add(result);
            await _context.SaveChangesAsync();
        }


        public async Task UpdatePostImage(FileView file, Guid postId, Guid imageId)
        {
            var image = file.File;
            var existingImage = await _context.PostImages.FindAsync(imageId);
            
            if (existingImage == null)
            {
                throw new KeyNotFoundException("Nothing to update");
            }
            
            _fileOperations.ThrowLengthError(image.Length);

            var relativePath = $"{_appSettings.PostImagesPath}/{postId}/";

            var result = await _fileOperations.GetAndUpdateImage(image, relativePath, existingImage);
            
            _context.Update(result);
            await _context.SaveChangesAsync();
        }

        public async Task DeletePostImage(Guid imageId)
        {
            var existingImage = await _context.PostImages.FindAsync(imageId);

            if (existingImage == null)
            {
                throw new KeyNotFoundException("No image found");
            }
            
            _fileOperations.DeleteFile(existingImage);
            
            _context.Remove(existingImage);
            
            await _context.SaveChangesAsync();
            
           
        }
    }
}