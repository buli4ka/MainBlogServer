using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Server.Models.UserModels;
using Server.Services.UserServices;
using Server.Utils.Jwt;

namespace Server.Utils
{
    public class JwtMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly AppSettings _appSettings;
        private readonly IMapper _mapper;

        public JwtMiddleware(RequestDelegate next, IOptions<AppSettings> appSettings, IMapper mapper)
        {
            _next = next;
            _appSettings = appSettings.Value;
            _mapper = mapper;

        }

        public async Task Invoke(HttpContext context, IUserService userService, IJwtUtils jwtUtils)
        {
            var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
            var userId = jwtUtils.ValidateToken(token);
            if (userId != null)
            {
                // attach user to context on successful jwt validation
                context.Items["User"] = _mapper.Map<User>(await userService.GetUserById(userId.Value));
            }

            await _next(context);
        }
    }
}