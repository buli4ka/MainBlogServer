using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Server.DatabaseConfig;
using Server.Exceptions;
using Server.Models.UserModels;
using Server.Utils;
using Server.ViewModels.ImageViewModels;

namespace Server.Services.ImageServices
{
    public class IconService : IIconService
    {
        private readonly Context _context;
        private readonly AppSettings _appSettings;
        private readonly FileOperations<UserIcon> _fileOperations;
        public IconService(IOptions<AppSettings> appSettings, Context context)
        {
            _context = context;
            _appSettings = appSettings.Value;
            _fileOperations = new FileOperations<UserIcon>(appSettings.Value);
        }

        public async Task<UserIcon> GetUserIconById(Guid imageId)
        {
            return await _context.UserIcons.FindAsync(imageId);
        }

        public async Task AddOrUpdateUserIcon(FileView file, Guid userId)
        {
            var user = await _context.Users.Where(u => u.Id == userId)
                .Include(u => u.UserIcon)
                .FirstOrDefaultAsync();

            if (user == null)
            {
                throw new KeyNotFoundException("User not Found");
            }
            
            var image = file.File;
          
            _fileOperations.ThrowLengthError(image.Length);

            var relativePath = $"{_appSettings.UserIconPath}/{userId}/";
            
            UserIcon result;
            if (user.UserIcon != null)
            {
                result = await _fileOperations.GetAndUpdateImage(image, relativePath, user?.UserIcon);
                _context.Update(result);
            }
            else
            {
                result = await _fileOperations.GetAndCreateImage(image, relativePath);
                result.UserId = userId;
                _context.Add(result);
            }
           
            await _context.SaveChangesAsync();
        }
        
        public async Task DeleteUserIcon(Guid imageId)
        {
            var existingImage = await _context.UserIcons.FindAsync(imageId);
            
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