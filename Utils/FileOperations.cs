using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Server.Exceptions;
using Server.Models;
using Server.Models.PostModels;

namespace Server.Utils
{
    public class FileOperations<TImage> where TImage : BaseImageModel, new()
    {
        private readonly AppSettings _appSettings;
        private readonly string _projectPath;

        public FileOperations(AppSettings appSettings)
        {
            _appSettings = appSettings;
            _projectPath = Directory.GetCurrentDirectory();
        }


        public async Task<TImage> GetAndCreateImage(IFormFile image, string relativePath)
        {
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

            return new TImage
            {
                ImageType = imageType,
                ImageName = imageName,
                ImagePath = relativePath,
            };
        }


        public async Task<TImage> GetAndUpdateImage(IFormFile image, string relativePath, TImage existingImage)
        {
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

            return existingImage;
        }

        public void ThrowLengthError(long imageLength)
        {
            if (imageLength <= 0)
                throw new AppException("Image is empty");
        }
        
       

        public void DeleteFile(TImage existingImage)
        {
          
            var postDirectory = Path.GetDirectoryName(Path.Combine(_projectPath, existingImage.ImagePath));

            if (postDirectory == null)
            {
                throw new Exception("This is very strange");
            }
            
            File.Delete(Path.Combine(_projectPath, existingImage.ImagePath));
            
            if (!Directory.EnumerateFiles(postDirectory).Any())
                Directory.Delete(postDirectory);

        }
    }
}