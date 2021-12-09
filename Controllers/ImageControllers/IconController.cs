using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Server.Services.ImageServices;
using Server.Utils;
using Server.ViewModels.ImageViewModels;

namespace Server.Controllers.ImageControllers
{
    [ControllerAttributes.Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class IconController : ControllerBase
    {
        private readonly AppSettings _appSettings;
        private readonly IIconService _imageService;

        public IconController(IOptions<AppSettings> appSettings, IIconService imageService)
        {
            _appSettings = appSettings.Value;
            _imageService = imageService;
        }

        [AllowAnonymous]
        [HttpGet("getById/{imageId:guid}")]
        public async Task<IActionResult> GetById(Guid imageId)
        {
            var image = await _imageService.GetUserIconById(imageId);
            var projectPath = Directory.GetCurrentDirectory();
            if (image is null)
            {
                return new PhysicalFileResult(Path.Combine(projectPath, _appSettings.NoImagePath),
                    "image/png");
            }

            return new PhysicalFileResult(Path.Combine(projectPath, image.ImagePath),
                $"image/{image.ImageType}");
        }

        [AllowAnonymous] // todo remove
        [HttpPost("AddPostImage/{userId:guid}"), DisableRequestSizeLimit]
        public async Task<IActionResult> AddPostImage([FromForm] FileView file, Guid userId)
        {
            await _imageService.AddOrUpdateUserIcon(file, userId);
            return Ok();
        }


        [AllowAnonymous] // todo remove
        [HttpDelete("DeletePostImage/{imageId:guid}")]
        public async Task<IActionResult> DeletePostImage(Guid imageId)
        {
            await _imageService.DeleteUserIcon(imageId);
            return Ok();
        }
    }
}