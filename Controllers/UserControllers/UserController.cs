using System;
using System.Threading.Tasks;
using AutoMapper;
// using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Server.ControllerAttributes;
using Server.Models.UserModels;
using Server.Services.UserServices;
using Server.Utils;
using Server.ViewModels.UserViewModels;

namespace Server.Controllers.UserControllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private IMapper _mapper;
        private readonly AppSettings _appSettings;

        public UserController(
            IUserService userService,
            IMapper mapper,
            IOptions<AppSettings> appSettings)
        {
            _userService = userService;
            _mapper = mapper;
            _appSettings = appSettings.Value;
        }

        [AllowAnonymous]
        [HttpPost("authenticate")]
        public async Task<IActionResult> Authenticate(AuthenticateRequest model)
        {
            var response = await _userService.Authenticate(model);
            return Ok(response);
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterRequest model)
        {
            var user = await _userService.Register(model);
            return Ok(user);
        }

        [HttpPost("subscribe")]
        public async Task<IActionResult> Subscribe(SubscribeRequest model)
        {
            await _userService.Subscribe(model);
            return Ok();
        }

        [HttpGet("getAll")]
        public async Task<IActionResult> GetAll()
        {
            var users = await _userService.GetAll();
            return Ok(users);
        }

        [HttpGet("getById/{id:guid}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var user = await _userService.GetUserById(id);
            return Ok(user);
        }

        [HttpGet("GetUserSubscribers/{userId:guid}")]
        public async Task<IActionResult> GetUserSubscribers(Guid userId)
        {
            var user = await _userService.GetUserSubscribers(userId);
            return Ok(user);
        }

        [HttpGet("GetUserSubscribed/{userId:guid}")]
        public async Task<IActionResult> GetUserSubscribed(Guid userId)
        {
            var user = await _userService.GetUserSubscribed(userId);
            return Ok(user);
        }

        [AllowAnonymous]
        [HttpGet("getAuthorById/{id:guid}")]
        public async Task<IActionResult> GetAuthorById(Guid id)
        {
            var user = await _userService.GetAuthorById(id);
            return Ok(user);
        }


        // how to update
        [HttpPut("update/{id:guid}")]
        public async Task<IActionResult> Update(Guid id, UpdateRequest model)
        {
            var user = await _userService.Update(id, model);
            return Ok(user);
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _userService.Delete(id);
            return Ok(new {message = "User deleted successfully"});
        }

        [HttpGet("isUserSubscribed")]
        public async Task<IActionResult> IsUserSubscribed([FromQuery(Name = "userId")] Guid userId,
            [FromQuery(Name = "authorId")] Guid authorId)
        {
            return Ok(await _userService.IsSubscribed(userId, authorId));
        }
    }
}