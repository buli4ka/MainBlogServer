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
    public class ImageController : ControllerBase
    {
        private readonly AppSettings _appSettings;
        private readonly IImageService _imageService;

        public ImageController(IOptions<AppSettings> appSettings, IImageService imageService)
        {
            _appSettings = appSettings.Value;
            _imageService = imageService;
        }

        [AllowAnonymous]
        [HttpGet("getById/{imageId:guid}")]
        public async Task<IActionResult> GetById(Guid imageId)
        {
            var image = await _imageService.GetPostImageById(imageId);
            var projectPath = Directory.GetCurrentDirectory();
            if (image is null)
            {
                return new PhysicalFileResult(Path.Combine(projectPath, _appSettings.NoImagePath),
                    "image/png");
            }

            return new PhysicalFileResult(Path.Combine(projectPath, image.ImagePath),
                $"image/{image.ImageType}");
        }

        [AllowAnonymous]

        [HttpPost("AddPostImage/{postId:guid}"), DisableRequestSizeLimit]
        public async Task<IActionResult> AddPostImage([FromForm] FileView file, Guid postId)
        {
            await _imageService.AddPostImage(file, postId);
            return Ok();
        }

        [HttpPut("UpdatePostImage"), DisableRequestSizeLimit]
        public async Task<IActionResult> UpdatePostImage([FromForm] FileView file,
            [FromQuery(Name = "postId")] Guid postId, [FromQuery(Name = "imageId")] Guid imageId)
        {
            await _imageService.UpdatePostImage(file, postId, imageId);
            return Ok();
        }

        [HttpDelete("DeletePostImage/{imageId:guid}")]
        public async Task<IActionResult> DeletePostImage(Guid imageId)
        {
            await _imageService.DeletePostImage(imageId);
            return Ok();
        }
    }
}