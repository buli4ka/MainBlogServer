using System;
using System.IO;
using Microsoft.AspNetCore.Mvc;

namespace Server.Controllers.ImageControllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ImageController : ControllerBase
    {
        [HttpGet("getById/{imageId:guid}")]
        public IActionResult Get(Guid imageId)
        {
            Console.WriteLine(imageId);
            return new PhysicalFileResult(Path.Combine(Directory.GetCurrentDirectory(), "Resources/NoImage/NoImage.png"),$"image/png");
        }
    }
}