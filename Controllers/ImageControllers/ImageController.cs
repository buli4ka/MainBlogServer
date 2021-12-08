using System;
using System.IO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Server.Controllers.ImageControllers
{
    [ControllerAttributes.Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class ImageController : ControllerBase
    {
        
        [AllowAnonymous]
        [HttpGet("getById/{imageId:guid}")]
        public IActionResult Get(Guid imageId)
        {
            Console.WriteLine(imageId);
            return new PhysicalFileResult(Path.Combine(Directory.GetCurrentDirectory(), "Resources/NoImage/NoImage.png"),$"image/png");
        }
    }
}