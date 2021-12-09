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
        private readonly string _projectPath;

        public ImageService(IOptions<AppSettings> appSettings, Context context)
        {
            _context = context;
            _appSettings = appSettings.Value;
            _projectPath = Directory.GetCurrentDirectory();
        }

        public async Task<PostImage> GetPostImageById(Guid imageId)
        {
            return await _context.PostImages.FindAsync(imageId);
        }

        public async Task AddPostImage(FileView file, Guid postId)
        {
            var image = file.File;
            var relativePath = $"{_appSettings.PostImagesPath}/{postId}/";

            if (image.Length <= 0)
                throw new AppException("Image is empty");

            var imageType = Path.GetExtension(image.FileName)[1..];
            var imageName = Guid.NewGuid().ToString();

            Directory.CreateDirectory(Path.Combine(_projectPath, relativePath));
            relativePath += imageName + '.' + imageType;
            try
            {
                await using var fs = new FileStream(Path.Combine(_projectPath, relativePath),
                    FileMode.Create);
                await image.CopyToAsync(fs);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw new Exception("Internal server error, an error was occured when saving an image");
            }

            var result = new PostImage
            {
                ImageType = imageType,
                ImageName = imageName,
                ImagePath = relativePath,
                PostId = postId
            };
            _context.Add(result);
            await _context.SaveChangesAsync();
        }


        public async Task UpdatePostImage(FileView file, Guid postId, Guid imageId)
        {
            var existingImage = await _context.PostImages.FindAsync(imageId);
            if (existingImage == null)
            {
                throw new KeyNotFoundException("Nothing to update");
            }

            var image = file.File;
            var relativePath = $"{_appSettings.PostImagesPath}/{postId}/";

            if (image.Length <= 0)
                throw new AppException("Image is empty");

            var imageType = Path.GetExtension(image.FileName)[1..];

            relativePath += existingImage.ImageName + '.' + imageType;

            File.Delete(Path.Combine(_projectPath, existingImage.ImagePath));

            try
            {
                await using var fs = new FileStream(Path.Combine(_projectPath, relativePath),
                    FileMode.Create);
                await image.CopyToAsync(fs);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw new Exception("Internal server error, an error was occured when saving an image");
            }

           
            existingImage.ImageType = imageType;
            existingImage.ImagePath = relativePath;

            _context.Update(existingImage);
            await _context.SaveChangesAsync();
        }

        public async Task DeletePostImage(Guid imageId)
        {
            var existingImage = await _context.PostImages.FindAsync(imageId);

            if (existingImage == null)
            {
                throw new KeyNotFoundException("No image found");
            }

            File.Delete(Path.Combine(_projectPath, existingImage.ImagePath));
            
            _context.Remove(existingImage);
            await _context.SaveChangesAsync();
            
            var postDirectory = Path.GetDirectoryName(Path.Combine(_projectPath, existingImage.ImagePath));

            if (postDirectory == null)
            {
                throw new Exception("This is very strange");
            }
            
            if (!Directory.EnumerateFiles(postDirectory).Any())
                Directory.Delete(postDirectory);
            
           
        }
    }
}