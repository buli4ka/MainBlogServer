using System;
using Server.Models.UserModels;

namespace Server.Utils.Jwt
{
    public interface IJwtUtils
    {
        public string GenerateToken(User user);
        public Guid? ValidateToken(string token);
    }
}