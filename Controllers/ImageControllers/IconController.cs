using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Server.ControllerAttributes;
using Server.Services.ImageServices;
using Server.Utils;
using Server.ViewModels.ImageViewModels;

namespace Server.Controllers.ImageControllers
{
    [Authorize]
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
        public async Task<IActionResult> GetUserIconById(Guid imageId)
        {
            var image = await _imageService.GetUserIconById(imageId);
            if (image is null)
            {
                return new PhysicalFileResult(Path.Combine(_appSettings.ProjectDirectory, _appSettings.NoImagePath),
                    "image/png");
            }

            return new PhysicalFileResult(Path.Combine(_appSettings.ProjectDirectory, image.ImagePath),
                $"image/{image.ImageType}");
        }

        [HttpPost("AddOrUpdateUserIcon/{userId:guid}"), DisableRequestSizeLimit]
        public async Task<IActionResult> AddOrUpdateUserIcon([FromForm] FileView file, Guid userId)
        {
            await _imageService.AddOrUpdateUserIcon(file, userId);
            return Ok();
        }


        [HttpDelete("DeleteUserIcon/{imageId:guid}")]
        public async Task<IActionResult> DeleteUserIcon(Guid imageId)
        {
            await _imageService.DeleteUserIcon(imageId);
            return Ok();
        }
    }
}