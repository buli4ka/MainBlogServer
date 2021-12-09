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
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        
        public string Email { get; set; }
        
        public bool IsPrivate { get; set; }
        public string JwtToken { get; set; }

        public AuthenticateResponse()
        {
        }

        public AuthenticateResponse(User user, string token)
        {
            Id = user.Id;
            FirstName = user.FirstName;
            LastName = user.LastName;
            Username = user.Username;
            Email = user.Email;
            IsPrivate = user.IsPrivate;
            JwtToken = token;
        }
    }
    public class RegisterRequest
    {
        [Required]
        public string FirstName { get; set; }
        
        [Required]
        public string Email { get; set; }
        
        [Required]
        public string LastName { get; set; }

        [Required]
        public string Username { get; set; }

        [Required]
        public string HashedPassword { get; set; }
    }
}