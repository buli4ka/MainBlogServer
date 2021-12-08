using System;
using Server.Models.UserModels;

namespace Server.ViewModels.UserViewModels
{
    public class AuthenticateResponse
    {
        public Guid Id { get; set; }
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
}