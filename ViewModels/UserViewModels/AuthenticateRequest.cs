using System.ComponentModel.DataAnnotations;

namespace Server.ViewModels.UserViewModels
{
    public class AuthenticateRequest
    {
        [Required] public string Username { get; set; }

        [Required] public string HashedPassword { get; set; }
    }
}