using System.ComponentModel.DataAnnotations;
using Server.Models;
using Server.Models.UserModels;

namespace Server.ViewModels.UserViewModels
{
    public class AuthenticateRequest
    {
        [Required] public string Username { get; set; }

        [Required] public string HashedPassword { get; set; }
    }

    public class AuthenticateResponse : BaseModel
    {
        public string Biography { get; set; }
        public bool IsPrivate { get; set; }
        public string JwtToken { get; set; }

        public AuthenticateResponse()
        {
        }

        public AuthenticateResponse(User user, string token)
        {
            Biography = user.Biography;
            IsPrivate = user.IsPrivate;
            JwtToken = token;
        }
    }

    public class RegisterRequest
    {
        [Required] [MaxLength(50)] public string FirstName { get; set; }

        [Required] [MaxLength(50)] public string Email { get; set; }

        [Required] [MaxLength(50)] public string LastName { get; set; }

        [Required] [MaxLength(50)] public string Username { get; set; }

        [Required] [MaxLength(50)] public string HashedPassword { get; set; }

        public string Biography { get; set; }

        public bool IsPrivate { get; set; }
    }
}